using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void update_health(int change)
    {
        health += change;
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
}
