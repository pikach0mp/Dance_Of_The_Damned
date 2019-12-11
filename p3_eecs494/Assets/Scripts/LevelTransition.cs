using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject door1;
    public GameObject backdoor1;
    public GameObject door2;
    public GameObject backdoor2;

    public float before_enemy_wait;
    public float before_door_wait;

    public List<GameObject> chaseEnemies;

    public bool isTutorial;
    private bool activating;

    private void Update()
    {
        if (activating)
        {
            foreach (var enemy in chaseEnemies)
            {
                Vector3 currentScale = enemy.transform.GetChild(11).transform.localScale;
                enemy.transform.GetChild(11).transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, Time.deltaTime * .6f);
            }
        }

    }

    public IEnumerator PerformCutScene()
    {
        yield return new WaitForSeconds(before_enemy_wait);
        activating = true;

        yield return new WaitForSeconds(before_door_wait);
        activating = false;

        door2.GetComponent<DoorController>().OpenDoor();
        door1.SetActive(true);
        backdoor1.SetActive(false);
        backdoor2.SetActive(true);
    }

    public void OnDisable()
    {
        Debug.Log("END");
        BeatGenerator.ToggleBeatSystem(true);
    }
}
