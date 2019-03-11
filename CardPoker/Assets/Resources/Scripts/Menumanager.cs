using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Menumanager : MonoBehaviour {

    public static bool[] Result = { false, false, false, false };

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Result()");
        for (int i = 0; i < 4; i++)
            Result[i] = false;

        if (File.Exists(Application.dataPath + "/Resources/ResultData.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/ResultData.json");

            Debug.Log(jsonStr);

            JsonData playerData = JsonMapper.ToObject(jsonStr);
            
            for (int i = 0; i < 4; i++)
                Result[i] = (bool)playerData[i];
        }
        else
        {
            Debug.Log("파일이 존재하지 않습니다.");
        }
    }

    public void initResult()
    {
        Score.endingScore = 0;
        BestScore.scoreValue = 0;
        int[] jsonscore = { BestScore.scoreValue };
        for (int i = 0; i < 4; i++)
            Result[i] = false;

        JsonData ResultJson = JsonMapper.ToJson(Result);
        File.WriteAllText(Application.dataPath + "/Resources/ResultData.json", ResultJson.ToString());

        JsonData ResultScore = JsonMapper.ToJson(jsonscore);
        File.WriteAllText(Application.dataPath + "/Resources/EndingScoreData.json", ResultScore.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < 4; i++)

        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Savior").transform.gameObject.SetActive(Result[0]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("OffSavior").transform.gameObject.SetActive(!Result[0]);

        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Friend").transform.gameObject.SetActive(Result[1]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("OffFriend").transform.gameObject.SetActive(!Result[1]);

        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Impeccable").transform.gameObject.SetActive(Result[2]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("OffImpeccable").transform.gameObject.SetActive(!Result[2]);

        GameObject.Find("Canvas").transform.Find("Result").transform.Find("Follower").transform.gameObject.SetActive(Result[3]);
        GameObject.Find("Canvas").transform.Find("Result").transform.Find("OffFollower").transform.gameObject.SetActive(!Result[3]);

        if (Result[0] == true && Result[1] == true && Result[2] == true)
            GameObject.Find("Canvas").transform.Find("Result").transform.Find("Eternal").transform.gameObject.SetActive(true);
        else
            GameObject.Find("Canvas").transform.Find("Result").transform.Find("Eternal").transform.gameObject.SetActive(false);

        /*
        if (GameObject.Find("Canvas").transform.Find("Result").transform.Find("Eternal").transform.gameObject.activeSelf == true && Score.endingScore >= 200)
        {
            for(int i=0; i<4; i++)
                Result[i] = false;
            GameObject.Find("Canvas").transform.Find("Result").transform.Find("Ruler").transform.gameObject.SetActive(true);
        }
        */
    }
}
