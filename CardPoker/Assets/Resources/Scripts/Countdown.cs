using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;
using System.IO;

public class Countdown : MonoBehaviour
{
    public static Countdown countdown { get; private set; }

    Image timeBar;
    float maxTime = 100.0f;
    int score;

    private void OnEnable()
    {
        countdown = this;
    }

    [SerializeField] private Text   uiText = null;
    [SerializeField] private float  mainTimer = 0;

    private float timer;

    private bool canCount;
    private bool doOnce;
    private bool getIcon;

    private void Start()
    {
        timer = mainTimer;

        timeBar = GetComponent<Image>();

        canCount = false;
        doOnce = false;
        getIcon = false;
    }

    internal void startcountdown()
    {
        canCount = true;
    }

    internal void decreaseTime(float time)
    {
        timer = timer - time;
    }

    private void Update()
    {
        if (timer >= 100)
            timer = 100;

        timeBar.fillAmount = timer / maxTime;
        if (timer >= 0.0f && canCount == true)
        {
            timer = timer - Time.deltaTime;
            uiText.text = timer.ToString("F");
        }

        else if (timer <= 0.0f && doOnce == false)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;

            if (CardManager.boom == false && Score.scoreValue == 0 && CardManager.playCard > 10)
            {
                GameObject.Find("UI").transform.Find("Follower").transform.gameObject.SetActive(true);
                Menumanager.Result[3] = true;
                JsonData ResultJson = JsonMapper.ToJson(Menumanager.Result);
                File.WriteAllText(Application.dataPath + "/Resources/ResultData.json", ResultJson.ToString());
                getIcon = true;
            }

            else
            {
                if (CardManager.playCard == 0 && GameManager.combo == true && Score.scoreValue > 300)
                {
                    GameObject.Find("UI").transform.Find("Saviour").transform.gameObject.SetActive(true);
                    Menumanager.Result[0] = true;
                    JsonData ResultJson = JsonMapper.ToJson(Menumanager.Result);
                    File.WriteAllText(Application.dataPath + "/Resources/ResultData.json", ResultJson.ToString());
                    getIcon = true;
                }

                else if (CardManager.playCard == 0 && Score.scoreValue > 200)
                {
                    GameObject.Find("UI").transform.Find("Friend").transform.gameObject.SetActive(true);
                    Menumanager.Result[1] = true;
                    JsonData ResultJson = JsonMapper.ToJson(Menumanager.Result);
                    File.WriteAllText(Application.dataPath + "/Resources/ResultData.json", ResultJson.ToString());
                    getIcon = true;
                }

                else if (GameManager.combo == true && Score.scoreValue > 150)
                {
                    GameObject.Find("UI").transform.Find("Impeccable").transform.gameObject.SetActive(true);
                    Menumanager.Result[2] = true;
                    JsonData ResultJson = JsonMapper.ToJson(Menumanager.Result);
                    File.WriteAllText(Application.dataPath + "/Resources/ResultData.json", ResultJson.ToString());
                    getIcon = true;
                }
            }
            
            if (getIcon == false)
            {
                GameObject.Find("UI").transform.Find("GameOver").transform.gameObject.SetActive(true);
            }

            if (Score.scoreValue > score)       // 현재 스코어 저장
            {
                int[] jsonscore = { Score.scoreValue };
                JsonData ResultScore = JsonMapper.ToJson(jsonscore);
                File.WriteAllText(Application.dataPath + "/Resources/EndingScoreData.json", ResultScore.ToString());
                BestScore.scoreValue = Score.scoreValue;
            }
            else
                BestScore.scoreValue = score;
        }
    }
}
