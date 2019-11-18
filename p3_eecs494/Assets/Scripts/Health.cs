using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject healthDisplay;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        setDisplay();
    }

    public void update_health(int change)
    {
        cam.GetComponent<TakeDamageVisual>().TriggerShake();
        health += change;
        setDisplay();
    }

    public void ResetHealth()
    {
        health = 3;
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
}
