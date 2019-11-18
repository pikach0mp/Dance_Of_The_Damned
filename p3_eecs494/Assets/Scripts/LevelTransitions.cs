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

    private Animator anim;
	private Vector3 target_pos;

    void Start()
    {
        //transform.position = player.transform.position;
        //transform.position.y += 1;
    }
	
    public IEnumerator TransitionFrom(int level)
    {
        switch (level)
        {
            case 1:
                door1.SetActive(true);
                Level2.SetActive(true);

                GetComponent<Animator>().SetTrigger("Level1Transition");
                yield return new WaitForSeconds(6.5f);

                door2.GetComponent<DoorController>().OpenDoor();
                break;

            case 2:
                Level3.SetActive(true);
                GetComponent<Animator>().SetTrigger("Level2Transition");
                yield return new WaitForSeconds(6f);
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
