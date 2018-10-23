using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCube : MonoBehaviour {

    public Color[] color;
    public GameObject CubeFixedEffect;
    int num;

    Material m_Material;
    bool Fixed;
    
    // Use this for initialization
    void Start ()
    {
        m_Material = GetComponent<Renderer>().material;
        Fixed = false;
        num = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Fixed == true)
            m_Material.color = color[num];
    }

    public void fixedCube(int cubenum)
    {
        num = cubenum;
        Fixed = true;
        GameObject effectIns = (GameObject)Instantiate(CubeFixedEffect, this.transform.position, this.transform.rotation);
        Destroy(effectIns, 1f);
    }
}
