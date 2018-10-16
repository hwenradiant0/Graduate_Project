using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform Cam;
    Vector3 TargetPosition;
    float target;

    private void Start()
    {
        TargetPosition = new Vector3(0, 0, 0);
        this.transform.LookAt(TargetPosition);
        target = 0;
    }

    public void TargetingCamera(float TargetYpos)
    {
        target += 0.1f;
        TargetPosition = new Vector3(0, target, 0);
        this.transform.LookAt(TargetPosition);
    }
    
    public void CameraUpper()
    {
        this.transform.position = this.transform.position + this.transform.up*0.1f;
    }

    private void Update()
    {

    }
}