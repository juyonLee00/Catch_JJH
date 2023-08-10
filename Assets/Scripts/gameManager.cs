using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    float time;

    public GameObject card;
    public GameObject retryBtn;

    public GameObject firstCard;
    public GameObject secondCard;

    int pairNum = 0;

    public static gameManager I;

    void Awake()
    {
        I = this;    
    }
    void Start()
    {
        string[] cardIdx = new string[] {"jy0", "jy1", "jy2", "jy3", "jb0", "jb1", "jb2", "jb3", "hr0", "hr1", "hr2", "hr3", "rt", "rt", "rt", "rt"};
        cardIdx = cardIdx.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
        for (int i =0; i<16; i++)  
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string cardName = cardIdx[i];
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
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

        if(pairNum == 4)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        string firstCardName = firstCardImage.Substring(0, 2);
        string secondCardName = secondCardImage.Substring(0, 2);

        if(firstCardName == "rt" || secondCardName == "rt")
        {
            wrongCard();
        }

        else if(firstCardName == secondCardName)
        {
            correctCard();
        }

        else
        {
            wrongCard();
        }
        firstCard = null;
        secondCard = null;
    }

    void wrongCard()
    {
        firstCard.GetComponent<card>().closeCard();
        secondCard.GetComponent<card>().closeCard();
    }

    void correctCard()
    {
        firstCard.GetComponent<card>().destroyCard();
        secondCard.GetComponent<card>().destroyCard();
        pairNum += 1;
    }
}

