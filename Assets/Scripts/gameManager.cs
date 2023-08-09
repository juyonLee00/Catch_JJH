using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    float time;
    public GameObject card;
    public GameObject retryBtn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i<16; i++)  
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);
        }
    }

    void Update()
    {
        if (time < 60.0f)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }

        else
        {
            retryBtn.SetActive(true);
        }
        
    }
}
