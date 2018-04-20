using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    enum Day
    {
        Monday = 0,
        ThuseDay
    }


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
    int n_Cube = 0;
    //[SerializeField]
    //private Transform[] Pos_Zcubes = null;

    float a = 0;
    int n_xCube = 0;
    int n_zCube = 0;

    int x = 0;
    int z = 0;
    int shuffle = 0;

    int[] X_Deck = null;
    int[] Z_Deck = null;
    int tmp = 0;

    public bool X_Change, Z_Change;

    bool f_state;
    bool state;

    // Use this for initialization
    void Start()
    {
        X_Deck = new int[10];
        Z_Deck = new int[10];
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

        tmp = 0;

        for (int i = 10; i > 0; i--)
        {
            shuffle = Random.Range(0, i);
            tmp = Z_Deck[i - 1];
            Z_Deck[i - 1] = Z_Deck[shuffle];
            Z_Deck[shuffle] = tmp;
        }

        f_state = true;
        state = false;
        X_Change = false;
        Z_Change = false;
    }

    // Update is called once per frame
    void Update()
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
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (n_xCube < 10)
                    {
                        if (n_Cube < 20)
                        {
                            x = X_Deck[n_xCube];
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[x]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 0, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.1f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_xCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }
                    }
                }

                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (n_zCube < 10)
                    {
                        if (n_Cube < 20)
                        {
                            z = Z_Deck[n_zCube];
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[z]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 0, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.1f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_zCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }
                    }
                }

                else if (Input.GetKeyDown(KeyCode.E))
                {
                    if (n_xCube < 10)
                    {
                        if (n_Cube < 20)
                        {
                            x = X_Deck[n_xCube];
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[x]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 0, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.1f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_xCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }
                    }
                }

                else if (Input.GetKeyDown(KeyCode.R))
                {
                    if (n_zCube < 10)
                    {
                        if (n_Cube < 20)
                        {
                            z = Z_Deck[n_zCube];
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[z]);
                            Pos_cubes[n_Cube] = C[n_Cube].transform;
                            if (n_Cube == 0)
                                Pos_cubes[n_Cube].localPosition = new Vector3(0, 0, 0);
                            else
                            {
                                Pos_cubes[n_Cube].localPosition = Pos_cubes[n_Cube - 1].position;
                                Pos_cubes[n_Cube].Translate(0, 0.1f, 0);
                            }
                            Pos_cubes[n_Cube].localRotation = Quaternion.identity;

                            a = a + 0.1f;
                            n_Cube++;
                            n_zCube++;
                            state = false;
                        }
                        else
                        {
                            state = false;
                        }
                    }
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (f_state == false)
                        state = true;
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
                    //Debug.Log(X_Change);
                }
                else
                {
                    X_Change = false;
                    //Debug.Log(X_Change);
                }
            }
            else
            {
                X_Change = false;
                //Debug.Log(X_Change);
            }
        }
        else
        {
            X_Change = true;
            //Debug.Log(X_Change);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (n_Cube > 1)
        {
            if (C[n_Cube - 1].transform.position.z > C[n_Cube - 2].transform.position.z - 1.0f)
            {
                if (C[n_Cube - 1].transform.position.z < C[n_Cube - 2].transform.position.z + 1.0f)
                {
                    Z_Change = true;
                    //Debug.Log(Z_Change);
                }
                else
                {
                    Z_Change = false;
                    //Debug.Log(Z_Change);
                }
            }
            else
            {
                Z_Change = false;
                //Debug.Log(Z_Change);
            }
        }
        else
        {
            Z_Change = true;
            //Debug.Log(Z_Change);
        }

    }
}