using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    public static Countdown countdown { get; private set; }

    private void OnEnable()
    {
        countdown = this;
    }

    [SerializeField] private TextMeshProUGUI   uiText = null;
    [SerializeField] private float  mainTimer = 0;

    private float timer;
    private bool canCount;
    private bool doOnce;

    private void Start()
    {
        timer = mainTimer;

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
        }
    }
}
