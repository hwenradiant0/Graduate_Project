using UnityEngine;
using System.Collections;
using RTS_Cam;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour
{
    private RTS_Camera cam;
    private new Camera camera;
    public string targetsTag;

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam.SetTarget(GameObject.FindWithTag(targetsTag).transform);
        }
    }
}
