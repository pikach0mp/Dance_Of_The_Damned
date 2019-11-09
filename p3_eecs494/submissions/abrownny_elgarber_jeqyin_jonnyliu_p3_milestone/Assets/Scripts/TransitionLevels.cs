using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitionLevels : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
	public BeatGenerator beatGenerator;

    public AudioSource source;
	public GameObject gameOverText;

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
	}

    private void OnTriggerEnter(Collider other)
    {

		if (other.gameObject.tag == "Player")
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Level0")
            {
                FadeToLevel(1);
            }
            if (scene.name == "Level1")
            {
                FadeToLevel(2);
            }
            if (scene.name == "Level2")
            {
                gameOverText.SetActive(true);
                source.Play();
            }
        }
    }

    public void OnFadeOutComplete()
	{
        string level = "Level" + levelToLoad.ToString();
		SceneManager.LoadScene(level); 
    }

	public void OnFadeInComplete()
	{
		beatGenerator.setAudioVolume(1);
	}

}
