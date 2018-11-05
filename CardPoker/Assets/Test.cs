using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour {

    Image timeBar;

    float maxTime = 100.0f;

    public static float time;

	// Use this for initialization
	void Start ()
    {
        timeBar = GetComponent<Image>();
        time = maxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;
        timeBar.fillAmount = time / maxTime;
	}
}
