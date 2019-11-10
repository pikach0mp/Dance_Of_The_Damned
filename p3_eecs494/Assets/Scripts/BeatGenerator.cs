using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public struct BeatInfo {
	public int noteInPattern;
	public float proportionalLocation;
}

[System.SerializableAttribute]
public class SongList {
	public float[] pattern;

	public float noteProportion(int note) {
		float t = 0;
		for(int i =0; i < pattern.Length; ++i) {
			if(note == i) {
				return t/getLength();
			}
			t += pattern[i];
		}
		return 1;
	}

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

	private Queue<(BeatInfo, float)> times;
	private float lastTimeAdded;
	private int nextPattern;

    private static BeatGenerator instance; 

    private double startTime;
    private bool running;
    private float prevClipTime;

	public static float GetTime() {
		return (float)(AudioSettings.dspTime - instance.offset - instance.startTime);
	}

	void Awake()
    {
        instance = this;
        running = false;
		times = new Queue<(BeatInfo, float)>();
		source = GetComponents<AudioSource>()[0];
	}

	void Update() {
		// There was a loop
		if(prevClipTime > source.time) {
			float diff = source.clip.length % beatList[level].getLength();
			if(diff > beatList[level].getLength()/2) {
				diff -= beatList[level].getLength();
			}
			offset += diff;
		}
		prevClipTime = source.time;

		if(!running) {
			running = true;
			startTime = AudioSettings.dspTime + 2;
			nextPattern = 0;
			lastTimeAdded = 0;
			source.PlayScheduled(startTime);
		}

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {

			lastTimeAdded += beatList[level].pattern[nextPattern];

			BeatInfo info;
			info.noteInPattern = nextPattern;
			info.proportionalLocation = beatList[level].noteProportion(nextPattern);

			times.Enqueue((info, lastTimeAdded));
			onBeatAddedToQueue.Invoke((info, lastTimeAdded));

			nextPattern++;
			if(nextPattern == beatList[level].pattern.Length) {
				nextPattern = 0;
			}
		}

		if(times.Count > 0 && times.Peek().Item2 < BeatGenerator.GetTime()) {
			onBeat.Invoke(times.Dequeue().Item1);
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

	public void PrintBeat(BeatInfo info) {
		Debug.Log(GetTime()+", "+info.noteInPattern+", "+info.proportionalLocation);
    }

    public void setAudioVolume(int vol)
    {
        source.volume = vol;
    }
}
