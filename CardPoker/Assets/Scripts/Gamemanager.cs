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
    [SerializeField]
    private Transform[] Pos_cubes = null;

    int nCube = 0;

    int nQcard, nWcard, nEcard, nRcard;

    float posY = 0;

    int nQcube, nWcube, nEcube, nRcube;

    int countCard = 0;

    List<int> xDeck = new List<int>();
    List<int> zDeck = new List<int>();

    List<int> qDeck = new List<int>();
    List<int> wDeck = new List<int>();
    List<int> eDeck = new List<int>();
    List<int> rDeck = new List<int>();

    public bool xState, zState;

    bool fState;
    bool state;
    bool qState, wState, eState, rState;

    int q, w, e, r;

    void Shuffle()
    {
        int shuffle = 0;
        int tmp = 0;

        for (int i = 0; i < 10; i++)
        {
            xDeck.Insert(i, i);
            zDeck.Insert(i, i);
        }

        for (int i = 0; i < 10; i++)
        {
            shuffle = Random.Range(0, 10 - i);
            tmp = xDeck[9 - i];

            xDeck.RemoveAt(9 - i);
            xDeck.Insert(9 - i, xDeck[shuffle]);

            xDeck.RemoveAt(shuffle);
            xDeck.Insert(shuffle, tmp);
        }

        for (int i = 0; i < 10; i++)
        {
            if (xDeck[i] % 2 == 0)
            {
                qDeck.RemoveAt(q);
                qDeck.Insert(q, xDeck[i]);
                q++;
            }
            else
            {
                eDeck.RemoveAt(e);
                eDeck.Insert(e, xDeck[i]);
                eDeck[e] = xDeck[i];
                e++;
            }
        }

        tmp = 0;

        for (int i = 0; i < 10; i++)
        {
            shuffle = Random.Range(0, 10 - i);
            tmp = zDeck[9 - i];

            zDeck.RemoveAt(9 - i);
            zDeck.Insert(9 - i, zDeck[shuffle]);

            zDeck.RemoveAt(shuffle);
            zDeck.Insert(shuffle, tmp);
        }

        for (int i = 0; i < 10; i++)
        {
            if (zDeck[i] % 2 == 0)
            {
                wDeck.RemoveAt(w);
                wDeck.Insert(w, zDeck[i]);
                w++;
            }
            else
            {
                rDeck.RemoveAt(r);
                rDeck.Insert(r, zDeck[i]);
                r++;
            }
        }
    }

    public void ControlCubeKinemetic(Component Cube)
    {
        if (countCard % 5 == 0 && countCard != 0)
            Cube.GetComponent<Rigidbody>().isKinematic = false;

    }

    public void ChangeLastItemInQDeck(Component image, Sprite[] cardImage)
    {
        if (qDeck[nQcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[qDeck[nQcard] / 2];
    }

    public void ChangeLastItemInWDeck(Component image, Sprite[] cardImage)
    {
        if (wDeck[nWcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[wDeck[nWcard] / 2];
    }

    public void ChangeLastItemInEDeck(Component image, Sprite[] cardImage)
    {
        if (eDeck[nEcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[eDeck[nEcard] / 2];
    }

    public void ChangeLastItemInRDeck(Component image, Sprite[] cardImage)
    {
        if(rDeck[nRcard] == -1)
            image.GetComponent<Image>().overrideSprite = cardImage[5];

        else
            image.GetComponent<Image>().overrideSprite = cardImage[rDeck[nRcard] / 2];
    }

    // Use this for initialization
    void Start()
    {
        q = 0;
        w = 0;
        e = 0;
        r = 0;

        nQcard = 0;
        nWcard = 0;
        nEcard = 0;
        nRcard = 0;

        nQcube = 0;
        nWcube = 0;
        nEcube = 0;
        nRcube = 0;

        for (int i = 0; i < 10; i++)
        {
            qDeck.Insert(i, i);
            wDeck.Insert(i, i);
            eDeck.Insert(i, i);
            rDeck.Insert(i, i);
        }

        qDeck.RemoveAt(5);
        qDeck.Insert(5, -1);

        wDeck.RemoveAt(5);
        wDeck.Insert(5, -1);

        eDeck.RemoveAt(5);
        eDeck.Insert(5, -1);

        rDeck.RemoveAt(5);
        rDeck.Insert(5, -1);

        fState = true;
        state = false;

        qState = false;
        wState = false;
        eState = false;
        rState = false;

        xState = false;
        zState = false;

        Shuffle();
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

    void selectCard(GameObject[] Cube_Type, int Cube_num, List<int> Deck_Type, ref int Type_Cube, ref bool Cube_State)
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
            Type_Cube++;
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
                        selectCard(X_Cubes, nQcube, qDeck, ref nQcube, ref qState);
                    }

                    else if (Input.GetKeyDown(KeyCode.W) && nWcube < 5)
                    {
                        selectCard(Z_Cubes, nWcube, wDeck, ref nWcube, ref wState);
                    }

                    else if (Input.GetKeyDown(KeyCode.E) && nEcube < 5)
                    {
                        selectCard(X_Cubes, nEcube, eDeck, ref nEcube, ref eState);
                    }

                    else if (Input.GetKeyDown(KeyCode.R) && nRcube < 5)
                    {
                        selectCard(Z_Cubes, nRcube, rDeck, ref nRcube, ref rState);
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