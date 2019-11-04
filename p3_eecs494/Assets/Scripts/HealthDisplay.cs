using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private Health UnitHealth;

    // Start is called before the first frame update
    void Start()
    {
        UnitHealth = GetComponentInParent<Health>();
        for(int i = 0; i < UnitHealth.get_health(); i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
