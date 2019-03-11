using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

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
                script.text = "당신은 시계토끼의 계략을 알아내고 하트여왕과 힘을 합쳐 동물나라를 구했습니다.";
                break;
            case 1:
                script.text = "비록 처음에는 시계토끼에게 휘둘려 ";
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
