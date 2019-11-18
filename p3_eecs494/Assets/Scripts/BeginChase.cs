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

    private LevelTransitions transitions;
    public int level = 1;
    private bool first = true;

    private void Start()
    {
        transitions = Camera.GetComponent<LevelTransitions>();
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
            door.SetActive(true);

            //Cut Scene
            Camera.SetActive(true);
            StartCoroutine(transitions.TransitionFrom(level));
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
        foreach(var enemy in chaseEnemies)
        {
            enemy.GetComponent<ChaseEnemy>().trigger(true);
        }
    }
}
