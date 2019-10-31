using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BeatHitDetector))]
public class PlayerMovement : MonoBehaviour
{
    public float stepSize = 1;
    //private BeatHitDetector bhd;

    void Awake()
    {
        //bhd = GetComponent<BeatHitDetector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //bhd.PressButton(KeyCode.Space);
            OnBeatHit(KeyCode.Space);
        }
    }

    public void OnBeatHit(KeyCode key)
    {
        if (key == KeyCode.Space)
        {
            if (Input.GetKey(KeyCode.W) && !Physics.Raycast(transform.position, transform.forward, stepSize))
            {
                transform.position += transform.forward * stepSize;
            }
            else if (Input.GetKey(KeyCode.S) && !Physics.Raycast(transform.position, -transform.forward, stepSize))
            {
                transform.position -= transform.forward * stepSize;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up) * transform.rotation;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.up) * transform.rotation;
            }
        }
    }
}
