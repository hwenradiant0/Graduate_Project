using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCube : MonoBehaviour {

    public Color[] color;
    Material m_Material;
    bool Fixed;
    
    // Use this for initialization
    void Start ()
    {
        m_Material = GetComponent<Renderer>().material;
        Fixed = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Fixed == true)
            m_Material.color = color[0];
    }

    public void fixedCube()
    {
        Fixed = true;
    }
}
