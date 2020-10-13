using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiCanvasController : MonoBehaviour {

    private int fruitNumber;
    private int fruitToCollect;
    private int collectedFruitNumber;
    private Image[] fruitImages;
    public Image[] fruitPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddFruit()
    {
        if (collectedFruitNumber < fruitNumber)
        {
            collectedFruitNumber++;

        }
    }

    public void SetFruitNumber(int i, int f)   // ustawia ilość i rodzaj owoców do zebrania
    {
        int j,x;
        if (fruitImages != null)
        {
            x = fruitImages.Length;
        }
        else
        {
            x = 0;
        }
        for (j = 0; j < x; j++)
        {
            Destroy(fruitImages[j]);
        }
        fruitToCollect = f;
        if (i > 0 && i < 5) fruitNumber = i;
        else Debug.Log("Zła liczba owoców do zebrania:" + i);
        fruitImages = new Image[fruitNumber];
        for (j = 0; j < fruitNumber; j++)
        {
            fruitImages[j] = Instantiate(fruitPrefab[fruitToCollect]) as Image;
            fruitImages[j].transform.SetParent(gameObject.transform, false);
            fruitImages[j].GetComponent<RectTransform>().position += new Vector3(j*50, 0, 0);
            
        }
        Debug.Log("Ustawiłem - fruitNumber:" + fruitNumber);
    }

    public void CatchFruit()
    {
        int i;
        Debug.Log("Złapałem - fruitNumber:"+fruitNumber);
        for(i=0; i < fruitNumber; i++)
        {
            if (fruitImages[i].color == Color.black)
            {
                fruitImages[i].color = Color.white;
                return;
            }
        }
    }

    public void LostFruit()
    {
        int i;
        for (i = fruitNumber-1; i >= 0; i--)
        {
            if (fruitImages[i].color == Color.white)
            {
                fruitImages[i].color = Color.black;
                return;
            }
        }
    }

    public void ShowEnd()
    {
        GameObject tempGO;
        tempGO = GameObject.FindGameObjectWithTag("EndText");
        tempGO.GetComponent<Text>().text = "Koniec";
    }
}
