using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    public AudioClip thump;
    public AudioClip scraping;
    public GameObject target;
    public float stepSize = 1;
    public LayerMask Mask;
    public int turnsbeforemoving = 1;

    private AudioSource audioS;
    private int turnCount;

    private Vector3 travelDir;
    private Vector3 original_pos;
    private Vector3 target_pos;

    private bool chasing;
    private Vector3 lastSeen;
    private bool patroling;

    // Start is called before the first frame update
    void Start()
    {
        lastSeen.y = 999;
        audioS = GetComponent<AudioSource>();
        target_pos = transform.position;
        original_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
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

    public void OnBeat()
    {
        Debug.Log("direction" + travelDir.ToString());

        original_pos = transform.position;
        
        if (!Physics.Raycast(transform.position, travelDir, stepSize))
        {
            target_pos += travelDir * stepSize;
        }
        else
        {
            travelDir *= -1;
        }
    }

    //return true if no walls inbetween enemy and player
    private bool look_for_player()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        directionToPlayer.y = 0;
        Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit);
        Debug.DrawRay(transform.position, directionToPlayer, Color.red);
        //if there are not walls inbetween enemy and player
        if (hit.transform.gameObject.tag == "Player"){
            return true;
        }
        return false;
    }

    private void set_direction()
    {
        //set movement to chase player
        Vector3 diff = transform.position - lastSeen;
        List<float> directions = new List<float> { diff.x, 0, diff.z };
        List<float> one_direction = new List<float> { 0, 0, 0 };
        one_direction[get_max(directions)] = directions[get_max(directions)];
        diff.x = one_direction[0]; diff.y = one_direction[1]; diff.z = one_direction[2];
        travelDir = diff;
    }

    private void patrol()
    {
        List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.right, Vector3.left, Vector3.back };
        List<float> raycastDis = new List<float>();

        for(int i = 0; i < directions.Count; i++)
        {
            Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit);
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
    }

}
