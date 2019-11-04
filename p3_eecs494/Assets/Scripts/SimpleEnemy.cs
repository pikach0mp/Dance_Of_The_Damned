using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float stepSize = 1;
    public LayerMask Mask;

    private Quaternion target_rot;
    private Vector3 target_pos;
    private Vector3 original_pos;
    private BeatHitDetector bhd;
    private Health health;
    private int turnCount;

    // Start is called before the first frame update
    void Start()
    {
        turnCount = 0;
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
        turnCount++;

        if (turnCount % 2 == 0)
        {
            original_pos = transform.position;
            //always move forward
            if (!Physics.Raycast(transform.position, transform.forward, stepSize, Mask))
            {
                target_pos += transform.forward * stepSize;
            }
            else
            {
                target_rot = Quaternion.AngleAxis(180, Vector3.up) * transform.rotation;
            }
        }
    }

    //damage player if ran into, bounce back to original after
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().update_health(-1);
            target_pos = original_pos;
        }
    }
}
