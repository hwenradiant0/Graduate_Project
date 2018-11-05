﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    public static Countdown countdown { get; private set; }

    Image timeBar;
    float maxTime = 100.0f;

    private void OnEnable()
    {
        countdown = this;
    }

    [SerializeField] private Text   uiText = null;
    [SerializeField] private float  mainTimer = 0;

    private float timer;

    private bool canCount;
    private bool doOnce;

    private void Start()
    {
        timer = mainTimer;

        timeBar = GetComponent<Image>();

        canCount = false;
        doOnce = false;
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
            GameObject.Find("UI").transform.Find("GameOver").transform.gameObject.SetActive(true);
        }
    }
}
