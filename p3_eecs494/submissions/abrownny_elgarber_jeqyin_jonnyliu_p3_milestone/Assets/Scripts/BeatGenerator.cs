using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct BeatInfo {

}

[System.SerializableAttribute]
public class UnityEventBeatInfo : UnityEvent<BeatInfo> {}
[System.SerializableAttribute]
public class UnityEventBeatInfoFloat : UnityEvent<(BeatInfo, float)> {}

public class BeatGenerator : MonoBehaviour {
	public UnityEventBeatInfo onBeat;
	public UnityEventBeatInfoFloat onBeatAddedToQueue;
	private bool running;

	private AudioSource source;
    private AudioSource music;

    // Specifies time between beats
    public float[] pattern;

	// List of good times to sync music to
	public float[] syncPoints;

	// Cache beats for lookAheadTime seconds
	public float lookAheadTime;

	private Queue<float> times;
	private float lastTimeAdded;
	private int nextPattern;

	private static float time;

	public static float GetTime() {
		return time;
	}

	void Awake() {
		running = false;
		times = new Queue<float>();
		source = GetComponents<AudioSource>()[0];
        music = GetComponents<AudioSource>()[1];

        //music.Play();
	}

	public void StartBeats() {
		running = true;
		lastTimeAdded = BeatGenerator.GetTime() + 4 - 0.2F;
		source.Play();
        source.time = BeatGenerator.GetTime() % source.clip.length;
		StartCoroutine(sync());
	}

	private IEnumerator sync() {
		int currentSyncPoint = 0;
		while(true) {
			if( (currentSyncPoint != 0 || (BeatGenerator.GetTime() % source.clip.length) < syncPoints[0] + 0.2F) &&
				BeatGenerator.GetTime() % source.clip.length > syncPoints[currentSyncPoint]) {
				Debug.Log("Syncing to" + (BeatGenerator.GetTime() % source.clip.length) + " was " + source.time );
				source.time = BeatGenerator.GetTime() % source.clip.length;
				currentSyncPoint++;

				if(currentSyncPoint == syncPoints.Length) {
					currentSyncPoint=0;
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void Update() {
		time += Time.deltaTime;
        if (!running)
        {
            StartBeats();
        }

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {
			lastTimeAdded += pattern[nextPattern];

			nextPattern++;
			if(nextPattern == pattern.Length) {
				nextPattern = 0;
			}

			times.Enqueue(lastTimeAdded);
			onBeatAddedToQueue.Invoke((new BeatInfo{}, lastTimeAdded));
		}

		if(times.Peek() < BeatGenerator.GetTime()) {
			times.Dequeue();
			onBeat.Invoke(new BeatInfo{});
		}
	}

	public void PrintBeat() {
		Debug.Log(BeatGenerator.GetTime());
    }

    public void setAudioVolume(int vol)
    {
        source.volume = vol;
    }
}
