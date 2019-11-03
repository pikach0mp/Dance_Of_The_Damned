using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float stepSize = 1;
    private Quaternion target_rot;
    private Vector3 target_pos;
    private BeatHitDetector bhd;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        target_rot = transform.rotation;
        target_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.dead())
        {
            Destroy(gameObject);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, .25f);
        transform.position = Vector3.Lerp(transform.position, target_pos, .25f);
    }

    public void OnBeat()
    {
        //always move forward
        if (!Physics.Raycast(transform.position, transform.forward, stepSize))
        {
            target_pos += transform.forward * stepSize;
        }
        else
        {
            target_rot = Quaternion.AngleAxis(180, Vector3.up) * transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().update_health(-1);
        }
    }
}
