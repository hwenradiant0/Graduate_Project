using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int n_Cube = 0;

    int num_q, num_w, num_e, num_r;

    float Pos_Y = 0;

    int n_qCube, n_wCube, n_eCube, n_rCube;

    int count_card = 0;

    List<int> X_Deck = new List<int>();
    List<int> Z_Deck = new List<int>();

    List<int> Q_Deck = new List<int>();
    List<int> W_Deck = new List<int>();
    List<int> E_Deck = new List<int>();
    List<int> R_Deck = new List<int>();

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
            X_Deck.Insert(i, i);
            Z_Deck.Insert(i, i);
        }

        for (int i = 0; i < 10; i++)
        {
            shuffle = Random.Range(0, 10 - i);
            tmp = X_Deck[9 - i];

            X_Deck.RemoveAt(9 - i);
            X_Deck.Insert(9 - i, X_Deck[shuffle]);

            X_Deck.RemoveAt(shuffle);
            X_Deck.Insert(shuffle, tmp);
        }

        for (int i = 0; i < 10; i++)
        {
            if (X_Deck[i] % 2 == 0)
            {
                Q_Deck.RemoveAt(q);
                Q_Deck.Insert(q, X_Deck[i]);
                q++;
            }
            else
            {
                E_Deck.RemoveAt(e);
                E_Deck.Insert(e, X_Deck[i]);
                E_Deck[e] = X_Deck[i];
                e++;
            }
        }

        tmp = 0;

        for (int i = 0; i < 10; i++)
        {
            shuffle = Random.Range(0, 10 - i);
            tmp = Z_Deck[9 - i];

            Z_Deck.RemoveAt(9 - i);
            Z_Deck.Insert(9 - i, Z_Deck[shuffle]);

            Z_Deck.RemoveAt(shuffle);
            Z_Deck.Insert(shuffle, tmp);
        }

        for (int i = 0; i < 10; i++)
        {
            if (Z_Deck[i] % 2 == 0)
            {
                W_Deck.RemoveAt(w);
                W_Deck.Insert(w, Z_Deck[i]);
                w++;
            }
            else
            {
                R_Deck.RemoveAt(r);
                R_Deck.Insert(r, Z_Deck[i]);
                r++;
            }
        }
    }

    public int get_numq() { return num_q; }
    public int get_numw() { return num_w; }
    public int get_nume() { return num_e; }
    public int get_numr() { return num_r; }

    public List<int> get_qDeck() { return Q_Deck; }
    public List<int> get_wDeck() { return W_Deck; }
    public List<int> get_eDeck() { return E_Deck; }
    public List<int> get_rDeck() { return R_Deck; }

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

        for (int i = 0; i < 10; i++)
        {
            Q_Deck.Insert(i, i);
            W_Deck.Insert(i, i);
            E_Deck.Insert(i, i);
            R_Deck.Insert(i, i);
        }

        Q_Deck.RemoveAt(5);
        Q_Deck.Insert(5, -1);

        W_Deck.RemoveAt(5);
        W_Deck.Insert(5, -1);

        E_Deck.RemoveAt(5);
        E_Deck.Insert(5, -1);

        R_Deck.RemoveAt(5);
        R_Deck.Insert(5, -1);

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

    void Select_Card(GameObject[] Cube_Type, int Cube_num, List<int> Deck_Type)
    {
        if (Cube_num <= 5)
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

            if (Deck_Type == Q_Deck)
                Debug.Log("q : " + Cube_num);
            else if (Deck_Type == W_Deck)
                Debug.Log("w : " + Cube_num);
            Pos_Y = Pos_Y + 0.1f;
            n_Cube++;
            Cube_num++;
            state = false;
        }

        else
        {
            Debug.Log("d3" + Cube_num);
            state = false;
        }
    }

    bool Place_Card(int n_card, bool K_state, bool change)
    {
        if (change == true)
        {
            if (n_card < 5)
            {
                count_card++;
                change = false;
                state = true;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
                    if (Input.GetKeyDown(KeyCode.Q) && n_qCube < 5)
                    {
                        Debug.Log("a");
                        Select_Card(X_Cubes, n_qCube, Q_Deck);
                        n_qCube++;
                        Q_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.W) && n_wCube < 5)
                    {
                        Debug.Log("a");
                        Select_Card(Z_Cubes, n_wCube, W_Deck);
                        n_wCube++;
                        W_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.E) && n_eCube < 5)
                    {
                        Debug.Log("a");
                        Select_Card(X_Cubes, n_eCube, E_Deck);
                        n_eCube++;
                        E_state = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.R) && n_rCube < 5)
                    {
                        Debug.Log("a");
                        Select_Card(Z_Cubes, n_rCube, R_Deck);
                        n_rCube++;
                        R_state = true;
                    }
                }
                else
                {
                    //Debug.Log("b");
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
                //Debug.Log("c");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (f_state == false)
                    {
                        if (Q_state == true)
                        {
                            if (Place_Card(num_q, Q_state, X_Change) == true)
                            {
                                num_q++;
                                Q_state = false;
                            }
                        }

                        else if (W_state == true)
                        {
                            if (Place_Card(num_w, W_state, Z_Change) == true)
                            {
                                num_w++;
                                W_state = false;
                            }
                        }

                        else if (E_state == true)
                        {
                            if (Place_Card(num_e, E_state, X_Change) == true)
                            {
                                num_e++;
                                E_state = false;
                            }
                        }

                        else if (R_state == true)
                        {
                            if (Place_Card(num_r, R_state, Z_Change) == true)
                            {
                                num_r++;
                                R_state = false;
                            }
                        }

                        else
                        {
                            Debug.Log("d");
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