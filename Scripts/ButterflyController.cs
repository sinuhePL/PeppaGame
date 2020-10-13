using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour {

    public float myVelocity = 0.5f;
    private int direction;
	// Use this for initialization
	void Start () {
        direction = 1;  // 1 - lewy górny róg
                        // 2 - prawy górny róg
                        // 3 - prawy dolny róg
                        // 4 - lewy dolny róg
        Random.InitState(33);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 myScale;

        myScale = transform.localScale;
        if (Random.value < 0.01f)
        {
            if (direction == 1)
            {
                direction = 4;
                myScale.y *= -1;
                transform.localScale = myScale;
                return;
            }
            if (direction == 3)
            {
                direction = 2;
                myScale.y *= -1;
                transform.localScale = myScale;
                return;
            }
            if (direction == 2)
            {
                direction = 1;
                myScale.x *= -1;
                transform.localScale = myScale;
                return;
            }
            if (direction == 4)
            {
                direction = 3;
                myScale.x *= -1;
                transform.localScale = myScale;
                return;
            }
        }
        if (transform.position.y >= 6.0f)   // górna granica
        {
            if (direction == 1) direction = 4;
            if (direction == 2) direction = 3;
            myScale.y *= -1;
            transform.localScale = myScale;
        }
        if (transform.position.y <= -3.0f )   // dolna granica
        {
            if (direction == 3) direction = 2;
            if (direction == 4) direction = 1;
            myScale.y *= -1;
            transform.localScale = myScale;
        }
        if (transform.position.x >= 13.0f )   // prawa granica
        {
            if (direction == 2) direction = 1;
            if (direction == 3) direction = 4;
            myScale.x *= -1;
            transform.localScale = myScale;
        }
        if (transform.position.x <= -13.0f)   // lewa granica
        {
            if (direction == 1) direction = 2;
            if (direction == 4) direction = 3;
            myScale.x *= -1;
            transform.localScale = myScale;
        }
        if (direction == 1) transform.Translate(new Vector3(-myVelocity, myVelocity,0) * Time.deltaTime, Space.World);
        else if (direction == 2) transform.Translate(new Vector3(myVelocity, myVelocity,0) * Time.deltaTime, Space.World);
        else if (direction == 3) transform.Translate(new Vector3(myVelocity, -myVelocity,0) * Time.deltaTime, Space.World);
        else if (direction == 4) transform.Translate(new Vector3(-myVelocity, -myVelocity,0) * Time.deltaTime, Space.World);
    }
}
