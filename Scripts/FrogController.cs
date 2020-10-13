using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Animator frogAnimator;
    private AudioSource frogAudioSource;
    private bool facingLeft = true;
    public float frogVelocity = 0.5f;
    private bool isJump = false;

    // Use this for initialization
    void Start()
    {
        Random.InitState(35);
        frogAnimator = GetComponent<Animator>();
        frogAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isJump)
        {
            if(facingLeft) transform.Translate(new Vector3(-frogVelocity, 0, 0) * Time.deltaTime, Space.World);
            else transform.Translate(new Vector3(frogVelocity, 0, 0) * Time.deltaTime, Space.World);
        }
        if (transform.position.x > 14.0f) Flip();
        if (transform.position.x < -14.0f) Flip();
    }

    public void EndIdle()
    {
        if (Random.value < 0.1f)
        {
            frogAnimator.SetBool("isCroak", true);
            frogAudioSource.Play();
            return;
        }
        if (Random.value > 0.3f)
        {
            frogAnimator.SetBool("isJump", true);
            isJump = true;
            return;
        }
        if (Random.value > 0.45f && Random.value < 0.55f) Flip();
    }

    public void EndJump()
    {
        frogAnimator.SetBool("isJump", false);
        isJump = false;
    }

    public void EndCroak()
    {
        frogAnimator.SetBool("isCroak", false);
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
