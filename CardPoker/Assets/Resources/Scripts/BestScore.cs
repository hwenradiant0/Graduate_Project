using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;
using System.IO;

public class BestScore : MonoBehaviour {

    public static int scoreValue = 0;
    
    Text scoreText;

    // Use this for initialization
    void Start ()
    {
        scoreText = GetComponent<Text>();

        string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/EndingScoreData.json");
        JsonData playerData = JsonMapper.ToObject(jsonStr);
        scoreValue = (int)playerData[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "최고점수 : " + scoreValue;
    }
}
