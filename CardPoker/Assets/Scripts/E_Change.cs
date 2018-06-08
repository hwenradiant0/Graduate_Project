using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Change : MonoBehaviour {

    [SerializeField]
    private Sprite[] Card_image = null;

    GameManager gamemanager;

    // Use this for initialization
    void Start ()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gamemanager.ChangeLastItemInEDeck(this, Card_image);
    }
}
