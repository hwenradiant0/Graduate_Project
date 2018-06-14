using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck
{
    bool even;

    List<int> CardDeck = new List<int>();
   
    public Deck()
    {
    }

    public List<int> getCards()
    {
        return CardDeck;
    }

    void shuffle(ref List<int> shuffleList)
    {
        for (int i = 0; i < 4; i++)
        {
            int shuffle = Random.Range(0, 3 - i);
            int temp = shuffleList[4 - i];

            shuffleList[4 - i] = shuffleList[shuffle];
            shuffleList[shuffle] = temp;
        }
    }

    public void Init(bool even)
    {
        this.even = even;

        if (this.even == true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 5)
                    CardDeck.Add(-1);
                else
                    CardDeck.Add(i * 2);
            }
        }

        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 5)
                    CardDeck.Add(-1);
                else
                    CardDeck.Add(i * 2 + 1);
            }
        }

        shuffle(ref CardDeck);
    }
}

public class GameManager : MonoBehaviour
{
    Deck QDeck = new Deck();
    Deck WDeck = new Deck();
    Deck EDeck = new Deck();
    Deck RDeck = new Deck();

    [SerializeField]
    private GameObject[] X_Cubes = null;
    [SerializeField]
    private GameObject[] Z_Cubes = null;
    [SerializeField]
    private GameObject[] C = null;
    [SerializeField]
    private Transform[] Pos_cubes = null;

    int nCube = 0;

    int nQcard, nWcard, nEcard, nRcard;

    float posY = 0;

    int nQcube, nWcube, nEcube, nRcube;

    int countCard = 0;

    List<int> xDeck = new List<int>();
    List<int> zDeck = new List<int>();

    public bool xState, zState;

    bool fState;
    bool state;
    bool qState, wState, eState, rState;

    public void ControlCubeKinemetic(Component Cube)
    {
        if (countCard % 5 == 0 && countCard != 0)
            Cube.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ChangeLastItemInQDeck(Component image, Sprite[] cardImage)
    {
        if (QDeck.getCards()[nQcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[QDeck.getCards()[nQcard] / 2];
    }

    public void ChangeLastItemInWDeck(Component image, Sprite[] cardImage)
    {
        if (WDeck.getCards()[nWcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[WDeck.getCards()[nWcard] / 2];
    }

    public void ChangeLastItemInEDeck(Component image, Sprite[] cardImage)
    {
        if (EDeck.getCards()[nEcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[EDeck.getCards()[nEcard] / 2];
    }

    public void ChangeLastItemInRDeck(Component image, Sprite[] cardImage)
    {
        if (RDeck.getCards()[nRcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[RDeck.getCards()[nRcard] / 2];
    }


    // Use this for initialization
    void Start()
    {
        QDeck.Init(true);
        WDeck.Init(true);
        EDeck.Init(false);
        RDeck.Init(false);

        nQcard = 0;
        nWcard = 0;
        nEcard = 0;
        nRcard = 0;

        nQcube = 0;
        nWcube = 0;
        nEcube = 0;
        nRcube = 0;

        fState = true;
        state = false;

        qState = false;
        wState = false;
        eState = false;
        rState = false;

        xState = false;
        zState = false;

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

    void selectCard(GameObject[] Cube_Type, ref int Cube_num, List<int> Deck_Type, ref bool Cube_State)
    {
        if (Cube_num <= 5)
        {
            int card = Deck_Type[Cube_num];
            C[nCube] = GameObject.Instantiate(Cube_Type[card]);
            Pos_cubes[nCube] = C[nCube].transform;
            if (nCube == 0)
                Pos_cubes[nCube].localPosition = new Vector3(0, 1.0f, 0);
            else
            {
                Pos_cubes[nCube].localPosition = Pos_cubes[nCube - 1].position;
                Pos_cubes[nCube].Translate(0, 0.5f, 0);
            }
            Pos_cubes[nCube].localRotation = Quaternion.identity;

            posY = posY + 0.1f;
            nCube++;
            Cube_num++;
            state = false;
            Cube_State = true;
        }

        else
        {
            state = false;
        }
    }

    void placeCard(ref int nCard, bool axis, ref bool cubeState)
    {
        if (nCard < 5)
        {
            countCard++;
            nCard++;
            axis = false;
            cubeState = false;
            state = true;
        }
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
                if (countCard < 5)
                {
                    if (Input.GetKeyDown(KeyCode.Q) && nQcube < 5)
                    {
                        selectCard(X_Cubes, ref nQcube, QDeck.getCards(), ref qState);
                    }

                    else if (Input.GetKeyDown(KeyCode.W) && nWcube < 5)
                    {
                        selectCard(Z_Cubes, ref nWcube, WDeck.getCards(),  ref wState);
                    }

                    else if (Input.GetKeyDown(KeyCode.E) && nEcube < 5)
                    {
                        selectCard(X_Cubes, ref nEcube, EDeck.getCards(), ref eState);
                    }

                    else if (Input.GetKeyDown(KeyCode.R) && nRcube < 5)
                    {
                        selectCard(Z_Cubes, ref nRcube, RDeck.getCards(),  ref rState);
                    }
                }
                else
                {
                    if (nCube >= 5)
                    {
                        if (Input.GetKeyDown(KeyCode.K))
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Destroy(C[i]);
                            }
                            nCube = 0;
                            countCard = 0;
                        }
                    }
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (fState == false)
                    {
                        if (qState == true)
                        {
                            placeCard(ref nQcard, xState, ref qState);
                        }

                        else if (wState == true)
                        {
                            placeCard(ref nWcard, zState, ref wState);
                        }

                        else if (eState == true)
                        {
                            placeCard(ref nEcard, xState, ref eState);
                        }

                        else if (rState == true)
                        {
                            placeCard(ref nRcard, zState, ref rState);
                        }

                        else
                        {
                            state = false;
                        }
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