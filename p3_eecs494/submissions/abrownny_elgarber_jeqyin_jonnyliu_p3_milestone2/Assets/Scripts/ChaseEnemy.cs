using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeatHitDetector))]
public class ChaseEnemy : MonoBehaviour
{
    public AudioClip thump;
    public GameObject target;
    public float stepSize = 1;
    public int turnsbeforemoving = 1;
    public Material active;
    public Material not_active;

    private bool triggered = false;

    private AudioSource audioS;
    private int turnCount;

    private Vector3 travelDir;
    private Vector3 original_pos;
    private Vector3 target_pos;

    private bool chasing;
    private Vector3 lastSeen;
    private bool patroling;

    public void trigger(bool onOff)
    {
        triggered = onOff;
        if (triggered)
        {
            GetComponent<Renderer>().material = active;
        }
        else
        {
            GetComponent<Renderer>().material = not_active;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = not_active;
        lastSeen.y = 999;
        audioS = GetComponent<AudioSource>();
        target_pos = transform.position;
        original_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            transform.position = Vector3.Lerp(transform.position, target_pos, .25f);
            //reached last seen
            if (Vector3.Distance(lastSeen, transform.position) < 1)
            {
                lastSeen.y = 999;
            }

            //player spotted
            if (look_for_player())
            {
                patroling = false;
                chasing = true;
                lastSeen = target.transform.position;

                set_direction();
            }
            //did not see player
            else
            {
                chasing = false;
                //go to last seen place if previously was chasing
                if (lastSeen.y!=999)
                {
                    set_direction();
                }
                //previously was going to last seen place, switch to patrolling
                else if(lastSeen.y == 999 && !patroling)
                {
                    patroling = true;
                    patrol();
                }
            }
        }
    }
    public void OnBeatMissed(BeatInfo info)
    {
        if (info.noteInPattern != 0 && info.noteInPattern != 2)
        {
            return;
        }

        original_pos = transform.position;

        if (triggered)
        {
            audioS.Play();
        }

        if (!Physics.Raycast(transform.position, travelDir, stepSize))
        {
            target_pos += travelDir * stepSize;
        }
        else
        {
            travelDir *= -1;
            target_pos += travelDir * stepSize;
        }
    }

    public void OnBeatHit((ButtonPress, BeatInfo) info)
    {
        if(info.Item2.noteInPattern != 0 && info.Item2.noteInPattern != 2)
        {
            return;
        }

        original_pos = transform.position;

        if (triggered)
        {
            audioS.Play();
        }
        
        if (!Physics.Raycast(transform.position, travelDir, stepSize))
        {
            target_pos += travelDir * stepSize;
        }
        else
        {
            travelDir *= -1;
            target_pos += travelDir * stepSize;
        }
    }

    //return true if no walls inbetween enemy and player
    private bool look_for_player()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;

        Vector3 lowerRay = transform.position;
        lowerRay.y -= 0.6f;
        if(Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit))
        {
            //if there are not walls inbetween enemy and player
            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private void set_direction()
    {
        //set movement to chase player
        Vector3 diff = lastSeen - transform.position;
        diff.y = 0;

        //if x is not zero, smaller than z or z is zero. 
        if(Math.Abs(diff.x) > 0.9f && 
            !Physics.Raycast(transform.position, new Vector3(diff.x/Math.Abs(diff.x), 0, 0), stepSize) 
            && (Math.Abs(diff.x) < Math.Abs(diff.z) || Math.Abs(diff.z) < 0.9f || Physics.Raycast(transform.position, new Vector3(0, 0, diff.z / Math.Abs(diff.z)), stepSize) ))
        {
            travelDir = new Vector3(diff.x/ Math.Abs(diff.x), 0, 0);
        }
        else
        {
            travelDir = new Vector3(0, 0, diff.z/ Math.Abs(diff.z));
        }
    }

    private void patrol()
    {
        List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.right, Vector3.left, Vector3.back };
        List<float> raycastDis = new List<float>();

        for(int i = 0; i < directions.Count; i++)
        {
            Physics.Raycast(transform.position, directions[i], out RaycastHit hit);
            raycastDis.Add(hit.distance);
        }

        travelDir = directions[get_max(raycastDis)];
    }

    private int get_max(List<float> input)
    {
        int index = 0;
        float maxVal = 0;

        for(int i = 1; i < input.Count; i++)
        {
            if (maxVal < Math.Abs(input[i]))
            {
                index = i;
                maxVal = Math.Abs(input[i]);
            }
        }
        return index;
    }

    //damage player if ran into, bounce back to original after
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().update_health(-1);
            target_pos = original_pos;
        }

        Debug.Log(other.transform.name);
    }

}
