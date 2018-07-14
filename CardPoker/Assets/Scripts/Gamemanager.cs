using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] X_Cubes = null;
    [SerializeField]
    public GameObject[] Z_Cubes = null;

    public class CardManager
    {
        List<Card> Cards = new List<Card>();

        bool xstate;
        bool zstate;

        public bool getxState() { return xstate; }
        public bool getzState() { return zstate; }

        private List<GameObject> Cubes = new List<GameObject>();

        public CardManager()
        {
        }

        public void InputCard(string Type, int Num)
        {
            Cards.Add(new Card { CardType = Type, CardNum = Num });

            Debug.Log("Type : " + Cards[Cards.Count-1].CardType);
            Debug.Log("Num : " + Cards[Cards.Count-1].CardNum);
        }

        public void CreateCube(GameObject[] Cube_Type1, GameObject[] Cube_Type2, int nCube)
        {
            if (Cards[Cards.Count - 1].CardType == "Q" || Cards[Cards.Count - 1].CardType == "E")
            {
                Cubes.Add(GameObject.Instantiate(Cube_Type1[Cards[Cards.Count - 1].CardNum]));
            }
            else
            {
                Cubes.Add(GameObject.Instantiate(Cube_Type2[Cards[Cards.Count - 1].CardNum]));
            }

            if (nCube == 0)
                Cubes[nCube].transform.localPosition = new Vector3(0, 1.0f, 0);
            else
            {
                Cubes[nCube].transform.localPosition = Cubes[nCube - 1].transform.position;
                Cubes[nCube].transform.Translate(0, 0.5f, 0);
            }

            Cubes[nCube].transform.localRotation = Quaternion.identity;
        }

        public void CheckCollision(int nCube)
        {
            if (nCube > 1)
            {
                if (Cubes[nCube - 1].transform.position.x > Cubes[nCube - 2].transform.position.x - 1.0f && Cubes[nCube - 1].transform.position.x < Cubes[nCube - 2].transform.position.x + 1.0f)
                {
                    xstate = true;
                }
                else
                {
                    xstate = false;
                }
          
                if (Cubes[nCube - 1].transform.position.z > Cubes[nCube - 2].transform.position.z - 1.0f && Cubes[nCube - 1].transform.position.z < Cubes[nCube - 2].transform.position.z + 1.0f)
                {
                    zstate = true;
                }

                else
                {
                    zstate = false;
                }
            }
        }

        public void Destory()
        {
            Cards.RemoveAt(0);
            
            Destroy(Cubes[0]);

            Cubes.RemoveAt(0);
        }
    }

    /*
    enum CardType{
        Q = 1,
        W = 2,
        E = 3,
        R = 4
    };
    */
    
    public class Card
    {
        public string       CardType;   // Q = 1, W = 2, E = 3, R = 4
        public int          CardNum;
        
    }

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

        public void selectCard()
        {
            //Debug.Log(CardDeck.Count);
            CardDeck.RemoveAt(0);
        }

        public int getCountCard()
        {
            return CardDeck.Count;
        }

        public int getLastCard()
        {
            return CardDeck[0];
        }
    }
    
    public int nCube = 0;

    public bool xState, zState;
    bool fState;
    bool state;

    Deck QDeck = null;
    Deck WDeck = null;
    Deck EDeck = null;
    Deck RDeck = null;

    CardManager CMG = null;

    // Use this for initialization
    void Start()
    {
        QDeck = new Deck(true);
        WDeck = new Deck(true);
        EDeck = new Deck(false);
        RDeck = new Deck(false);
        CMG = new CardManager();

        fState = true;
        state = false;

        xState = false;
        zState = false;
    }

    public void ChangeLastItemInQDeck(Component image, Sprite[] cardImage)
    {
        if (QDeck.getCountCard() < 1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
        {
            image.GetComponent<Image>().overrideSprite = cardImage[QDeck.getLastCard() / 2];
        }
    }

    public void ChangeLastItemInWDeck(Component image, Sprite[] cardImage)
    {
        if (WDeck.getCountCard() < 1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[WDeck.getLastCard() / 2];
    }

    public void ChangeLastItemInEDeck(Component image, Sprite[] cardImage)
    {
        if (EDeck.getCountCard() < 1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[EDeck.getLastCard() / 2];
    }

    public void ChangeLastItemInRDeck(Component image, Sprite[] cardImage)
    {
        if (RDeck.getCountCard() < 1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[RDeck.getLastCard() / 2];
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

        else   // In game
        {
            if (state == true)
            {
                if (Input.GetKeyDown(KeyCode.Q) && QDeck.getCountCard() > 0)
                {
                    CMG.InputCard("Q", QDeck.getLastCard());
                    CMG.CreateCube(X_Cubes, Z_Cubes, nCube);
                    QDeck.selectCard();
                    nCube++;
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.W) && WDeck.getCountCard() > 0)
                {
                    CMG.InputCard("W", WDeck.getCards()[0]);
                    CMG.CreateCube(X_Cubes, Z_Cubes, nCube);
                    WDeck.selectCard();
                    nCube++;
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.E) && EDeck.getCountCard() > 0)
                {
                    CMG.InputCard("E", EDeck.getCards()[0]);
                    CMG.CreateCube(X_Cubes, Z_Cubes, nCube);
                    EDeck.selectCard();
                    nCube++;
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.R) && RDeck.getCountCard() > 0)
                {
                    CMG.InputCard("R", RDeck.getCards()[0]);
                    CMG.CreateCube(X_Cubes, Z_Cubes, nCube);
                    RDeck.selectCard();
                    nCube++;
                    state = false;
                }
            }

            else
            {
                //Debug.Log("nCube : " + nCube);
                CMG.CheckCollision(nCube);
                //Debug.Log("b");

                if (Input.GetKeyDown(KeyCode.Space) && fState == false)
                {
                    if (xState == true && zState == true)
                    {
                        state = true;
                    }
                }
            }

            if(nCube > 5)
            {
                nCube--;
                CMG.Destory();
            }
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
            xState = CMG.getxState();
            zState = CMG.getzState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ingame();

        Collision_Cube();
    }
}