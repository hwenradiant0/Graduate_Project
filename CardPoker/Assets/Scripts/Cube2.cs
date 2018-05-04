﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour {

    Gamemanager gamemanager;

    Vector3 temp;
    int a = 0;

    public Color[] color;
    Material m_Material;

    // Use this for initialization
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        m_Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update ()
    {
        temp = transform.localPosition;
        transform.localPosition = temp;

        if (temp.z >= 2.5f)
        {
            a = 1;
        }
        else if (temp.z <= -2.5f)
        {
            a = 0;
        }

        switch (a)
        {
            case 0:
                temp.z = temp.z + 2*Time.deltaTime;
                break;
            case 1:
                temp.z = temp.z - 2*Time.deltaTime;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gamemanager.Z_Change == true)
            {
                a = 2;
            }
            else
            {

            }
        }

        if (a != 2) // 큐브가 움직일때
        {
            if (gamemanager.Z_Change == true)
            {
                m_Material.color = color[0];
            }
            else
            {
                m_Material.color = color[1];
            }
        }

        else // 큐브가 멈췄을 때
        {
            m_Material.color = color[2];
        }

        transform.localPosition = temp;
	}
}
