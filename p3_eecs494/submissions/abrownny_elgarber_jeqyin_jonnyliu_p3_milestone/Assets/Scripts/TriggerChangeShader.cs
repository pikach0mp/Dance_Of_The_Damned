using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeShader : MonoBehaviour
{
    public Shader baseShader;
    public Shader fadeShader;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Wall blocking camera");

        if (other.gameObject.tag == "MainCamera")
        {
            GetComponent<Renderer>().material.shader = fadeShader;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Wall no longer blocking");

        if (other.gameObject.tag == "MainCamera")
        {
            GetComponent<Renderer>().material.shader = baseShader;
        }
    }
}