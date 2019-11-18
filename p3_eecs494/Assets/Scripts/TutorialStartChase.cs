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

    IEnumerator WaitForDirections()
    {
        yield return new WaitForSeconds(8f);
        //Cut Scene
        Camera.SetActive(true);

        Camera.GetComponent<Animator>().SetTrigger("triggered");
        StartCoroutine(WaitForLookingAtDoor());
    }

    IEnumerator WaitForLookingAtDoor()
    {
        yield return new WaitForSeconds(3f);
        door.GetComponent<TutorialDoor>().OpenDoor();
        door.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        Camera.SetActive(false);
        StartCoroutine(startEnemies());
        BeatGenerator.ToggleBeatSystem(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && first)
        {
            first = false;
            canv.GetComponent<Animator>().SetTrigger("StartCutScene");

            // Disable Player Controls
            // PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
            // movement.OnDisable();
            BeatGenerator.ToggleBeatSystem(false);
            StartCoroutine(WaitForDirections());
        }
    }

    IEnumerator startEnemies()
    {
        yield return new WaitForSeconds(TimeB4Enemy);
        foreach (var enemy in chaseEnemies)
        {
            enemy.GetComponent<ChaseEnemy>().trigger(true);
        }
    }
}

