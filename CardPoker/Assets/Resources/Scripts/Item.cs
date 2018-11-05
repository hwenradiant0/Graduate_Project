using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    Text itemText;

    // Use this for initialization
    void Start ()
    {
        itemText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (CardManager.numItem)
        {
            case 1: // 0
                itemText.text = "Blinder";
                break;
            case 2: // 1
                itemText.text = "Accel";
                break;
            case 3: // 2
                itemText.text = "Slow";
                break;
            case 4: // 3
                itemText.text = "Defence";
                break;
            default:
                itemText.text = "Space!";
                break;
        }
    }
}
