using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PreTutorialTriggeredEvents : MonoBehaviour
{
    public DoorController door;
    public ChaseEnemy enemy;
    bool activating;
    bool deactivating;
    Vector3 originalScale;

    private void Start()
    {
        originalScale = enemy.transform.GetChild(11).transform.localScale;
    }

    public void OnCloseDoor()
    {
        door.CloseDoor();
    }

    public void StartEnemies()
    {

        activating = true;
    }

    public void EndEnemies()
    {
        activating = false;
        deactivating = true;
    }

    public void TransitionToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void Update()
    {
        if (activating)
        {
            Vector3 currentScale = enemy.transform.GetChild(11).transform.localScale;
            enemy.transform.GetChild(11).transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, Time.deltaTime * .6f);
        }

        if (deactivating)
        {
            Vector3 currentScale = enemy.transform.GetChild(11).transform.localScale;
            enemy.transform.GetChild(11).transform.localScale = Vector3.Lerp(currentScale, originalScale, Time.deltaTime * .6f);
        }
    }
}
