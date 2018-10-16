﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] X_Cubes = null;
    [SerializeField]
    public GameObject[] Z_Cubes = null;

    public CameraManager cameramanager;
    public CameraShake   camerashaker;

    private bool Keydownable;
    private bool processcoroutine;

    public Radial_Slider    slider;

    public int numCube = 0;

    bool startgame;

    public bool tutorial = true;

    public void OnTutorial() { tutorial = true;}
    public void OffTutorial() { tutorial = false; startgame = true;}
    public void ResetScore() { Score.scoreValue = 0; }
    public void GoTime() { Time.timeScale = 1.0f; }

    public bool xState, yState, zState;
    bool fState;
    bool state;
    bool recentCard;

    IEnumerator coroutine;

    Deck QDeck = null;
    Deck WDeck = null;
    Deck EDeck = null;
    Deck RDeck = null;

    CardManager CMG = null;

    Countdown countdown = null;

    // Use this for initialization
    void Start()
    {
        QDeck = new Deck(true);
        WDeck = new Deck(true);
        EDeck = new Deck(false);
        RDeck = new Deck(false);

        CMG = new CardManager();
        countdown = new Countdown();

        fState = true;
        state = false;

        xState = false;
        zState = false;

        slider.maxValue = 5;
        slider.value = 5;
        Keydownable = true;

        processcoroutine = false;

        startgame = false;
    }

    public class CardManager
    {
        List<Card> Cards = new List<Card>();

        bool xstate;
        bool zstate;

        public int numCube()
        {
            return Cubes.Count;
        }

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

        public void InputCard(string Type, int Num, string Color)
        {
            Cards.Add(new Card { CardType = Type, CardNum = Num, CardColor = Color});
            Debug.Log(Num);
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
                Cubes[Cubes.Count-1].transform.localPosition = new Vector3(0.0f,0.05f,0.0f);
            }
            else
            {
                if (Cards[Cards.Count - 1].CardType == "Q" || Cards[Cards.Count - 1].CardType == "E")
                {
                    Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x-Cubes[Cubes.Count-2].transform.localScale.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z);
                    Cubes[Cubes.Count - 1].transform.localScale = Cubes[Cubes.Count - 2].transform.localScale;
                }
                else
                {
                    Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z- Cubes[Cubes.Count - 2].transform.localScale.z);
                    Cubes[Cubes.Count - 1].transform.localScale = Cubes[Cubes.Count - 2].transform.localScale;
                }
            }

            Cubes[Cubes.Count-1].transform.localRotation = Quaternion.identity;
        }

        public void ResizeCube(bool card)
        {
            if (Cubes.Count>1)
            {
                float hangover;

                if (card == true)
                {
                    hangover = Cubes[Cubes.Count - 1].transform.position.x - Cubes[Cubes.Count - 2].transform.position.x;
                    SplitCubeOnX(hangover);
                }
                else
                {
                    hangover = Cubes[Cubes.Count - 1].transform.position.z - Cubes[Cubes.Count - 2].transform.position.z;
                    SplitCubeOnZ(hangover);
                }
            }
        }

        private void SplitCubeOnX(float hangover)
        {
            float newXSize = Cubes[Cubes.Count - 2].transform.localScale.x - Mathf.Abs(hangover);

            float newXPosition = Cubes[Cubes.Count - 2].transform.position.x + (hangover / 2);

            Cubes[Cubes.Count - 1].transform.localScale = new Vector3(newXSize, Cubes[Cubes.Count - 1].transform.localScale.y, Cubes[Cubes.Count - 1].transform.localScale.z);
            Cubes[Cubes.Count - 1].transform.position = new Vector3(newXPosition, Cubes[Cubes.Count - 1].transform.position.y, Cubes[Cubes.Count - 1].transform.position.z);
        }

        private void SplitCubeOnZ(float hangover)
        {
            float newZSize = Cubes[Cubes.Count - 2].transform.localScale.z - Mathf.Abs(hangover);

            float newZPosition = Cubes[Cubes.Count - 2].transform.position.z + (hangover / 2);

            Cubes[Cubes.Count - 1].transform.localScale = new Vector3(Cubes[Cubes.Count - 1].transform.localScale.x, Cubes[Cubes.Count - 1].transform.localScale.y, newZSize);
            Cubes[Cubes.Count - 1].transform.position = new Vector3(Cubes[Cubes.Count - 1].transform.position.x, Cubes[Cubes.Count - 1].transform.position.y, newZPosition);
        }

        public float LastCubePosion()
        {
            if (Cubes.Count > 1)
                return Cubes[Cubes.Count - 2].transform.position.y;
            else
                return Cubes[0].transform.position.y;
        }

        public void CheckCollision()
        {
            if (Cubes.Count > 1)
            {
                if (Cubes[Cubes.Count - 1].transform.position.x > Cubes[Cubes.Count - 2].transform.position.x - Cubes[Cubes.Count - 1].transform.localScale.x && 
                    Cubes[Cubes.Count - 1].transform.position.x < Cubes[Cubes.Count - 2].transform.position.x + Cubes[Cubes.Count - 1].transform.localScale.x)
                {
                    xstate = true;
                }
                else
                {
                    xstate = false;
                }
          
                if (Cubes[Cubes.Count - 1].transform.position.z > Cubes[Cubes.Count - 2].transform.position.z - Cubes[Cubes.Count - 1].transform.localScale.z && 
                    Cubes[Cubes.Count - 1].transform.position.z < Cubes[Cubes.Count - 2].transform.position.z + Cubes[Cubes.Count - 1].transform.localScale.z)
                {
                    zstate = true;
                }

                else
                {
                    zstate = false;
                }
            }
        }
        
        public void ControlCube(int num)
        {
            int temp = Cards.Count;

            for (int i = num; i > 0; i--)
            {
                Cubes[temp - i].GetComponent<FixedCube>().fixedCube();
                Cards[temp - i].CardNum = 10;
                Cards[temp - i].CardType = "Joker";
                Cards[temp - i].CardColor = "White";
            }
        }

        public void scoreCheck()
        {
            if (Cubes.Count > 1)
            {
                if (Cards[Cards.Count - 1].CardColor == Cards[Cards.Count - 2].CardColor)           ///////////////////// 같은 색 일때
                {
                    if (Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == 1 || Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == -1)  // 연속된 수 일때 제거
                    {
                        Score.scoreValue += 2;
                        ControlCube(2);
                    }

                    else if (Cubes.Count > 2)
                    {
                        if(Cards[Cards.Count - 2].CardColor == Cards[Cards.Count - 3].CardColor) // 세개가 같은 색 일때
                        {
                            Score.scoreValue += 1;
                            ControlCube(3);
                        }
                    }
                }

                else // 다른 색 일때
                {
                    if (Cards[Cards.Count - 1].CardNum == Cards[Cards.Count - 2].CardNum) // 같은 숫자일때
                    {
                        Score.scoreValue += 2;
                        ControlCube(2);
                    }

                    if (Cubes.Count > 2)
                    {
                        if (Cards[Cards.Count - 1].CardColor == Cards[Cards.Count - 3].CardColor)
                        {
                            if (Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == 1)
                            {
                                if (Cards[Cards.Count - 2].CardNum - Cards[Cards.Count - 3].CardNum == 1)
                                {
                                    Score.scoreValue += 3;
                                    ControlCube(3);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
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

    //public void Resize()
    //{
    //    CMG.CheckCollision();
        
    //    slider.value = sw.ElapsedMilliseconds / 1000;
 
    //    if (fState == false && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (xState == true && zState == true)
    //        {
    //            if (Keydownable == true)
    //            {
    //                if (recentCard == true)
    //                    Cube1.CurrentCube.Stop();
    //                else
    //                    Cube2.CurrentCube.Stop();

    //                state = true;
    //                CMG.ResizeCube(recentCard);
    //                CMG.CubeDestroy();
    //            }
    //        }

    //        else
    //        {
    //            Keydownable = false;
    //            StartCoroutine(OnUpdateRoutine());
    //            //Timer();
    //            //////////////////// 이부분
    //        }
    //    }
    //}

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
                        Cube1.CurrentCube.Stop();
                    else
                        Cube2.CurrentCube.Stop();

                    state = true;
                    CMG.ResizeCube(recentCard);
                    CMG.scoreCheck();
                    if (numCube > 1)
                    {
                        //cameramanager.TargetingCamera(CMG.LastCubePosion());
                        cameramanager.CameraUpper();
                    }
                }
            }

            else
                {
                    if (processcoroutine == false && tutorial == false)
                    {
                        StartCoroutine(OnUpdateRoutine());
                        Countdown.countdown.decreaseTime(10.0f);
                        StartCoroutine(camerashaker.Shake(0.15f, 0.5f));
                        //CameraShaker.Instance.ShakeOnce(1.0f, 4.0f, 0.1f, 0.1f);
                    }
                }

            if (GameObject.Find("Canvas").transform.FindChild("FourthTutorial").gameObject.activeSelf == true && tutorial == true)
            {
                GameObject.Find("Canvas").transform.FindChild("FourthTutorial").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.FindChild("FifthTutorial").gameObject.SetActive(true);
            }

            else if (GameObject.Find("Canvas").transform.FindChild("SixthTutorial").gameObject.activeSelf == true && tutorial == true)
            {
                if (xState == true && Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Canvas").transform.FindChild("SixthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Canvas").transform.FindChild("SeventhTutorial").gameObject.SetActive(true);
                    Time.timeScale = 1.0f;
                }
            }

            else if (GameObject.Find("Canvas").transform.FindChild("EighthTutorial").gameObject.activeSelf == true && tutorial == true && numCube == 3)
            {
                if (xState == true && Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("Canvas").transform.FindChild("EighthTutorial").gameObject.SetActive(false);
                    GameObject.Find("Canvas").transform.FindChild("NinethTutorial").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
        }
    }

    //void Timer()
    //{
    //    slider.value = 0;

    //    if(time != 0)
    //    {
    //        Debug.Log(time);
    //        time = time - Time.deltaTime;
    //        slider.value = time/1000;
    //        if(time<=0)
    //        {
    //            time = 0;
    //        }
    //    }
    //    Keydownable = true;
    //    slider.value = 5;
    //}

    //void Timer()
    //{
    //    sw.Start();
    //    if (sw.ElapsedMilliseconds > 5000)
    //    {
    //        Keydownable = true;

    //        sw.Stop();
    //        sw.Reset();
    //    }
    //}

    IEnumerator OnUpdateRoutine()
    {
        processcoroutine = true;

        slider.value = 0;

        Keydownable = false;

        for (int i = 0; i < 5; i++)
        {
            slider.value = i;

            yield return new WaitForSeconds(1);
        }

        Keydownable = true;
        slider.value = 5;

        processcoroutine = false;
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
        numCube = CMG.numCube();

        if (Input.GetKeyDown(KeyCode.S) && GameObject.Find("Canvas").transform.FindChild("FirstTutorial").gameObject.activeSelf == true)
        {
            GameObject.Find("FirstTutorial").SetActive(false);

            GameObject.Find("Canvas").transform.FindChild("SecondTutorial").gameObject.SetActive(true);
            fState = false;
            state = true;

            Countdown.countdown.startcountdown();

            Time.timeScale = 0.0f;
        }

        if (GameObject.Find("Canvas").transform.FindChild("SecondTutorial").gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Find("SecondTutorial").SetActive(false);
                GameObject.Find("Canvas").transform.FindChild("ThirdTutorial").gameObject.SetActive(true);
            }
        }

        if (state == true)
        {
            if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Canvas").transform.FindChild("ThirdTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("ThirdTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Canvas").transform.FindChild("FourthTutorial").gameObject.SetActive(true);
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Canvas").transform.FindChild("FifthTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("FifthTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Canvas").transform.FindChild("SixthTutorial").gameObject.SetActive(true);
            }

            else if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("Canvas").transform.FindChild("SeventhTutorial").gameObject.activeSelf == true)
            {
                GameObject.Find("SeventhTutorial").SetActive(false);
                Time.timeScale = 1.0f;
                CMG.InputCard("Q", QDeck.getLastCard(), "Black");
                CMG.CreateCube(X_Cubes, Z_Cubes);
                QDeck.selectCard();
                state = false;
                recentCard = true;
                GameObject.Find("Canvas").transform.FindChild("EighthTutorial").gameObject.SetActive(true);
            }

            else if (GameObject.Find("Canvas").transform.FindChild("NinethTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("NinethTutorial").SetActive(false);
                    GameObject.Find("Canvas").transform.FindChild("TenthTutorial").gameObject.SetActive(true);
                }
            }

            else if (GameObject.Find("Canvas").transform.FindChild("TenthTutorial").gameObject.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.Find("TenthTutorial").SetActive(false);
                    Time.timeScale = 1.0f;
                    tutorial = false;
                }
            }
        }

        else
        {
            Resize();
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
        }

        else if (Input.GetKeyDown(KeyCode.W) && WDeck.getCountCard() > 0)
        {
            CMG.InputCard("W", WDeck.getLastCard(), "Red");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            WDeck.selectCard();
            state = false;
            recentCard = false;
        }

        else if (Input.GetKeyDown(KeyCode.E) && EDeck.getCountCard() > 0)
        {
            CMG.InputCard("E", EDeck.getLastCard(), "Black");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            EDeck.selectCard();
            state = false;
            recentCard = true;
        }

        else if (Input.GetKeyDown(KeyCode.R) && RDeck.getCountCard() > 0)
        {
            CMG.InputCard("R", RDeck.getLastCard(), "Red");
            CMG.CreateCube(X_Cubes, Z_Cubes);
            RDeck.selectCard();
            state = false;
            recentCard = false;
        }
    }

    void Ingame()
    {
        numCube = CMG.numCube();

        if (startgame == true && Input.GetKeyDown(KeyCode.S))
        {
            fState = false;
            state = true;
            Countdown.countdown.startcountdown();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Canvas").transform.FindChild("IngameOptions").gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            GameObject.Find("Canvas").transform.FindChild("ScoreBoard").gameObject.SetActive(true);
            Time.timeScale = 1.0f;
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            GameObject.Find("ScoreBoard").SetActive(false);
        }

        else
        {
            bool reSheffle = false;
            
            if (numCube>0 && numCube%20 == 0)
            {
                reSheffle = true;
            }

            if (reSheffle == true)
            {
                QDeck = new Deck(true);
                WDeck = new Deck(true);
                EDeck = new Deck(false);
                RDeck = new Deck(false);
                reSheffle = false;
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
        if (tutorial == true)
            Tutorial();
        else
        {
            Ingame();
        }

        Collision_Cube();
    }
}