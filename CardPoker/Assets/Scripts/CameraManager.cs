using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void CameraUpper()
    {
        this.transform.position = this.transform.position + this.transform.up*0.1f;
    }
}