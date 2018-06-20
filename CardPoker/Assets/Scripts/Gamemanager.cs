using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] X_Cubes = null;
    [SerializeField]
    private GameObject[] Z_Cubes = null;
    [SerializeField]
    private GameObject[] C = null;

    public class Deck
    {
        List<int> CardDeck = new List<int>(new int[] { 0, 2, 4, 6, 8 });

        public Deck(bool even)
        {
            if (even == false)
            {
                for (int i = 0; i < CardDeck.Count; i++)
                    CardDeck[i]++;
            }

            shuffle(ref CardDeck);
        }

        void shuffle(ref List<int> shuffleList)
        {
            for (int i = 0; i < CardDeck.Count-1; i++)
            {
                int shuffle = Random.Range(0, CardDeck.Count -2 - i);
                int temp = shuffleList[CardDeck.Count -1 - i];

                shuffleList[CardDeck.Count-1 - i] = shuffleList[shuffle];
                shuffleList[shuffle] = temp;
            }
        }

        public List<int> getCards()
        {
            return CardDeck;
        }
    }
    
    int nCube = 0;

    int nQcard, nWcard, nEcard, nRcard;
    
    public bool xState, zState;
    bool fState;
    bool state;

    Deck QDeck = null;
    Deck WDeck = null;
    Deck EDeck = null;
    Deck RDeck = null;

    // Use this for initialization
    void Start()
    {
        QDeck = new Deck(true);
        WDeck = new Deck(true);
        EDeck = new Deck(false);
        RDeck = new Deck(false);

        nQcard = 0;
        nWcard = 0;
        nEcard = 0;
        nRcard = 0;

        fState = true;
        state = false;

        xState = false;
        zState = false;

    }

    public void ChangeLastItemInQDeck(Component image, Sprite[] cardImage)
    {
        if (nQcard == 5)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[QDeck.getCards()[nQcard] / 2];

    }

    public void ChangeLastItemInWDeck(Component image, Sprite[] cardImage)
    {
        if (nWcard == 5)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[WDeck.getCards()[nWcard] / 2];
    }

    public void ChangeLastItemInEDeck(Component image, Sprite[] cardImage)
    {
        if (nEcard == 5)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[EDeck.getCards()[nEcard] / 2];
    }

    public void ChangeLastItemInRDeck(Component image, Sprite[] cardImage)
    {
        if (nRcard == 5)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[RDeck.getCards()[nRcard] / 2];
    }

    bool isColliding(float pos1, float pos2)
    {
        if (pos1 > pos2 - 1.0f && pos1 < pos2 + 1.0f)
        {
            return true;
        }
        else
            return false;
    }

    void selectCard(GameObject[] Cube_Type, ref int Card_num, List<int> Deck_Type)
    {
        C[nCube] = GameObject.Instantiate(Cube_Type[Deck_Type[Card_num]]);

        if (nCube == 0)
            C[nCube].transform.localPosition = new Vector3(0, 1.0f, 0);
        else
        {
            C[nCube].transform.localPosition = C[nCube-1].transform.position;
            C[nCube].transform.Translate(0, 0.5f, 0);
        }
        C[nCube].transform.localRotation = Quaternion.identity;

        nCube++;
        Card_num++;
        state = false;
    }
    
    void Ingame()
    {
        if (fState == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                fState = false;
                state = true;
            }
        }

        else
        {
            if (state)
            {
                if (Input.GetKeyDown(KeyCode.Q) && nQcard < 5 )
                {
                    selectCard(X_Cubes, ref nQcard, QDeck.getCards());
                }

                else if (Input.GetKeyDown(KeyCode.W) && nWcard < 5)
                {
                    selectCard(Z_Cubes, ref nWcard, WDeck.getCards());
                }

                else if (Input.GetKeyDown(KeyCode.E) && nEcard < 5)
                {
                    selectCard(X_Cubes, ref nEcard, EDeck.getCards());
                }

                else if (Input.GetKeyDown(KeyCode.R) && nRcard < 5)
                {
                    selectCard(Z_Cubes, ref nRcard, RDeck.getCards());
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && fState == false)
                {
                    if (xState == true && zState == true)
                    {
                        state = true;
                    }
                }
            }

            if (nQcard > 4) nQcard = 5;

            if (nWcard > 4) nWcard = 5;

            if (nEcard > 4) nEcard = 5;

            if (nRcard > 4) nRcard = 5;
        }
    }

    void Collision_Cube()
    {
        if (nCube <= 1)
        {
            xState = true;
            zState = true;
        }

        else
        {
            xState = isColliding(C[nCube - 1].transform.position.x, C[nCube - 2].transform.position.x);
            zState = isColliding(C[nCube - 1].transform.position.z, C[nCube - 2].transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ingame();

        Collision_Cube();
    }
}