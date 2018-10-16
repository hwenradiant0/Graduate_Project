using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {

    public static int scoreValue = 0;

    TextMeshProUGUI scoreText;

    void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
	}
	

	void Update () {
        scoreText.text = "Score : " + scoreValue;
	}
}
