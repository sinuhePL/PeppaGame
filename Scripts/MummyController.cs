using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour {

    private AudioSource mummyAudioSource;
    private Animator mummyAnimator;
    public AudioClip startTalk;
    public AudioClip endTalkSound;

    // Use this for initialization
    void Start ()
    {
        mummyAudioSource = GetComponent<AudioSource>();
        mummyAnimator = GetComponent<Animator>();
        mummyAudioSource.PlayOneShot(startTalk, 1.4f);
        mummyAnimator.SetBool("isTalk", true);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CongratulatePeppa()
    {
        mummyAnimator.SetBool("isTalk", true);
        mummyAudioSource.Play();
    }

    public void EndTalk()
    {
        mummyAnimator.SetBool("isTalk", false);
    }

    public void EndLongTalk()
    {    
        
        GameObject tempPeppa;
        tempPeppa = GameObject.FindGameObjectWithTag("Peppa");
        tempPeppa.SendMessage("StartTalk");
    }

    public void GiveCake()
    {
        Debug.Log("Mama mówi");
        mummyAnimator.SetBool("isEnd", true);
        mummyAudioSource.PlayOneShot(endTalkSound, 1.4f);
    }

    public void EndLongTalk2()
    {

        GameObject tempPeppa;
        tempPeppa = GameObject.FindGameObjectWithTag("Peppa");
        tempPeppa.SendMessage("StartTalk2");
    }
}
