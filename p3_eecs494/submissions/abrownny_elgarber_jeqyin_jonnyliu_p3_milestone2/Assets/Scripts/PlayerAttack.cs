using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1000 * Time.deltaTime, 0);
    }

    public IEnumerator spinAttack()
    {
        Debug.Log("spin!");
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "enemy")
        {
            other.gameObject.GetComponent<Health>().update_health(-1);
        }
    }
}
