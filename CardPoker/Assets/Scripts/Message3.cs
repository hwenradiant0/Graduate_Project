using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message3 : MonoBehaviour {

    int numScript;
    public Text script;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numScript++;
        }

        switch (numScript)
        {
            case 0:
                script.text = "만약 이렇게 큐브가 겹치지 않았을때 배치할 경우, ";
                break;
            case 1:
                script.text = "남은 시간이 감소하게 되며 \n 일정 시간동안 큐브를 쌓지 못하게 됩니다.";
                break;
            case 2:
                script.text = "지금은 튜토리얼이기에 패널티는 없도록 하겠습니다.";
                break;
            case 3:
                script.text = "Space Bar를 누르면 계속 진행 됩니다.";
                break;
            case 4:
                FindObjectOfType<SoundManager>().Play("FlipSound");
                Time.timeScale = 1.0f;
                numScript = 0;
                GameObject.Find("Tutorial").transform.FindChild("Message3").gameObject.SetActive(false);
                break;
        }
    }
}
