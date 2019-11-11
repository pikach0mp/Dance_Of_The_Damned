using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HideTextAfterSeconds : MonoBehaviour
{
    public float time = 5;
    public bool hide = true;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        if (hide)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Text>().text = "[WASD]+[space] -> move \n" +
                                                    "[ . ] -> attack \n" +
                                                    "[ r ] -> reset";
        }
    }
}
