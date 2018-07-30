using System;
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

        public bool getxState()
        {
            if (Cubes.Count <= 1)
                return true;
            else
                return xstate;
        }
        public bool getzState()
        {
            if (Cubes.Count <= 1)
                return true;
            else
                return zstate;
        }

        private List<GameObject> Cubes = new List<GameObject>();

        public CardManager()
        {
        }

        public void InputCard(string Type, int Num, string Color)
        {
            Cards.Add(new Card { CardType = Type, CardNum = Num, CardColor = Color});

            //Debug.Log("Type : " + Cards[Cards.Count-1].CardType);
            //Debug.Log("Num : " + Cards[Cards.Count-1].CardNum);
        }

        public void CreateCube(GameObject[] Cube_Type1, GameObject[] Cube_Type2)
        {
            if (Cards[Cards.Count - 1].CardType == "Q" || Cards[Cards.Count - 1].CardType == "E")
            {
                Cubes.Add(GameObject.Instantiate(Cube_Type1[Cards[Cards.Count - 1].CardNum]));
            }
            else
            {
                Cubes.Add(GameObject.Instantiate(Cube_Type2[Cards[Cards.Count - 1].CardNum]));
            }

            if (Cubes.Count-1 == 0)
            {
                Debug.Log("a" + Cubes.Count);
                Cubes[Cubes.Count-1].transform.localPosition = new Vector3(0, 1.0f, 0);
            }
            else
            {
                Cubes[Cubes.Count-1].transform.localPosition = Cubes[Cubes.Count - 2].transform.position;
                Cubes[Cubes.Count - 1].transform.localScale = Cubes[Cubes.Count - 2].transform.localScale;
                Cubes[Cubes.Count-1].transform.Translate(0, 0.5f, 0);
            }

            Cubes[Cubes.Count-1].transform.localRotation = Quaternion.identity;
        }

        public void ResizeCube()
        {
            if (Cubes.Count>1)
            {
                float hangover = Cubes[Cubes.Count - 1].transform.position.x - Cubes[Cubes.Count - 2].transform.position.x;

                float direction;

                if (hangover > 0)
                    direction = 1.0f;
                else
                    direction = -1.0f;

            SplitCubeOnX(hangover, direction);

            }
        }

        /*
         * transform.position.z             :   Cubes[Cubes.Count - 2].transform.position.x
         * LastCube.transform.position.z    :   Cubes[Cubes.Count - 1].transform.position.x
         */

        private void SplitCubeOnX(float hangover, float direction)
        {
            float newXSize = Cubes[Cubes.Count - 2].transform.localScale.x - Mathf.Abs(hangover);
            float fallingBlockSize = Cubes[Cubes.Count - 1].transform.localScale.x - newXSize;

            float newXPosition = Cubes[Cubes.Count - 2].transform.position.x + (hangover / 2);

            Cubes[Cubes.Count - 1].transform.localScale = new Vector3(newXSize, Cubes[Cubes.Count - 1].transform.localScale.y, Cubes[Cubes.Count - 1].transform.localScale.z);
            Cubes[Cubes.Count - 1].transform.position = new Vector3(newXPosition, Cubes[Cubes.Count - 1].transform.position.y, Cubes[Cubes.Count - 1].transform.position.z);

            float cubeEdge = Cubes[Cubes.Count - 1].transform.position.x + (newXSize / 2.0f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2.0f * direction;

            //SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
        }

        public void CheckCollision()
        {
            if (Cubes.Count > 1)
            {
                if (Cubes[Cubes.Count - 1].transform.position.x > Cubes[Cubes.Count - 2].transform.position.x - Cubes[Cubes.Count-1].transform.localScale.x && Cubes[Cubes.Count - 1].transform.position.x < Cubes[Cubes.Count - 2].transform.position.x + Cubes[Cubes.Count - 1].transform.localScale.x)
                {
                    xstate = true;
                    Debug.Log("true");
                }
                else
                {
                    xstate = false;
                    Debug.Log("false");
                }
          
                if (Cubes[Cubes.Count - 1].transform.position.z > Cubes[Cubes.Count - 2].transform.position.z - Cubes[Cubes.Count - 1].transform.localScale.x && Cubes[Cubes.Count - 1].transform.position.z < Cubes[Cubes.Count - 2].transform.position.z + Cubes[Cubes.Count - 1].transform.localScale.x)
                {
                    zstate = true;
                }

                else
                {
                    zstate = false;
                }
            }
        }

        public void CubeDestroy()
        {
            ////0번째 카드 1장을 없앨때
            //if (Cubes.Count > 5)
            //{
            //    Cards.RemoveAt(0);

            //    Destroy(Cubes[0]);

            //    Cubes.RemoveAt(0);
            //}

            if (Cubes.Count > 1)
            {
                Debug.Log(Cards.Count);
                if (Cards[Cards.Count - 1].CardColor == Cards[Cards.Count - 2].CardColor)           ///////////////////// 같은 색 일때
                {
                    if (Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == 1 || Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == -1)
                    {
                        int temp = Cards.Count;
                        Cards.RemoveRange(temp - 2, 2);

                        Destroy(Cubes[temp - 2],0.1f);
                        Destroy(Cubes[temp - 1],0.1f);

                        Cubes.RemoveRange(temp - 2, 2);
                    }
                }
            }

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
        public string       CardColor;
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
                int shuffle = UnityEngine.Random.Range(0, CardDeck.Count -2 - i);
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
    
    public bool xState, zState;
    bool fState;
    bool state;
    bool test;

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
        test = true;
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
                    CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                    CMG.CreateCube(X_Cubes, Z_Cubes);
                    QDeck.selectCard();
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.W) && WDeck.getCountCard() > 0)
                {
                    CMG.InputCard("W", WDeck.getCards()[0], "Red");
                    CMG.CreateCube(X_Cubes, Z_Cubes);
                    WDeck.selectCard();
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.E) && EDeck.getCountCard() > 0)
                {
                    CMG.InputCard("E", EDeck.getCards()[0], "Black");
                    CMG.CreateCube(X_Cubes, Z_Cubes);
                    EDeck.selectCard();
                    state = false;
                }

                else if (Input.GetKeyDown(KeyCode.R) && RDeck.getCountCard() > 0)
                {
                    CMG.InputCard("R", RDeck.getCards()[0], "Red");
                    CMG.CreateCube(X_Cubes, Z_Cubes);
                    RDeck.selectCard();
                    state = false;
                }
                if(test == true)
                    CMG.ResizeCube();
                test = false;
            }

            else
            {
                //Debug.Log("nCube : " + nCube);
                CMG.CheckCollision();
                //Debug.Log("b");

                if (Input.GetKeyDown(KeyCode.Space) && fState == false)
                {
                    if (xState == true && zState == true)
                    {
                        state = true;
                        test = true;
                    }
                    CMG.CubeDestroy();
                }
            }
            
        }
    }

    void Collision_Cube()
    {
        xState = CMG.getxState();
        zState = CMG.getzState();
    }

    // Update is called once per frame
    void Update()
    {
        Ingame();

        Collision_Cube();
    }
}