using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void CameraMover(float x, float y, float z)
    {
        this.transform.Translate(x,y,z);
    }
}