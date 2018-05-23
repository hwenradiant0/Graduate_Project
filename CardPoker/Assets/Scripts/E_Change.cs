using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Change : MonoBehaviour {

    [SerializeField]
    private Sprite[] Card_image = null;
    
    Gamemanager gamemanager;

    // Use this for initialization
    void Start ()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (gamemanager.get_eDeck()[gamemanager.get_nume()])
        {
            case 1:
                this.GetComponent<Image>().overrideSprite = Card_image[0];
                break;
            case 3:
                this.GetComponent<Image>().overrideSprite = Card_image[1];
                break;
            case 5:
                this.GetComponent<Image>().overrideSprite = Card_image[2];
                break;
            case 7:
                this.GetComponent<Image>().overrideSprite = Card_image[3];
                break;
            case 9:
                this.GetComponent<Image>().overrideSprite = Card_image[4];
                break;
            default:
                this.GetComponent<Image>().overrideSprite = Card_image[5];
                break;
        }
    }
}
