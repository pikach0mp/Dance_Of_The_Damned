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
public class UnityEventBeatInfo : UnityEvent<BeatInfo> {}
[System.SerializableAttribute]
public class UnityEventBeatInfoFloat : UnityEvent<(BeatInfo, float)> {}
[System.SerializableAttribute]
public class UnityEventBool : UnityEvent<bool> {}

public class BeatGenerator : MonoBehaviour {
	public UnityEventBeatInfo onBeat;
	public UnityEventBeatInfoFloat onBeatAddedToQueue;
	public UnityEventBool onPausePlay;
	public float offset;

	private AudioSource source;
    // private AudioSource music;

    // Specifies time between beats
    public AudioTrack track;
	private int loops;

	// Cache beats for lookAheadTime seconds
	public double lookAheadTime;

	private Queue<(BeatInfo, float)> times;
	private float lastTimeAdded;
	private int nextPattern;

    private static BeatGenerator instance; 

    private double startTime;
    private bool running;
    private float prevClipTime;

    private bool generateBeats;

	public static float GetTime() {
		return (float)(AudioSettings.dspTime - instance.offset - instance.startTime);
	}

	void Awake()
    {
        instance = this;
        running = false;
        generateBeats = true;
		times = new Queue<(BeatInfo, float)>();
		source = GetComponent<AudioSource>();
		source.clip = track.audio;
	}

	void Update() {

		if(!running) {
			running = true;
			startTime = AudioSettings.dspTime + 2;
			lastTimeAdded = 0;
			nextPattern = 0;
			loops = 0;
			source.PlayScheduled(startTime);
		}

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {

			(float, BeatInfo) next = track.Get(nextPattern);
			lastTimeAdded = next.Item1 + track.audio.length * loops;


			if(generateBeats) {
				times.Enqueue((next.Item2, lastTimeAdded));
				onBeatAddedToQueue.Invoke((next.Item2, lastTimeAdded));
			}

			nextPattern++;
			if(nextPattern == track.NumBeats()) {
				loops++;
				nextPattern = 0;
			}
		}

		if(times.Count > 0 && times.Peek().Item2 < BeatGenerator.GetTime()) {
			onBeat.Invoke(times.Dequeue().Item1);
		}
	}

	// public static int GetLevel() {
	// 	return instance._GetLevel();
	// }

	// public static bool SetLevel(int newLevel) {
	// 	return instance._SetLevel(newLevel);
	// }

	// private int _GetLevel() {
	// 	return level;
	// }

	public static void ToggleBeatSystem(bool play) {
		instance.onPausePlay.Invoke(play);
	}

	// private bool _SetLevel(int newLevel) {
	// 	if(newLevel < 0 || newLevel >= beatList.Length) {
	// 		return false;
	// 	}

	// 	level = newLevel;

	// 	float time = lastTimeAdded % beatList[level].getLength();
	// 	float totalTime = 0;

	// 	nextPattern = 0;

	// 	for(int i = 0; i<beatList[level].pattern.Length; ++i) {			
	// 		if(totalTime >= time || Mathf.Abs(totalTime-time)< 1e-2) {
	// 			nextPattern = i;
	// 			break;
	// 		}
	// 		totalTime += beatList[level].pattern[i];
	// 	}

	// 	return true;
	// }

	public void ToggleBeats(bool play) {
		generateBeats = play;

		if(!play) {
			times.Clear();
		}
	}

	public void PrintBeat(BeatInfo info) {
		Debug.Log(GetTime()+", "+info.noteInPattern+", "+info.proportionalLocation);
    }

    public void setAudioVolume(int vol)
    {
        source.volume = vol;
    }
}
