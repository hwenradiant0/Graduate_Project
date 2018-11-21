using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;
using TMPro;

using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] X_Cubes = null;
    [SerializeField]
    public GameObject[] Z_Cubes = null;

    public CameraShake   camerashaker;
    
    Deck QDeck = null;
    Deck WDeck = null;
    Deck EDeck = null;
    Deck RDeck = null;

    CardManager CMG = null;

    Countdown countdown = null;

    private bool Keydownable;
    private bool processcoroutine;
    private bool reSheffle;

    public int numCube = 0;

    bool startgame;

    public bool tutorial;
    
    public void OffTutorial() { tutorial = false; startgame = true; ResetScore(); }
    public void ResetScore() { Score.scoreValue = 0; MultipleScore.multipleValue = 1; combo = true;}
    public void GoTime() { Time.timeScale = 1.0f; }

    public bool xState, yState, zState;

    public static bool combo;

    bool fState;
    bool state;
    bool recentCard;
    public bool AccelerateCube = false;
    public bool SlowCube = false;
    public void stateInit() { AccelerateCube = false; SlowCube = false; }

    // Use this for initialization
    void Start()
    {
        if (tutorial == true)
        {
            QDeck = new Deck(true, 1);
            WDeck = new Deck(true, 2);
            EDeck = new Deck(false, 3);
            RDeck = new Deck(false, 0);
        }
        else
        {
            QDeck = new Deck(true, 0);
            WDeck = new Deck(true, 0);
            EDeck = new Deck(false, 0);
            RDeck = new Deck(false, 0);
        }

        CMG = new CardManager();
        countdown = new Countdown();

        fState = true;
        state = false;

        xState = false;
        zState = false;

        Keydownable = true;

        processcoroutine = false;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = true;
            ResetScore();
        }
        else
            OffTutorial();

        FloatingTextController.Initialize();
        reSheffle = true;
    }

    public class Deck
    {
        List<int> CardDeck = new List<int>(new int[] { 0, 2, 4, 6, 8 });

        public Deck(bool even, int num)
        {
            if (even == false)
            {
                for (int i = 0; i < CardDeck.Count; i++)
                    CardDeck[i]++;
            }
            shuffle(ref CardDeck, num);
        }

        void shuffle(ref List<int> shuffleList, int num)
        {
            if (num == 0)
            {
                for (int i = 0; i < CardDeck.Count - 1; i++)
                {
                    int shuffle = UnityEngine.Random.Range(0, CardDeck.Count - 2 - i);
                    int temp = shuffleList[CardDeck.Count - 1 - i];

                    shuffleList[CardDeck.Count - 1 - i] = shuffleList[shuffle];
                    shuffleList[shuffle] = temp;
                }
            }

            else
            {
                switch(num)
                {
                    case 1:
                        shuffleList[0] = 6;
                        shuffleList[1] = 2;
                        shuffleList[2] = 8;
                        shuffleList[3] = 0;
                        shuffleList[4] = 4;
                        break;
                    case 2:
                        shuffleList[0] = 4;
                        shuffleList[1] = 8;
                        shuffleList[2] = 2;
                        shuffleList[3] = 0;
                        shuffleList[4] = 6;
                        break;
                    case 3:
                        for (int i = 0; i < CardDeck.Count - 1; i++)
                        {
                            int shuffle = UnityEngine.Random.Range(1, CardDeck.Count - 2 - i);
                            int temp = shuffleList[CardDeck.Count - 1 - i];

                            shuffleList[CardDeck.Count - 1 - i] = shuffleList[shuffle];
                            shuffleList[shuffle] = temp;
                        }
                        break;
                }
            }
        }

        public List<int> getCards()
        {
            return CardDeck;
        }

        public void selectCard()
        {
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
    
    
    public void ChangeLastItemInQDeck(Component image, Sprite[] cardImage)
    {
        if (QDeck.getCountCard() < 1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
        {
            image.GetComponent<Image>().overrideSprite = cardImage[QDeck.getLastCard() / 2];
        }
    }

    public void Resize()
    {
        CMG.CheckCollision();

        if (fState == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (xState == true && zState == true)
            {
                if (Keydownable == true)
                {
                    if (recentCard == true)
                    {
                        Cube1.CurrentCube.Stop();
                    }
                    else
                    {
                        Cube2.CurrentCube.Stop();
                    }

                    state = true;
                    CMG.ResizeCube(recentCard);
                    CMG.scoreCheck();
                    if (tutorial == false)
                    {
                        if (CardManager.numItem == 1)
                        {
                            Debug.Log("a");
                            StartCoroutine(Blinder());
                        }
                        else if (CardManager.numItem == 2)
                        {
                            AccelerateCube = true;
                            SlowCube = false;
                        }
                        else if (CardManager.numItem == 3)
                        {
                            AccelerateCube = false;
                            SlowCube = true;
                        }
                    }
                }
            }

            else
            {
                if (processcoroutine == false)
                {
                    if (tutorial == true)
                    {
                        GameObject.Find("Tutorial").transform.Find("Message3").gameObject.SetActive(true);
                    }
                    else
                    {
                        if(CardManager.numItem == 4)
                        {
                            Debug.Log("now");
                        }
                        else
                        {
                            Debug.Log("no!");
                            Countdown.countdown.decreaseTime(10.0f);
                            MultipleScore.multipleValue = 1;
                            combo = false;
                            StartCoroutine(OnUpdateRoutine());
                            StartCoroutine(camerashaker.Shake(0.15f, 0.5f));
                        }
                    }
                }
            }
        }
    }


    IEnumerator OnUpdateRoutine()
    {
        processcoroutine = true;

        Keydownable = false;

        GameObject.Find("UI").transform.Find("Filled").transform.gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("Circle Pie").transform.gameObject.SetActive(true);
        for (float i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
        }

        GameObject.Find("UI").transform.Find("Circle Pie").transform.gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("Filled").transform.gameObject.SetActive(true);
        Keydownable = true;
        processcoroutine = false;
    }

    IEnumerator Blinder()
    {
        GameObject.Find("UI").transform.Find("Blinder").transform.gameObject.SetActive(true);
        for (float i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        GameObject.Find("UI").transform.Find("Blinder").transform.gameObject.SetActive(false);

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

    void Tutorial()
    {
        if (GameObject.Find("Tutorial").transform.Find("FirstTutorial").gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject.Find("FirstTutorial").SetActive(false);

                GameObject.Find("Tutorial").transform.Find("SecondTutorial").gameObject.SetActive(true);
                fState = false;
                state = true;

                Countdown.countdown.startcountdown();

                Time.timeScale = 0.0f;
            }
        }

        if (GameObject.Find("Tutorial").transform.Find("SecondTutorial").gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Find("SecondTutorial").SetActive(false);
                GameObject.Find("Tutorial").transform.Find("ThirdTutorial").gameObject.SetActive(true);
            }
        }

        if (state == true)
        {
            if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Tutorial").transform.Find("ThirdTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("ThirdTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Tutorial").transform.Find("FourthTutorial").gameObject.SetActive(true);
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Tutorial").transform.Find("FifthTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("FifthTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Tutorial").transform.Find("SixthTutorial").gameObject.SetActive(true);
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Tutorial").transform.Find("SeventhTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("SeventhTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Tutorial").transform.Find("EighthTutorial").gameObject.SetActive(true);
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
            }

            else if (GameObject.Find("Tutorial").transform.Find("NinethTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("NinethTutorial").SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("TenthTutorial").gameObject.SetActive(true);
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("TenthTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("TenthTutorial").SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("ItemTutorial").gameObject.SetActive(true);
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("FifteenthTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("FifteenthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("SixteenthTutorial").gameObject.SetActive(true);
                }
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Tutorial").transform.Find("EleventhTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("EleventhTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Tutorial").transform.Find("TwelvethTutorial").gameObject.SetActive(true);
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
            }

            else if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("Tutorial").transform.Find("ThirteenthTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("ThirteenthTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("E", EDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                EDeck.selectCard();
                state = false;
                recentCard = true;
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
                GameObject.Find("Tutorial").transform.Find("FourteenthTutorial").gameObject.SetActive(true);
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Tutorial").transform.Find("SixteenthTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("SixteenthTutorial").SetActive(false);
                GameObject.Find("Tutorial").transform.Find("SeventeenthTutorial").gameObject.SetActive(true);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
            }

            else if (Input.GetKeyDown(KeyCode.W) && GameObject.Find("Tutorial").transform.Find("EighteenthTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("EighteenthTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("W", WDeck.getLastCard(), "Red");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                WDeck.selectCard();
                state = false;
                recentCard = false;
                FindObjectOfType<SoundManager>().Play("CardSelectSound");
                GameObject.Find("Tutorial").transform.Find("NineteenthTutorial").gameObject.SetActive(true);
            }

            else if (GameObject.Find("Tutorial").transform.Find("TwentiethTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("TwentiethTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("Last").gameObject.SetActive(true);
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("Last").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorial = false;
                    SceneManager.LoadScene(2);
                }
            }
        }

        else
        {
            Resize();

            if (GameObject.Find("Tutorial").transform.Find("FourthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (xState == true && Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("FourthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("FifthTutorial").gameObject.SetActive(true);
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("SixthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (xState == true && Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("SixthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("SeventhTutorial").gameObject.SetActive(true);
                    Time.timeScale = 1.0f;
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("EighthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (xState == true && Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("EighthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("NinethTutorial").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
            
            else if (GameObject.Find("Tutorial").transform.Find("TwelvethTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("TwelvethTutorial").SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("ThirteenthTutorial").gameObject.SetActive(true && xState == true);
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("FourteenthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("FourteenthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("FifteenthTutorial").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("SeventeenthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("SeventeenthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("EighteenthTutorial").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

            else if (GameObject.Find("Tutorial").transform.Find("NineteenthTutorial").gameObject.activeSelf == true && xState == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Tutorial").transform.Find("NineteenthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Tutorial").transform.Find("TwentiethTutorial").gameObject.SetActive(true);
                }
            }
        }
    }

    void cubeSelect()
    {
        if (Input.GetKeyDown(KeyCode.Q) && QDeck.getCountCard() > 0)
        {
            //이거 한번 살펴보기
            CMG.InputCard("Q", QDeck.getLastCard(), "Black");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            QDeck.selectCard();
            state = false;
            recentCard = true;
            FindObjectOfType<SoundManager>().Play("CardSelectSound");
        }

        else if (Input.GetKeyDown(KeyCode.W) && WDeck.getCountCard() > 0)
        {
            CMG.InputCard("W", WDeck.getLastCard(), "Red");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            WDeck.selectCard();
            state = false;
            recentCard = false;
            FindObjectOfType<SoundManager>().Play("CardSelectSound");
        }

        else if (Input.GetKeyDown(KeyCode.E) && EDeck.getCountCard() > 0)
        {
            CMG.InputCard("E", EDeck.getLastCard(), "Black");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            EDeck.selectCard();
            state = false;
            recentCard = true;
            FindObjectOfType<SoundManager>().Play("CardSelectSound");
        }

        else if (Input.GetKeyDown(KeyCode.R) && RDeck.getCountCard() > 0)
        {
            CMG.InputCard("R", RDeck.getLastCard(), "Red");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            RDeck.selectCard();
            state = false;
            recentCard = false;
            FindObjectOfType<SoundManager>().Play("CardSelectSound");
        }
    }

    void Ingame()
    {
        numCube = CMG.numCube();

        if(Input.GetKey(KeyCode.Z))
        {
            Camera.main.fieldOfView--;
        }
        else if(Input.GetKey(KeyCode.X))
        {
            Camera.main.fieldOfView++;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            CMG.Resetcubesize();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("UI").transform.Find("IngameOptions").gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        

        if (startgame == true && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("a");
            fState = false;
            state = true;
            GoTime();
            Countdown.countdown.startcountdown();
        }

        else
        {
            if(numCube%21 == 0)
            {
                reSheffle = true;
            }
            
            if (numCube>0 && numCube%20 == 0)
            {
                if (reSheffle  == true)
                {
                    QDeck = new Deck(true, 0);
                    WDeck = new Deck(true, 0);
                    EDeck = new Deck(false, 0);
                    RDeck = new Deck(false, 0);
                    reSheffle = false;
                }
            }

            if (state == true)
            {
                cubeSelect();
            }
            else
            {
                Resize();
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
        if (GameObject.Find("UI").transform.Find("GameOver").transform.gameObject.activeSelf == false)
        {
            if (tutorial == true)
                Tutorial();
            else
            {
                Ingame();
            }

            Collision_Cube();
        }
    }
}