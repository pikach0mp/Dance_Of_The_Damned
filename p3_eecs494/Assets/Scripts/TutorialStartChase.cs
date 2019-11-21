using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartChase : MonoBehaviour
{
    public GameObject door;
    public GameObject Camera;
    public GameObject canv;
    public List<GameObject> chaseEnemies;
    public float TimeB4Enemy = 1;

    private bool first = true;

    IEnumerator PerformCutScene()
    {
        //Cut Scene
        Camera.SetActive(true);

        Camera.GetComponent<Animator>().SetTrigger("triggered");

        yield return new WaitForSeconds(6f);
        door.GetComponent<TutorialDoor>().OpenDoor();
        door.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        Camera.SetActive(false);
        StartCoroutine(StartEnemies());
        BeatGenerator.ToggleBeatSystem(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && first)
        {
            first = false;
            canv.GetComponent<Animator>().SetTrigger("StartCutScene");

            BeatGenerator.ToggleBeatSystem(false);
            StartCoroutine(PerformCutScene());
        }
    }

    IEnumerator StartEnemies()
    {
        yield return new WaitForSeconds(TimeB4Enemy);
        foreach (var enemy in chaseEnemies)
        {
            enemy.GetComponent<ChaseEnemy>().trigger(true);
        }
    }
}

