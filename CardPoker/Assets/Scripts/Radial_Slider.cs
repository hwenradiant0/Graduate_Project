using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class Radial_Slider : MonoBehaviour
{
    public Image filled;
    public TextMeshProUGUI text;

    public float maxValue = 100;
    public float value = 0;

    private void Update()
    {
        value = Mathf.Clamp(value, 0, maxValue);
        float amount = value / maxValue;
        
        filled.fillAmount = amount;
        text.text = Mathf.RoundToInt(5-value).ToString();
    }
}
