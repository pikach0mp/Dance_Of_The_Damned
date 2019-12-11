using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginChase : MonoBehaviour
{
    public GameObject door;
    public GameObject nextDoor;
    public GameObject Camera;
    public List<GameObject> chaseEnemies;
    public float TimeB4Enemy = 1;
    public float cutSceneLength;

    private LevelTransition transition;
    public int level = 1;
    private bool first = true;
    private bool fogDecrease = false;
    private float originalFogDensity;

    public AudioTrack track;

    private void Start()
    {
        transition = Camera.GetComponent<LevelTransition>();
        originalFogDensity = RenderSettings.fogDensity;
    }

    private void Update()
    {
        if(fogDecrease)
        {
            if (level == 2) 
                RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, 0f, Time.deltaTime * 200);
            else
                RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, .2f, Time.deltaTime * 100);
        }

        //else
        //{
        //    RenderSettings.fogDensity = originalFogDensity;
        //}
    }

    private IEnumerator PerformCutScene()
    {
        Camera.SetActive(true);
        if (level == 2)
        {
            fogDecrease = true;
        }
        StartCoroutine(transition.PerformCutScene());
        yield return new WaitForSeconds(cutSceneLength);

        Camera.SetActive(false);
        Debug.Log("setting to original");
        RenderSettings.fogDensity = originalFogDensity;
        BeatGenerator.StartAudio(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && first)
        {
            first = false;
            if (level != 3)
            {
                BeatGenerator.StartAudio(2);
                BeatGenerator.ToggleBeatSystem(false);
                door.SetActive(true);

                StartCoroutine(PerformCutScene());
            } else {
                fogDecrease = true;
                AudioSource source = GetComponent<AudioSource>();
                source.Play();
                BeatGenerator.StartAudio(1);
            }

            //enable enemies
            StartCoroutine(StartEnemies());
        }
    }

    IEnumerator StartEnemies()
    {
        yield return new WaitForSeconds(TimeB4Enemy);
        foreach(var enemy in chaseEnemies)
        {
            enemy.GetComponent<ChaseEnemy>().trigger(true);
        }
    }
}
