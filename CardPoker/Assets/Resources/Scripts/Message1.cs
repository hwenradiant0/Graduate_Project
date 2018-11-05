using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message1 : MonoBehaviour {

    int numScript;
    public Text script;


    // Use this for initialization
    void Start ()
    {
        numScript = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            numScript++;
        }

        switch(numScript)
        {
            case 0:
                script.text = "블록 포커에 오신것을 환영합니다!";
                break;
            case 1:
                script.text = "블록 포커는 카드를 이용해 블록을 쌓는 게임입니다.";
                break;
            case 2:
                script.text = "튜토리얼을 진행하기 전에 간단한 설명이 있겠습니다.";
                break;
            case 3:
                FindObjectOfType<SoundManager>().Play("FlipSound");
                GameObject.Find("Tutorial").transform.Find("Message1").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Filled").gameObject.SetActive(false);
                GameObject.Find("Tutorial").transform.Find("Message2").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("Q").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("W").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("E").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(true);
                break;
            default:
                break;
        }
	}


}
