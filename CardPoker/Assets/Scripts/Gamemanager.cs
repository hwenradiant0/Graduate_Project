using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] X_Cubes = null;
    [SerializeField]
    private GameObject[] Z_Cubes = null;
    [SerializeField]
    private GameObject[] C = null;
    [SerializeField]
    private Transform[] Pos_cubes = null;

    int n_Cube = 0;

    int num_q, num_w, num_e, num_r;

    float Pos_Y = 0;

    int n_qCube, n_wCube, n_eCube, n_rCube;
    
    int count_card = 0;

    int[] X_Deck = null;
    int[] Z_Deck = null;

    int[] Q_Deck = null;
    int[] W_Deck = null;
    int[] E_Deck = null;
    int[] R_Deck = null;

    public bool X_Change, Z_Change;

    bool f_state;
    bool state;
    bool Q_state, W_state, E_state, R_state;

    public bool X_state, Z_state;

    int q, w, e, r;

    void Shuffle()
    {
        int shuffle = 0;
        int tmp = 0;

        for (int i = 0; i < 10; i++)
        {
            X_Deck[i] = i;
            Z_Deck[i] = i;
        }

        for (int i = 10; i > 0; i--)
        {
            shuffle = Random.Range(0, i);
            tmp = X_Deck[i - 1];
            X_Deck[i - 1] = X_Deck[shuffle];
            X_Deck[shuffle] = tmp;
        }

        for (int i = 0; i < 10; i++)
        {
            if (X_Deck[i] % 2 == 0)
            {
                Q_Deck[q] = X_Deck[i];
                q++;
            }
            else
            {
                E_Deck[e] = X_Deck[i];
                e++;
            }
        }

        tmp = 0;

        for (int i = 10; i > 0; i--)
        {
            shuffle = Random.Range(0, i);
            tmp = Z_Deck[i - 1];
            Z_Deck[i - 1] = Z_Deck[shuffle];
            Z_Deck[shuffle] = tmp;
        }

        for (int i = 0; i < 10; i++)
        {
            if (Z_Deck[i] % 2 == 0)
            {
                W_Deck[w] = Z_Deck[i];
                w++;
            }
            else
            {
                R_Deck[r] = Z_Deck[i];
                r++;
            }
        }
    }

    public int get_numq() { return num_q; }
    public int get_numw() { return num_w; }
    public int get_nume() { return num_e; }
    public int get_numr() { return num_r; }

    public int[] get_qDeck() { return Q_Deck; }
    public int[] get_wDeck() { return W_Deck; }
    public int[] get_eDeck() { return E_Deck; }
    public int[] get_rDeck() { return R_Deck; }

    public int get_countcard() { return count_card; }

    // Use this for initialization
    void Start()
    {
        q = 0;
        w = 0;
        e = 0;
        r = 0;

        num_q = 0;
        num_w = 0;
        num_e = 0;
        num_r = 0;

        n_qCube = 0;
        n_wCube = 0;
        n_eCube = 0;
        n_rCube = 0;

        X_Deck = new int[10];
        Z_Deck = new int[10];

        Q_Deck = new int[6];
        W_Deck = new int[6];
        E_Deck = new int[6];
        R_Deck = new int[6];

        Q_Deck[5] = -1;
        W_Deck[5] = -1;
        E_Deck[5] = -1;
        R_Deck[5] = -1;

        f_state = true;
        state = false;

        Q_state = false;
        W_state = false;
        E_state = false;
        R_state = false;

        X_Change = false;
        Z_Change = false;

        X_state = false;
        Z_state = false;

        Shuffle();

        //test = true;
    }

    void Select_Card(GameObject[] Cube_Type, int Cube_num, int[] Deck_Type)
    {
        if (Cube_num < 5)
        {
            if (n_Cube < 20)
            {
                int card = Deck_Type[Cube_num];
                C[n_Cube] = GameObject.Instantiate(Cube_Type[card]);
                Pos_cubes[n_Cube] = C[n_Cube].transform;
                if (n_Cube == 0)
                    Pos_cubes[n_Cube].localPosition = new Vector3(0, 1.0f, 0);
                else
                {
                    Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                    Pos_cubes[n_Cube].Translate(0, 0.5f, 0);
                }
                Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                Pos_Y = Pos_Y + 0.1f;
                n_Cube++;
                Cube_num++;
                state = false;
            }
            else
            {
                state = false;
            }
        }

        X_state = true;
        Z_state = false;
    }

    void Place_Card(int n_card, bool K_state, bool change)
    {
        if (change == true)
        {
            if (n_card < 5)
            {
                count_card++;
                change = false;
                state = true;
            }
        }
    }

    void Ingame()
    {
        if (f_state == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                f_state = false;
                state = true;
            }
        }

        else
        {
            if (state)
            {
                if (count_card < 5)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Select_Card(X_Cubes, n_qCube, Q_Deck);
                        n_qCube++;
                        Q_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        Select_Card(Z_Cubes, n_wCube, W_Deck);
                        n_wCube++;
                        W_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Select_Card(X_Cubes, n_eCube, E_Deck);
                        n_eCube++;
                        E_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        Select_Card(Z_Cubes, n_rCube, R_Deck);
                        n_rCube++;
                        R_state = true;
                    }
                }
                else
                {
                    if (n_Cube >= 5)
                    {
                        if (Input.GetKeyDown(KeyCode.K))
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Destroy(C[i]);
                            }
                            n_Cube = 0;
                            count_card = 0;
                        }
                    }
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (f_state == false)
                    {
                        if (Q_state == true)
                        {
                            Place_Card(num_q, Q_state, X_Change);
                            num_q++;
                            Q_state = false;
                        }

                        else if (W_state == true)
                        {
                            Place_Card(num_w, W_state, Z_Change);
                            num_w++;
                            W_state = false;
                        }

                        else if (E_state == true)
                        {
                            Place_Card(num_e, E_state, X_Change);
                            num_e++;
                            E_state = false;
                        }

                        else if (R_state == true)
                        {
                            Place_Card(num_r, R_state, Z_Change);
                            num_r++;
                            R_state = false;
                        }

                        else
                        {
                            state = false;
                        }

                        X_state = false;
                        Z_state = false;
                    }
                }
            }

            if (num_q > 4)
            {
                num_q = 5;
            }

            if (num_w > 4)
            {
                num_w = 5;
            }

            if (num_e > 4)
            {
                num_e = 5;
            }

            if (num_r > 4)
            {
                num_r = 5;
            }
        }
    }

    void Collision_Cube()
    {
        if (n_Cube > 1)
        {
            if (C[n_Cube - 1].transform.position.x > C[n_Cube - 2].transform.position.x - 1.0f)
            {
                if (C[n_Cube - 1].transform.position.x < C[n_Cube - 2].transform.position.x + 1.0f)
                {
                    X_Change = true;
                }
                else
                {
                    X_Change = false;
                }
            }
            else
            {
                X_Change = false;
            }
        }
        else
        {
            X_Change = true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (n_Cube > 1)
        {
            if (C[n_Cube - 1].transform.position.z > C[n_Cube - 2].transform.position.z - 1.0f)
            {
                if (C[n_Cube - 1].transform.position.z < C[n_Cube - 2].transform.position.z + 1.0f)
                {
                    Z_Change = true;
                }
                else
                {
                    Z_Change = false;
                }
            }
            else
            {
                Z_Change = false;
            }
        }
        else
        {
            Z_Change = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ingame();

        Collision_Cube();
    }
}