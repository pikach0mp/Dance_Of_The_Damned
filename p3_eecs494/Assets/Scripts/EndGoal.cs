using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//create 
public class EndGoal : MonoBehaviour
{
    public GameObject Canvas;

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            BeatGenerator.ToggleBeatSystem(false);
            StartCoroutine(CompleteGame());
        }
    }

    private IEnumerator CompleteGame()
    {
        Animator anim = Canvas.GetComponent<Animator>();
        anim.Play("EndGame");
        ManageGame.ResetLevel();
        yield return new WaitForSeconds(12.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
