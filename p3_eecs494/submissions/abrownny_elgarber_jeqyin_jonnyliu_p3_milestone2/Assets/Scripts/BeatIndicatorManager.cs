using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatIndicatorManager : MonoBehaviour
{

    public GameObject indicatorPrefab;
    public Canvas canvas;
    public RectTransform target;
    private Stack<BeatIndicator> availableIndicators;
    private Queue<BeatIndicator> indicatorsInUse;

    void Awake() {
        availableIndicators = new Stack<BeatIndicator>();
        indicatorsInUse = new Queue<BeatIndicator>();
    }

    public void OnBeatSpawn((BeatInfo, float) info) {
        BeatIndicator indicator;
        if(availableIndicators.Count > 0) {
            indicator = availableIndicators.Pop();
            indicator.gameObject.SetActive(true);
        } else {
            indicator = Instantiate<GameObject>(indicatorPrefab, canvas.transform).GetComponent<BeatIndicator>();
        }

        indicator.SetHitTime(info.Item2);
        indicator.SetTarget(target);
        indicator.UpdatePos();
        indicatorsInUse.Enqueue(indicator);
    }
    
    public void OnBeatHit((ButtonPress, BeatInfo) info) {
        BeatIndicator hit = indicatorsInUse.Dequeue();
        hit.Hit();
        // Debug.Log("Hit");
        StartCoroutine(addToAvailable(hit));
    }
    
    public void OnBeatMissed(BeatInfo info) {
        BeatIndicator missed = indicatorsInUse.Dequeue();
        missed.Missed();
        // Debug.Log(missed.hitTime - BeatGenerator.GetTime());
        // Debug.Log("Miss");
        StartCoroutine(addToAvailable(missed));
    }

    private IEnumerator addToAvailable(BeatIndicator indicator) {
        yield return new WaitForSeconds(2);
        indicator.gameObject.SetActive(false);
        indicator.Reset();
        availableIndicators.Push(indicator);
    }
}
