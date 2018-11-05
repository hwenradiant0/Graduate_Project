using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MultipleScore : MonoBehaviour {

    public static int multipleValue = 1;

    Text multipleText;

    void Start()
    {
        multipleText = GetComponent<Text>();
    }


    void Update()
    {
        multipleText.text = "X " + multipleValue;
    }
}
