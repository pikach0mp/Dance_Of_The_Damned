using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;

[System.SerializableAttribute]
public class UnityEventQueueFloat : UnityEvent<Queue<float>> {}


public class BeatGenerator : MonoBehaviour {
	public UnityEvent onBeat;
	public UnityEventQueueFloat onBeatListUpdated;

    public GameObject indicator;

	// Specifies time between beats
	public float[] pattern;

	// Cache beats for lookAheadTime seconds
	public float lookAheadTime;

	private Queue<float> times;
	private float lastTimeAdded;
	private int nextPattern;

	void Awake() {
		times = new Queue<float>();
		lastTimeAdded = Time.timeSinceLevelLoad;
	}

	void Update() {
		bool beatListChanged = false;
		while(lastTimeAdded < Time.timeSinceLevelLoad + lookAheadTime) {
			beatListChanged = true;
			lastTimeAdded += pattern[nextPattern];

			nextPattern++;
			if(nextPattern == pattern.Length) {
				nextPattern = 0;
			}

			times.Enqueue(lastTimeAdded);
		}

		if(beatListChanged) {
			onBeatListUpdated.Invoke(times);
		}

		if(times.Peek() < Time.timeSinceLevelLoad) {
			times.Dequeue();
			onBeat.Invoke();
		}
	}

	public void PrintBeat() {
        StartCoroutine(changeColor());
		Debug.Log(Time.timeSinceLevelLoad);
    }

    private IEnumerator changeColor()
    {
        indicator.GetComponent<RawImage>().color = new Color32(255, 86, 86, 255);
        yield return new WaitForSeconds(0.1f);
        indicator.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
    }
}
