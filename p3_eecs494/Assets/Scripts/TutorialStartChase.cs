using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartChase : MonoBehaviour
{
    public GameObject door;
    public GameObject Camera;
    public List<GameObject> chaseEnemies;
    public float TimeB4Enemy = 1;

    private bool first = true;

    IEnumerator WaitForLookingAtDoor()
    {
        yield return new WaitForSeconds(4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && first)
        {
            first = false;
            // Disable Player Controls
            // PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
            // movement.OnDisable();
            BeatGenerator.ToggleBeatSystem(false);

            //Cut Scene
            Camera.SetActive(true);

            Camera.GetComponent<Animator>().SetTrigger("triggered");
            StartCoroutine(WaitForLookingAtDoor());

            door.GetComponent<DoorController>().OpenDoor();
            door.SetActive(true);

            //nextDoor.GetComponent<DoorController>().OpenDoor();

            //Re - Enable Player Controls
            // movement.OnEnable();

            //enable enemies
            StartCoroutine(startEnemies());
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

