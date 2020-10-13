using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeppaController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float acceleration = 0.001f;
    public float deceleration = 0.05f;
    public Canvas uiCanvas;
    public AudioClip fruitCatchSound;
    public AudioClip fruitLostSound;
    public AudioClip oneSound;
    public AudioClip twoSound;
    public AudioClip threeSound;
    public AudioClip fourSound;
    public AudioClip startTalkSound;
    public AudioClip endTalkSound;
    public GameObject gameEngine;
    private float speed = 0.0f;
    private bool facingLeft = true;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private AudioSource peppaAudioSource;
    private List<GameObject> fruitList; // lista owoców aktualnie znajdujących się w koszyku
    private int collectedFruits; // liczba jabłek w koszyku
    private bool isWorking;

	// Use this for initialization
	void Start () {

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        peppaAudioSource = GetComponent<AudioSource>();
        Random.InitState(20);
        fruitList = new List<GameObject>();
        collectedFruits = 0;
        isWorking = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (isWorking)
        {
            if (transform.position.x > 11.0f && !facingLeft || transform.position.x < -11.0f && facingLeft)
            {
                speed = 0.0f;
                myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);
                myAnimator.SetFloat("walk_speed", speed);
                if (facingLeft && Input.GetAxis("Horizontal") > 0.0f || !facingLeft && Input.GetAxis("Horizontal") < 0.0f) Flip();
            }
            else if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") != 0.0f)
            {
                if (Mathf.Abs(speed) < maxSpeed)
                {
                    if (Input.GetAxis("Horizontal") > 0.0f)
                    {
                        if (speed == 0) speed = 1.0f;
                        else
                        {
                            if (speed >= 0.0f) speed += acceleration;
                            else speed += acceleration * 6.0f;
                        }
                    }
                    else
                    {
                        if (speed == 0) speed = -1.0f;
                        else
                        {
                            if (speed <= 0.0f) speed -= acceleration;
                            else speed -= acceleration * 6.0f;
                        }
                    }
                }
            }
            else if (speed != 0.0f)
            {
                if (speed > 0.0f)
                {
                    if (speed >= deceleration) speed -= deceleration;
                    else speed = 0.0f;
                }
                else
                {
                    if (speed <= -deceleration) speed += deceleration;
                    else speed = 0.0f;
                }
            }
            myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);
            myAnimator.SetFloat("walk_speed", Mathf.Abs(speed));
            if (speed < 0.0f && !facingLeft) Flip();
            else if (speed > 0.0f && facingLeft) Flip();
        }
    }

    void Flip()
    {
        Rigidbody2D rb;
        Vector3 theScale;
        Vector2 moveVector, newPosition;
        facingLeft = !facingLeft;
        theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        foreach (GameObject fruit in fruitList)
        {
            rb = fruit.GetComponent<Rigidbody2D>();
            moveVector = new Vector2((myRigidbody2D.position.x - fruit.transform.position.x) * 2, 0);
            newPosition = new Vector2(rb.position.x + moveVector.x, fruit.transform.position.y);
            rb.transform.position = newPosition;
            fruit.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        }
    }

    public void CheckSnort()
    {
        if (Random.value < 0.2f)
        {
            myAnimator.SetBool("isSnort", true);
            peppaAudioSource.Play();
        }
    }

    public void EndSnort()
    {
        myAnimator.SetBool("isSnort", false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject fruit;
        if (other.gameObject.tag == "Mummy")
        {
            Debug.Log("Zderzenie z Mamą");
            if (collectedFruits == GameController.numberToCollect)
            {
                if (collectedFruits < 4)
                {
                    other.GetComponent<MummyController>().CongratulatePeppa();
                    gameEngine.GetComponent<GameController>().Restart();
                    while (fruitList.Count > 0)
                    {
                        Destroy(fruitList[0]);
                    }
                    fruitList.Clear();
                    collectedFruits = 0;
                }
                else
                {
                    other.GetComponent<MummyController>().GiveCake();
                    collectedFruits = 0;
                }
            }
        }
        else
        {
            peppaAudioSource.PlayOneShot(fruitCatchSound, 1.0f);
            fruit = other.gameObject;
            //Debug.Log("Id owocu z collidera: " + fruit.GetComponent<FruitController>().GetFruitType());
            //Debug.Log("Id owocu do zebrania: " + GameController.fruitToCollect);

            if (fruit.GetComponent<FruitController>().GetFruitType() == GameController.fruitToCollect && collectedFruits < GameController.numberToCollect)
            {
                collectedFruits++;
                myAnimator.SetBool("isTalk", true);
                switch (collectedFruits)
                {
                    case 1:
                        peppaAudioSource.PlayOneShot(oneSound, 1.0f);
                        break;
                    case 2:
                        peppaAudioSource.PlayOneShot(twoSound, 1.0f);
                        break;
                    case 3:
                        peppaAudioSource.PlayOneShot(threeSound, 1.0f);
                        break;
                    case 4:
                        peppaAudioSource.PlayOneShot(fourSound, 1.0f);
                        break;
                }
                //Debug.Log("Zaraz wyślę że złapałem");
                uiCanvas.GetComponent<uiCanvasController>().CatchFruit();
            }
            fruitList.Add(fruit);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        GameObject fruit;
        if (other.gameObject.tag != "Mummy")
        {
            fruit = other.gameObject;
            if (fruit.GetComponent<FruitController>().GetFruitType() == GameController.fruitToCollect)
            {
                collectedFruits--;
                uiCanvas.GetComponent<uiCanvasController>().LostFruit();
                myAnimator.SetBool("isTalk", true);
                peppaAudioSource.PlayOneShot(fruitLostSound, 1.4f);
            }
            fruitList.Remove(fruit);
        }
        else Debug.Log("Koniec zderzenia z Mamą");
    }

    public void Jump()
    {
        //myRigidbody2D.AddForce(transform.up * 75.0f);
        Rigidbody2D tempRigidbody2D;
        foreach (GameObject fruit in fruitList)
        {
            tempRigidbody2D = fruit.GetComponent<Rigidbody2D>();
            tempRigidbody2D.MovePosition(tempRigidbody2D.position + new Vector2(0, 0.4f));
            tempRigidbody2D.AddForce(transform.up*180.0f);
        }
    }

    public void StopTalk()
    {
        myAnimator.SetBool("isTalk", false);
    }

    private IEnumerator WaitAndTalk()
    {
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetBool("isLongTalk", true);
        peppaAudioSource.PlayOneShot(startTalkSound, 1.4f);
    }

    public void StartTalk()
    {
        StartCoroutine(WaitAndTalk());
    }

    public void StopLongTalk()
    {
        myAnimator.SetBool("isLongTalk", false);
        isWorking = true;
    }

    private IEnumerator WaitAndTalk2()
    {
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetBool("isLongTalk2", true);
        peppaAudioSource.PlayOneShot(endTalkSound, 1.4f);
        gameEngine.GetComponent<GameController>().StopWork();
        isWorking = false;
        while (fruitList.Count > 0)
        {
            Destroy(fruitList[0]);
        }
        fruitList.Clear();
    }

    public void StartTalk2()
    {
        StartCoroutine(WaitAndTalk2());
    }

    public void StopLongTalk2()
    {
        myAnimator.SetBool("isLongTalk2", false);
    }
}
