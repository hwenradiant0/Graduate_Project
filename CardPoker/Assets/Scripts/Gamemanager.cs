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
    //[SerializeField]
    //private GameObject[] Z_C = null;
    [SerializeField]
    private Transform[] Pos_cubes = null;
    public int n_Cube = 0;
    //[SerializeField]
    //private Transform[] Pos_Zcubes = null;

    public int num_q, num_w, num_e, num_r = -1;

    float a = 0;
    int n_qCube, n_wCube, n_eCube, n_rCube = 0;

    public int x = 0;
    public int z = 0;
    int shuffle = 0;

    public int[] X_Deck = null;
    public int[] Z_Deck = null;

    public int[] Q_Deck = null;
    public int[] W_Deck = null;
    public int[] E_Deck = null;
    public int[] R_Deck = null;

    int tmp = 0;

    public bool X_Change, Z_Change;

    bool f_state;
    bool state;
    bool Q_state, W_state, E_state, R_state;

    int q,w,e,r=0;

    // Use this for initialization
    void Start()
    {
        X_Deck = new int[10];
        Z_Deck = new int[10];

        Q_Deck = new int[6];
        W_Deck = new int[6];
        E_Deck = new int[6];
        R_Deck = new int[6];


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
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log("Q");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Q_Deck[i]);
        }

        Debug.Log("E");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(E_Deck[i]);
        }

        Debug.Log("W");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(num_w);
        }

        Debug.Log("R");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(R_Deck[i]);
        }
        */

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
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (n_qCube < 5)
                    {
                        if (n_Cube < 20)
                        {
                            x = Q_Deck[n_qCube];
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[x]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 1.0f, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.5f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_qCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }
                    }
                    Q_state = true;
                }

                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (n_wCube < 5)
                    {
                        if (n_Cube < 20)
                        {
                            z = W_Deck[n_wCube];
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[z]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 1.0f, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.5f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_wCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }

                    }
                    W_state = true;
                }

                else if (Input.GetKeyDown(KeyCode.E))
                {
                    if (n_eCube < 5)
                    {
                        if (n_Cube < 20)
                        {
                            x = E_Deck[n_eCube];
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[x]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 1.0f, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.5f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_eCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }

                    }
                    E_state = true;
                }

                else if (Input.GetKeyDown(KeyCode.R))
                {
                    if (n_rCube < 5)
                    {
                        if (n_Cube < 20)
                        {
                            z = R_Deck[n_rCube];
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[z]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 1.0f, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.5f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_rCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }

                    }
                    R_state = true;
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (f_state == false)
                    {
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

                        if (Q_state == true)
                        {
                            if (num_q < 5)
                            {
                                num_q++;
                            }
                            Q_state = false;
                            Debug.Log(num_q);
                        }

                        else if (W_state == true)
                        {
                            if (num_w < 5)
                            {
                                num_w++;
                            }
                            W_state = false;
                            Debug.Log(num_w);
                        }

                        else if (E_state == true)
                        {
                            if (num_e < 5)
                            {
                                num_e++;
                            }
                            E_state = false;
                            Debug.Log(num_e);
                        }

                        else if (R_state == true)
                        {
                            if (num_r < 5)
                            {
                                num_r++;
                            }
                            R_state = false;
                            Debug.Log(num_r);
                        }
                        state = true;
                    }
                }
            }
        }

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
}