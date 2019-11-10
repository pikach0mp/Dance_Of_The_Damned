using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public struct BeatInfo {

}

[System.SerializableAttribute]
public class SongList {
	public float[] pattern;

	public float getLength() {
		float len = 0;
		foreach(float f in pattern) {
			len += f;
		}
		return len;
	}
}

[System.SerializableAttribute]
public class UnityEventBeatInfo : UnityEvent<BeatInfo> {}
[System.SerializableAttribute]
public class UnityEventBeatInfoFloat : UnityEvent<(BeatInfo, float)> {}

public class BeatGenerator : MonoBehaviour {
	public UnityEventBeatInfo onBeat;
	public UnityEventBeatInfoFloat onBeatAddedToQueue;
	public float offset;

	private AudioSource source;
    private AudioSource music;

    // Specifies time between beats
    public SongList[] beatList;
    public int level;

	// Cache beats for lookAheadTime seconds
	public double lookAheadTime;

	private Queue<float> times;
	private float lastTimeAdded;
	private int nextPattern;

    private static BeatGenerator instance; 

    private double startTime;
    private bool running;

	public static float GetTime() {
		return (float)(AudioSettings.dspTime - instance.offset - instance.startTime);
	}

	void Awake()
    {
        instance = this;
        running = false;
		times = new Queue<float>();
		source = GetComponents<AudioSource>()[0];
	}

	void Update() {
		if(!running) {
			running = true;
			startTime = AudioSettings.dspTime + 2;
			nextPattern = 0;
			lastTimeAdded = 0;
			source.PlayScheduled(startTime);
		}

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {

			lastTimeAdded += beatList[level].pattern[nextPattern];

			times.Enqueue(lastTimeAdded);
			onBeatAddedToQueue.Invoke((new BeatInfo{}, lastTimeAdded));

			nextPattern++;
			if(nextPattern == beatList[level].pattern.Length) {
				nextPattern = 0;
			}
		}

		if(times.Count > 0 && times.Peek() < BeatGenerator.GetTime()) {
			times.Dequeue();
			onBeat.Invoke(new BeatInfo{});
		}
	}

	public static int GetLevel() {
		return instance._GetLevel();
	}

	public static bool SetLevel(int newLevel) {
		return instance._SetLevel(newLevel);
	}

	private int _GetLevel() {
		return level;
	}

	private bool _SetLevel(int newLevel) {
		if(newLevel < 0 || newLevel >= beatList.Length) {
			return false;
		}

		level = newLevel;

		float time = lastTimeAdded % beatList[level].getLength();
		float totalTime = 0;

		nextPattern = 0;

		for(int i = 0; i<beatList[level].pattern.Length; ++i) {			
			if(totalTime >= time || Mathf.Abs(totalTime-time)< 1e-2) {
				nextPattern = i;
				break;
			}
			totalTime += beatList[level].pattern[i];
		}

		return true;
	}

	public void PrintBeat() {
		Debug.Log(BeatGenerator.GetTime());
    }

    public void setAudioVolume(int vol)
    {
        source.volume = vol;
    }
}
