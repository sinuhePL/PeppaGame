using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {
    private Animator fruitAnimator;
    private Rigidbody2D fruitRigidbody2D;
    private SpriteRenderer fruitRenderer;
    private GameObject gameMaster;
    private int myBranch;
    public Transform fruitParticleSystem;
    private int fruitType;


    // Use this for initialization
    void Start () {
        fruitAnimator = GetComponent<Animator>();
        fruitRigidbody2D = GetComponent<Rigidbody2D>();
        fruitRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetFruitType()
    {
        return fruitType;
    }

    public void SetGameMaster(GameObject gm, int i, int j)
    {
        gameMaster = gm;
        myBranch = i;
        fruitType = j;
    }

    public void StopAnimation()
    {
        fruitAnimator.enabled = false;
        fruitRigidbody2D.gravityScale = 1.0f;
        gameMaster.GetComponent<GameController>().FreeBranch(myBranch); 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            fruitRenderer.enabled = false;
            //Debug.Log("Niszczę jabłko");
            Destroy(this.gameObject);
            Instantiate(fruitParticleSystem, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
