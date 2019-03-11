using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemController : MonoBehaviour {

    private static GetItemText itemText;
    private static GameObject ui;

    public static void Initialize()
    {
        ui = GameObject.Find("UI");
        if (!itemText)
        {
            itemText = Resources.Load<GetItemText>("Prefabs/PopupTextParent");
            Debug.Log("a");
        }
    }

    public static void CreateGetItemText(string text, Transform location)
    {
        GetItemText instance = Instantiate(itemText);
        instance.transform.SetParent(ui.transform,false);
        instance.SetText(text);
    }
}
