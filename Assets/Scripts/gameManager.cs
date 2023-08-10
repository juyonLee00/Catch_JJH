using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public float time;

    public GameObject card;
    public GameObject retryBtn;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip match;
    public AudioSource audioSource;

    int pairNum = 0;
    public int openCardNum = 0;
    public float openCardTime = 0.0f;

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

        if(pairNum == 6)
        {
            SceneManager.LoadScene("EndScene");
        }

        if(openCardNum == 1 && (time - openCardTime) > 5)
        {
            incorrectCard(firstCard);
            InitializeCurrentOpenCard();
            firstCard = null;
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
            incorrectCard(firstCard);
            incorrectCard(secondCard);
            InitializeCurrentOpenCard();
        }

        else if(firstCardName == secondCardName)
        {
            audioSource.PlayOneShot(match);

            correctCard(firstCard);
            correctCard(secondCard);
            pairNum += 1;
            InitializeCurrentOpenCard();
        }

        else
        {
            incorrectCard(firstCard);
            incorrectCard(secondCard);
            InitializeCurrentOpenCard();
        }
        firstCard = null;
        secondCard = null;
    }

    void incorrectCard(GameObject card)
    {
        card.GetComponent<card>().closeCard();
    }

    void correctCard(GameObject card)
    {
        card.GetComponent<card>().destroyCard();
    }

    void InitializeCurrentOpenCard()
    {
        openCardNum = 0;
        openCardTime = 0.0f;
        
    }
}
