using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour {

    private AudioSource fruitAudioSource;
    // Use this for initialization
    void Start () {
        fruitAudioSource = GetComponent<AudioSource>();
        fruitAudioSource.Play();
        Destroy(this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
