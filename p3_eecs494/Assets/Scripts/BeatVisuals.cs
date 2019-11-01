using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisuals : MonoBehaviour
{
    public Canvas canvas;
    public GameObject indicator;    //The indicator for the beat
    public float speed = 5;             //Speed indicator moves
    public GameObject transparent_indicator;
    private Vector3 spawn_position;       //Where indicator gets spawned in

	private float t = 0;

    void Start()
    {
        spawn_position = transparent_indicator.transform.position + new Vector3(625, 0, 0);
    }

    void Update()
    {
		t += Time.deltaTime;
        if (t > 1.0f)
        {
            spawnBeat();
            t = 0;
        }
    }

    //Spawns a tile for beat visualization
    public void spawnBeat()
    {
        if(t < .05f)
        {
            return;
        }
        var temp = Instantiate(indicator, spawn_position, Quaternion.identity);
        temp.transform.SetParent(canvas.transform);
        //StartCoroutine(temp.GetComponent<BeatAnimations>().changeMaterial());
        temp.GetComponent<Rigidbody>().velocity = new Vector3(-1 * speed, 0, 0);
        Destroy(temp, 4.5f);
        t = 0;
    }

    
}
