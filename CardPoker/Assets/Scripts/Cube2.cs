using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour {

    Gamemanager gamemanager;

    Vector3 temp;
    int a = 0;

    // Use this for initialization
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
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

        transform.localPosition = temp;
	}
}
