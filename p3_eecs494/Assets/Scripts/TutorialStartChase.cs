using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartChase : MonoBehaviour
{
    public GameObject door;
    public GameObject Camera;
    public GameObject canv;
    public List<GameObject> chaseEnemies;
    public GameObject chase_enem;
    public float TimeB4Enemy = 1;

    public AudioSource funPart;

    bool activating = false;

    private bool first = true;

    private void Update()
    {
        if (activating)
        {
            Vector3 currentScale = chase_enem.transform.GetChild(11).transform.localScale;
            chase_enem.transform.GetChild(11).transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, Time.deltaTime * .6f);
        }

    }

    IEnumerator PerformCutScene()
    {
        //Cut Scene
        Camera.SetActive(true);

        Camera.GetComponent<Animator>().SetTrigger("triggered");

        yield return new WaitForSeconds(2f);
        activating = true;

        yield return new WaitForSeconds(2.5f);
        activating = false;

        yield return new WaitForSeconds(2f);
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

            funPart.Play();
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

