using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginChase : MonoBehaviour
{
    public GameObject door;
    public GameObject nextDoor;
    public GameObject Camera;
    public List<GameObject> chaseEnemies;

    private LevelTransitions transitions;
    public int level = 1;

    private void Start()
    {
        transitions = Camera.GetComponent<LevelTransitions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Disable Player Controls
            // PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
            // movement.OnDisable();
            BeatGenerator.ToggleBeatSystem(false);

            door.SetActive(true);

            //Cut Scene
            Camera.SetActive(true);
            transitions.TransitionFrom(level);
            nextDoor.GetComponent<DoorController>().OpenDoor();

            //Re - Enable Player Controls
            // movement.OnEnable();

            //enable enemies
            foreach(var enemy in chaseEnemies)
            {
                enemy.GetComponent<ChaseEnemy>().trigger(true);
            }
        }
    }
}
