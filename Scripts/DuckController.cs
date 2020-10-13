using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour {
    public float duckVelocity = 0.5f;
    private Animator duckAnimator;
    private bool isWalking;
    private AudioSource duckAudioSource;

	// Use this for initialization
	void Start () {
        Random.InitState(26);
        duckAnimator = GetComponent<Animator>();
        isWalking = true;
        duckAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(isWalking) transform.Translate(new Vector3(duckVelocity, 0, 0) * Time.deltaTime, Space.World);
        if (transform.position.x > 14.0f && Random.value < 0.005f) transform.position = new Vector3(-14.0f, -4.22f, 0.2988281f);
    }

    public void MarkQuackEnd()
    {
        isWalking = true;
        duckAnimator.SetFloat("duckSpeed", 0.5f);
    }

    public void MarkWalkEnd()
    {
        if (Random.value < 0.1f)
        {
            isWalking = false;
            duckAnimator.SetFloat("duckSpeed", 0.0f);
            duckAudioSource.Play();
        }
    }
}
