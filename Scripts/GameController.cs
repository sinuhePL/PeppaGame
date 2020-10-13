using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public Transform[] fruits;
    public static int fruitToCollect;  // id owocu który należy zebrać
    public static int numberToCollect;    // liczba owoców do zebrania
    public Canvas uiCanvas;
    public float fruitRate;
    private float nextFruitTime;
    private bool[] blockedBranches;
    private bool stopWorking;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fruitToCollect = -1;
        numberToCollect = 0;
        stopWorking = false;
        Random.InitState(System.Environment.TickCount);
        blockedBranches = new bool[6]; // inicjalizuje tablicę zablokowanych gałęzi
        for (int i = 0; i < 6; i++) blockedBranches[i] = false; // wszystkie gałęzie są na początku odblokowane
        Restart();
        nextFruitTime = 0.0f;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Start () {
        /*Random.InitState(System.Environment.TickCount);
        blockedBranches = new bool[6]; // inicjalizuje tablicę zablokowanych gałęzi
        for (int i = 0; i < 6; i++) blockedBranches[i] = false; // wszystkie gałęzie są na początku odblokowane
        Restart();*/
    }
	
	// Update is called once per frame
	void Update () {
        Transform tempObj;
        int fruitNumber, branchNumber;
        if (!stopWorking)
        {
            if (Time.time > nextFruitTime)
            {
                nextFruitTime = Time.time + fruitRate;
                fruitNumber = Random.Range(0, fruits.Length+1);
                if (fruitNumber >= fruits.Length) fruitNumber = fruitToCollect;
                branchNumber = Random.Range(0, 6);
                //Debug.Log("Wylosowana gałąź: " +branchNumber);
                if (branchNumber == 0 && !blockedBranches[0])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(-1.371373f, 4.272314f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 0, fruitNumber);
                    blockedBranches[0] = true;
                    return;
                }
                if (branchNumber == 1  && !blockedBranches[1])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(2.07f, 2.776212f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 1, fruitNumber);
                    blockedBranches[1] = true;
                    return;
                }
                if (branchNumber == 2 && !blockedBranches[2])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(8.234626f, 4.332735f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 2, fruitNumber);
                    blockedBranches[2] = true;
                    return;
                }
                if (branchNumber == 3 && !blockedBranches[3])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(-9.24f, 4.28f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 3, fruitNumber);
                    blockedBranches[3] = true;
                    return;
                }
                if (branchNumber == 4 && !blockedBranches[4])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(11.04f, 1.37f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 4, fruitNumber);
                    blockedBranches[4] = true;
                    return;
                }
                if (branchNumber == 5 && !blockedBranches[5])
                {
                    tempObj = Instantiate(fruits[fruitNumber], new Vector3(-5.77f, 5.39f, 0.0f), Quaternion.identity);
                    tempObj.GetComponent<FruitController>().SetGameMaster(gameObject, 5, fruitNumber);
                    blockedBranches[5] = true;
                    return;
                }
            }
        }
    }

    public void FreeBranch(int i)
    {
        blockedBranches[i] = false;
    }

    public void Restart()
    {
        numberToCollect++;
        fruitToCollect = Random.Range(0, fruits.Length);
        uiCanvas.GetComponent<uiCanvasController>().SetFruitNumber(numberToCollect, fruitToCollect);
    }

    public void StopWork()
    {
        stopWorking = true;
        uiCanvas.GetComponent<uiCanvasController>().ShowEnd();
    }
}
