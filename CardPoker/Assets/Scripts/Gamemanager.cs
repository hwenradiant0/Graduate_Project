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
    int n_Cube = 0;
    //[SerializeField]
    //private Transform[] Pos_Zcubes = null;
    
    float a = 0;
    int n_xCube = 0;
    int n_zCube = 0;

    int k = 0;

    public bool X_Change, Z_Change;

    bool f_state;
    bool state;

    // Use this for initialization
    void Start()
    {
        f_state = false;
        state = false;
        X_Change = false;
        Z_Change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            f_state = true;
            state = true;
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
                            k = Random.Range(0, 10);
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[k]);
                            Debug.Log(k);
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
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[n_zCube]);
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
                            k = Random.Range(0, 10);
                            C[n_Cube] = GameObject.Instantiate(X_Cubes[k]);
                            Debug.Log(k);
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
                            C[n_Cube] = GameObject.Instantiate(Z_Cubes[n_zCube]);
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
                    if(f_state == true)
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