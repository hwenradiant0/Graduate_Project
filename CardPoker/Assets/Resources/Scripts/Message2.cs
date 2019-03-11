using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message2 : MonoBehaviour {

    int numScript;
    public Text script;

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("UI").transform.Find("Score").gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numScript++;
        }

        switch (numScript)
        {
            case 0:
                script.text = "첫째로 카드 덱 입니다.";
                break;
            case 1:
                script.text = "카드 덱은 각각 Q, W, E, R로 제어가 가능합니다.";
                GameObject.Find("Tutorial").transform.Find("Q").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("W").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("E").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(true);
                break;
            case 2:
                GameObject.Find("Tutorial").transform.Find("W").gameObject.SetActive(false);//qe
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(false);
                script.text = "먼저 Q와 E는 스페이드 카드를, ";
                break;
            case 3:
                GameObject.Find("Tutorial").transform.Find("W").gameObject.SetActive(true);//wr
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("Q").gameObject.SetActive(false);
                GameObject.Find("Tutorial").transform.Find("E").gameObject.SetActive(false);
                script.text = "W와 R은 하트 카드를 쌓을 수 있습니다";
                break;
            case 4:
                GameObject.Find("Tutorial").transform.Find("Q").gameObject.SetActive(true);//qw
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(false);
                script.text = "그리고 Q와 W는 홀수 카드가 배치되며, ";
                break;
            case 5:
                GameObject.Find("Tutorial").transform.Find("E").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("Q").gameObject.SetActive(false);//er
                GameObject.Find("Tutorial").transform.Find("W").gameObject.SetActive(false);
                script.text = "E와 R은 짝수 카드가 배치됩니다.";
                break;
            case 6:
                script.text = "그럼 이제 카드를 직접 배치해보겠습니다.";
                break;
            case 7:
                FindObjectOfType<SoundManager>().Play("FlipSound");
                GameObject.Find("Tutorial").transform.Find("FirstTutorial").gameObject.SetActive(true);
                GameObject.Find("UI").transform.Find("Score").gameObject.SetActive(true);
                GameObject.Find("UI").transform.Find("Filled").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("Message2").gameObject.SetActive(false);
                GameObject.Find("Tutorial").transform.Find("E").gameObject.SetActive(false);
                GameObject.Find("Tutorial").transform.Find("R").gameObject.SetActive(false);
                break;
        }
    }
}
