using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeatHitDetector))]
public class PlayerMovement : MonoBehaviour
{
    public float stepSize = 1;

    private Quaternion target_rot;
    private Vector3 target_pos;
    private BeatHitDetector bhd;

    void Start()
    {
        target_rot = transform.rotation;
        target_pos = transform.position;
    }

    void Awake()
    {
        bhd = GetComponent<BeatHitDetector>();
    }

    void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, .3f);
        transform.position = Vector3.Lerp(transform.position, target_pos, .25f);

        if(Input.GetKeyDown(KeyCode.Space)) {
            bhd.PressButton(KeyCode.Space);
        }
        else if(Input.GetKeyDown(KeyCode.Period))
        {
            bhd.PressButton(KeyCode.Period);
        }

        //KeyCode key = KeyCode.Space;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Input.GetKey(KeyCode.W) && !Physics.Raycast(transform.position, transform.forward, stepSize))
        //    {
        //        target_pos += transform.forward * stepSize;
        //    }
        //    else if (Input.GetKey(KeyCode.S) && !Physics.Raycast(transform.position, -transform.forward, stepSize))
        //    {
        //        target_pos -= transform.forward * stepSize;
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        target_rot = Quaternion.AngleAxis(90, Vector3.up) * target_rot;
        //    }
        //    else if (Input.GetKey(KeyCode.A))
        //    {
        //        target_rot = Quaternion.AngleAxis(-90, Vector3.up) * target_rot;
        //    }
        //}
    }

    public void OnBeatHit((KeyCode key, BeatInfo) info)
    {
        KeyCode key = info.Item1;
        if (key == KeyCode.Space)
        {
            if (Input.GetKey(KeyCode.W) && !Physics.Raycast(transform.position, transform.forward, stepSize))
            {
                target_pos += transform.forward * stepSize;
            }
            else if (Input.GetKey(KeyCode.S) && !Physics.Raycast(transform.position, -transform.forward, stepSize))
            {
                target_pos -= transform.forward * stepSize;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                target_rot = Quaternion.AngleAxis(90, Vector3.up) * target_rot;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                target_rot = Quaternion.AngleAxis(-90, Vector3.up) * target_rot;
            }
        }
        else if (key == KeyCode.Period)
        {
            StartCoroutine(GetComponent<PlayerAttack>().SpinAttack());
        }
    }
}
