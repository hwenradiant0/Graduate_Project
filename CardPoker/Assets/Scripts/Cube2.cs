using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    public static Cube2 CurrentCube { get; private set; }

    private void OnEnable()
    {
        CurrentCube = this;
    }

    GameManager gamemanager;

    int CubeNum;

    float MoveSpeed;

    public Color[] color;
    Material m_Material;

    // Use this for initialization
    void Start()
    {
        MoveSpeed = 2.0f;
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_Material = GetComponent<Renderer>().material;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void StopCube()
    {
        MoveSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + transform.forward * MoveSpeed * Time.deltaTime;

        if (this.transform.position.z >= 2.5f)
        {
            MoveSpeed = -2.0f;
        }

        else if (this.transform.position.z <= -2.5f)
        {
            MoveSpeed = 2.0f;
        }

        if (MoveSpeed == 0)
            m_Material.color = color[2];
        else
        {
            if (gamemanager.zState == true)
            {
                m_Material.color = color[0];
            }
            else
            {
                m_Material.color = color[1];
            }
        }
    }

    internal void Stop()
    {
        MoveSpeed = 0;
    }
}