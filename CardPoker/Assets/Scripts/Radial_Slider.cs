using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Radial_Slider : MonoBehaviour
{
    public Image Open;

    public Image filled;
    public Text text;

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
