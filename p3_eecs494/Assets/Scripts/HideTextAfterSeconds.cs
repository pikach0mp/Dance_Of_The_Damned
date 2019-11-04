using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTextAfterSeconds : MonoBehaviour
{
    public float time = 5;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
