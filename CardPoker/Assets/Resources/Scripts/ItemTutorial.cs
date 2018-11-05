using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTutorial : MonoBehaviour {

    int numScript;
    public Text script;

    // Use this for initialization
    void Start()
    {
        numScript = 0;
    }

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
                script.text = "또한, 점수를 쌓으면 4개의 아이템 중 하나가 무작위로 발동 되며, \n각각 가속, 시야차단, 둔화, 방어의 효과를 가집니다.";
                break;
            case 1:
                CardManager.numItem = 1;
                script.text = "첫째로 시야차단 입니다. \n 시야 차단은 일정 시간 동안 화면을 가려서 블록을 쌓는데 방해 합니다";
                break;
            case 2:
                CardManager.numItem = 2;
                script.text = "둘째로 가속 입니다. \n 가속은 다음 1회에 한해 블록의 속도가 더 빨라져서 블록을 쌓는데 방해 합니다.";
                break;
            case 3:
                CardManager.numItem = 3;
                script.text = "셋째로 둔화 입니다.\n 둔화는 다음 1회에 한해 블록의 속도를 느리게 해서 블록을 더 쉽게 쌓도록 합니다.";
                break;
            case 4:
                CardManager.numItem = 4;
                script.text = "마지막으로 방어 입니다.\n 방어는 다음 블록을 쌓는 동안 블록쌓기를 실패 했을때의 패널티를 막아 줍니다.";
                break;
            case 5:
                GameObject.Find("Tutorial").transform.Find("EleventhTutorial").gameObject.SetActive(true);
                GameObject.Find("Tutorial").transform.Find("ItemTutorial").gameObject.SetActive(false);
                break;
        }
    }
}
