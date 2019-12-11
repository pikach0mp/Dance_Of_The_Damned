using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject healthDisplay;
    public GameObject cam;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        setDisplay();
    }

    public void update_health(int change)
    {
        cam.GetComponent<TakeDamageVisual>().TriggerShake();
        blood.GetComponent<AudioSource>().Play();
        StartCoroutine(FadeImage());
        health += change;
        setDisplay();
    }

    public void ResetHealth()
    {
        health = 3;
        setDisplay();
    }

    public int get_health()
    {
        return health;
    }

    public bool dead()
    {
        if(health == 0)
        {
            return true;
        }
        return false;
    }

    private void setDisplay()
    {
        for (int i = 0; i < healthDisplay.transform.childCount; i++)
        {
            if (i < health)
            {
                healthDisplay.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                healthDisplay.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator FadeImage()
    {
        // fade from transparent to opaque
            // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime*3)
        {
            // set color with i as alpha
            blood.GetComponent<RawImage>().color = new Color(1, 1, 1, i);
            yield return null;
        }
        // fade from opaque to transparent
            // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime*2)
        {
            // set color with i as alpha
            blood.GetComponent<RawImage>().color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
