using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSound : MonoBehaviour {

	public void buttonSound()
    {
        FindObjectOfType<SoundManager>().Play("SelectUISound");
    }
}
