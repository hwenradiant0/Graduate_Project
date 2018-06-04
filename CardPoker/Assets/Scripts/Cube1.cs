﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1 : MonoBehaviour {

    GameManager gamemanager;
    
    Vector3 temp;
    int a = 0;


    public Color[] color;
    Material m_Material;
    
	// Use this for initialization
	void Start ()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_Material = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Kinemetic(bool test)
    {
        GetComponent<Rigidbody>().isKinematic = test;
    }

    void Move_Cube()
    {
        if (gamemanager.get_countcard() % 5 == 0)
        {
            if (gamemanager.get_countcard() != 0)
                Kinemetic(false);
        }

        if (temp.x >= 2.5f)
        {
            a = 1;
        }
        else if (temp.x <= -2.5f)
        {
            a = 0;
        }

        switch (a)
        {
            case 0:
                temp.x = temp.x + 1 * Time.deltaTime;
                break;
            case 1:
                temp.x = temp.x - 1 * Time.deltaTime;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gamemanager.X_Change == true) // 큐브가 만날때
            {
                a = 2;
            }
        }

        if (a != 2) // 큐브가 움직일때
        {
            if (gamemanager.X_Change == true)
            {
                m_Material.color = color[0];
            }
            else
            {
                m_Material.color = color[1];
            }
        }

        else // 큐브가 멈췄을때
        {
            m_Material.color = color[2];
        }

        transform.localPosition = temp;
    }

    // Update is called once per frame
    void Update ()
    {
        temp = transform.localPosition;
        transform.localPosition = temp;

        Move_Cube();

    }
}
