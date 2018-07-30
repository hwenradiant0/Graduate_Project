using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1 : MonoBehaviour {

    GameManager gamemanager;
    
    int a = 0;

    int CubeNum;

    float MoveSpeed = 1.0f;

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

    private void KinematicOn()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Kinematicoff()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// kinematic = true
    /// 단, 한시적으로(큐브가 터졌을때) 
    /// kinematic = false 로 바꿔주고 충돌시 다시 true로
    /// 
    /// kinematicoff : 중력 영향 on
    /// kinematicon : 중력 영향 off
    /// </summary>

    void moveCube()
    {
        if(gamemanager.xState == true)
        {
            Kinematicoff();
        }

        else
        {
            KinematicOn();
        }

        if (this.transform.position.x >= 2.5f)
        {
            a = 1;
        }
        else if (this.transform.position.x <= -2.5f)
        {
            a = 0;
        }

        switch (a)
        {
            case 0:
                this.transform.position = this.transform.position + transform.right * MoveSpeed * Time.deltaTime;
                break;
            case 1:
                this.transform.position = this.transform.position - transform.right * MoveSpeed * Time.deltaTime;
                break;
            case 2:
                MoveSpeed = 0;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gamemanager.xState == true) // 큐브가 만날때
            {
                a = 2;
            }
        }

        if (a != 2) // 큐브가 움직일때
        {
            if (gamemanager.xState == true)
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
    }

    // Update is called once per frame
    void Update ()
    {
        moveCube();
    }
}
