using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatVisuals : MonoBehaviour
{
    public Canvas canvas;
    //public RawImage indicator;    //The indicator for the beat
    //public float speed = 5;             //Speed indicator moves
    public RawImage transparent_indicator;
    private Vector3 spawn_position;       //Where indicator gets spawned in

	private float t = 0;



    public RawImage IndicatorPrefab;                                    //The column game object.
    public int IndicatorPoolSize = 10;                                    //How many columns to keep on standby.
    public float spawnRate = 1f;                                    //How quickly columns spawn.

    private RawImage[] Indicators;                                    //Collection of pooled columns.
    private int currentIndicator = 0;                                    //Index of the current column in the collection.

    private Vector2 objectPoolPosition = new Vector2(-15, -25);        //A holding position for our unused columns offscreen.
    private float spawnXPosition = 10f;
    private float timeSinceLastSpawned;


    void Start()
    {
        spawn_position = transparent_indicator.transform.position + new Vector3(625, 0, 0);

        //Spawn in the indicator pool
        timeSinceLastSpawned = 0f;
        Indicators = new RawImage[IndicatorPoolSize];
        for(int i = 0; i < IndicatorPoolSize; i++)
        {
            Indicators[i] = Instantiate(IndicatorPrefab, spawn_position, Quaternion.identity);
            Indicators[i].transform.SetParent(canvas.transform);
        }

    }

    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if(timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            //Place Indicators in the right place 
            Indicators[currentIndicator].transform.position = spawn_position;
            currentIndicator++;
        }

        if(currentIndicator >= IndicatorPoolSize)
        {
            currentIndicator = 0;
        }







		//t += Time.deltaTime;
  //      if (t > 1.0f)
  //      {
  //          spawnBeat();
  //          t = 0;
  //      }
    }

    //Spawns a tile for beat visualization
    //public void spawnBeat()
    //{
    //    Debug.Log("Spawn");
    //    var temp = Instantiate(indicator, spawn_position, Quaternion.identity);
    //    temp.transform.SetParent(canvas.transform);
    //    temp.GetComponent<BeatAnimations>().moving = true;
    //    //StartCoroutine(temp.GetComponent<BeatAnimations>().changeMaterial());
    //    //temp.GetComponent<Rigidbody>().velocity = new Vector3(-1 * speed, 0, 0);
    //    Destroy(temp.gameObject, 4.0f);
    //    t = 0;
    //}


    IEnumerator foursec()
    {
        yield return new WaitForSeconds(4.0f);
        Debug.Log(transform.position.x);
    }

    
}
