using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Memumanager : MonoBehaviour {

    public static bool[] Result = { false, false, false, false };

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Result()");
        for (int i = 0; i < 4; i++)
            Result[i] = false;

        if (File.Exists(Application.dataPath + "/Resources/ResultDate.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/ResultDate.json");

            Debug.Log(jsonStr);

            JsonData playerData = JsonMapper.ToObject(jsonStr);

            Debug.Log(playerData[0]);
            Debug.Log(playerData[1]);
            Debug.Log(playerData[2]);
            Debug.Log(playerData[3]);

            for (int i = 0; i < 4; i++)
                Result[i] = (bool)playerData[i];

            Debug.Log(Result[0]);
            Debug.Log(Result[1]);
            Debug.Log(Result[2]);
            Debug.Log(Result[3]);
        }
        else
        {
            Debug.Log("파일이 존재하지 않습니다.");
        }
    }

    public void initResult()
    {
        for (int i = 0; i < 4; i++)
            Result[i] = false;
        JsonData ResultJson = JsonMapper.ToJson(Memumanager.Result);
        File.WriteAllText(Application.dataPath + "/Resources/ResultDate.json", ResultJson.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < 4; i++)
            Debug.Log(Result[i]);

        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Savior").transform.gameObject.SetActive(Result[0]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Friend").transform.gameObject.SetActive(Result[1]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Impeccable").transform.gameObject.SetActive(Result[2]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Follower").transform.gameObject.SetActive(Result[3]);
    }
}
