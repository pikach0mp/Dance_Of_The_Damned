using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;

    public GameObject player;

    public GameObject enemyLevel1;
    public GameObject enemy1Level2;
    public GameObject enemy2Level2;


    private Animator anim;
	private Vector3 target_pos;
    public bool isTutorial;
    private bool activating;


    void Start()
    {
        transform.position = player.transform.position;
    }
    private void Update()
    {
        if(activating)
        {
            Vector3 currentScale = enemyLevel1.transform.GetChild(11).transform.localScale;
            enemyLevel1.transform.GetChild(11).transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, Time.deltaTime * .6f);

        }
        
    }

    public IEnumerator TransitionFrom(int level)
    {
        switch (level)
        {
            case 1:
                Level2.SetActive(true);

                GetComponent<Animator>().SetTrigger("Level1Transition");
                yield return new WaitForSeconds(3.0f);
                activating = true;

                yield return new WaitForSeconds(3.5f);
                activating = false;

                door2.GetComponent<DoorController>().OpenDoor();
                door1.SetActive(true);
                break;

            case 2:
                Level3.SetActive(true);

                GetComponent<Animator>().SetTrigger("Level2Transition");
                enemy1Level2.transform.GetChild(11).gameObject.SetActive(false);
                enemy2Level2.transform.GetChild(11).gameObject.SetActive(false);
                yield return new WaitForSeconds(10.5f);

                door3.GetComponent<DoorController>().OpenDoor();
                door2.SetActive(true);
                yield return new WaitForSeconds(5f);
                door3.SetActive(true);

                break;
            default:
                yield return 0;
                break;
        }
    }

    public void OnDisable()
    {
    	Debug.Log("END");
        BeatGenerator.ToggleBeatSystem(true);
        this.gameObject.SetActive(false);
    }
}
