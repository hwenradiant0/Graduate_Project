using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    public static Cube2 CurrentCube { get; private set; }
    public GameObject CubePlaceEffect;

    private void OnEnable()
    {
        CurrentCube = this;
    }

    GameManager gamemanager;

    int CubeNum;

    float MoveSpeed;
    float Direction;

    public Color[] color;
    Material m_Material;

    // Use this for initialization
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CubeNum = gamemanager.numCube;

        if (gamemanager.tutorial == true)
            MoveSpeed = 3.0f;
        else
            MoveSpeed = 3.0f + 0.3f*CubeNum;
        Direction = 1.0f;
        m_Material = GetComponent<Renderer>().material;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + transform.forward * MoveSpeed * Direction * Time.deltaTime;

        if (this.transform.position.z >= 2.5f)
        {
            Direction = -1.0f;
            if (MoveSpeed > 3.0f)
                MoveSpeed = MoveSpeed - 0.3f;
        }

        else if (this.transform.position.z <= -2.5f)
        {
            Direction = 1.0f;
            if (MoveSpeed > 3.0f)
                MoveSpeed = MoveSpeed - 0.3f;
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
        GameObject effectIns = (GameObject)Instantiate(CubePlaceEffect, this.transform.position, this.transform.rotation);
        Destroy(effectIns, 0.5f);
    }
}