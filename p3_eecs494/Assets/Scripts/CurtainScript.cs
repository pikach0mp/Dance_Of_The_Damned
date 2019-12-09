using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainScript : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("Player"))
        {
            ManageGame.FirstTimeCurtain();
        }
    }
}
