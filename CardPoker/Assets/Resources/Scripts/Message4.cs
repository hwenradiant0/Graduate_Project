using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message4 : MonoBehaviour {

    int numScript;
    public Text script;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numScript++;
        }

        switch (numScript)
        {
            case 0:
                script.text = "친구들과 함께 놀고 있던 앨리스는 말하는 토끼에게 이끌려 동물 세계로 오게 된다.";
                break;
            case 1:
                script.text = "스스로 동물들의 대표라고 소개한 시계토끼는 하트여왕이 동물들을 카드로 바꿔서 카드 안에 가두었고, 동물 친구들을 구하기 위해서 앨리스를 불러왔다고 한다.";
                break;
            case 2:
                script.text = "아무것도 모르는 앨리스는 시계토끼의 말대로 동물들을 구하기 위해 동물들이 변한 카드로 탑을 쌓기 시작하는데...";
                break;
            case 3:
                GameObject.Find("Canvas").transform.Find("Story").gameObject.SetActive(false);
                numScript = 0;
                break;
            default:
                break;
        }
    }

}
