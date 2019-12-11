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
    public AudioSource upgrade;
    public AudioSource downgrade;
    // private AudioSource music;

    // Specifies time between beats
    public AudioTrack track;
	private int loops;

	// Cache beats for lookAheadTime seconds
	public double lookAheadTime;

	private Queue<(BeatInfo, float)> times;
	private float lastTimeAdded;
	private int nextPattern = 0;

    private static BeatGenerator instance; 

    private double startTime;
    private bool running;
    private float prevClipTime;

    private int level = 0;

    private bool generateBeats;

    public GameObject combo;
    float growFactor = 2f;
    float maxSize = 1.1f;
    float minSize = 0.9f;
    float waitTime = 0.1f;

    public static float GetTime() {
		return (float)(AudioSettings.dspTime - instance.offset - instance.startTime);
	}

	void Awake()
    {
        Debug.Log("here");

        instance = this;
        running = false;
        generateBeats = true;
		times = new Queue<(BeatInfo, float)>();
		source = GetComponent<AudioSource>();
		source.clip = track.audio;
		level = 0;

		offset = PlayerPrefs.GetFloat("AudioOffset", 0);
	}

	void Update() {

		if(!running) {
			running = true;
			startTime = AudioSettings.dspTime + 1F;
			lastTimeAdded = 0;
			nextPattern = 0;
			loops = 0;
			source.PlayScheduled(startTime);
		}

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {

			(float, BeatInfo) next = track.Get(level, nextPattern);
			Debug.Assert(next.Item1 != -1);

			lastTimeAdded = next.Item1 + track.audio.length * loops;

			if(generateBeats) {
				times.Enqueue((next.Item2, lastTimeAdded));
				onBeatAddedToQueue.Invoke((next.Item2, lastTimeAdded));
			}

			nextPattern++;
			if(nextPattern == track.NumBeats(level)) {
				loops++;
				nextPattern = 0;
			}
		}

		if(times.Count > 0 && times.Peek().Item2 < BeatGenerator.GetTime()) {
			onBeat.Invoke(times.Dequeue().Item1);
		}
	}

	public static void SwitchTrack(AudioTrack track, bool dontResetTime) {
		if(dontResetTime) {
			Debug.Assert(instance.source.clip.length == track.audio.length);
			float prevTime = instance.source.time;
			instance.track = track;
			instance.source.clip = track.audio;
			instance.source.Stop();
			instance.source.Play();
			instance.source.time = prevTime;
		}else {
			instance.source.Stop();
			instance.running = false;
			instance.track = track;

			// Reset using a toggle off and on
			ToggleBeatSystem(false);
			ToggleBeatSystem(true);
		}
	}

	public static int GetLevel() {
		return instance._GetLevel();
	}

    public static bool SetLevel(int newLevel, int isLoss) {
		return instance._SetLevel(newLevel, isLoss);
	}

	private int _GetLevel() {
		return level;
	}

    IEnumerator Scale(bool up)
    {
        float timer = 0;
        RectTransform comboTransform = combo.GetComponent<RectTransform>();

        // we scale all axis, so they will have the same value, 
        // so we can work with a float instead of comparing vectors
        if (up)
        {
            while (maxSize > comboTransform.localScale.x)
            {
                timer += Time.deltaTime;
                comboTransform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (1 < comboTransform.localScale.x)
            {
                timer += Time.deltaTime;
                comboTransform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
        }
        else
        {
            while (minSize < comboTransform.localScale.x)
            {
                timer += Time.deltaTime;
                comboTransform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (1 > comboTransform.localScale.x)
            {
                timer += Time.deltaTime;
                comboTransform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
        }

    }

    private bool _SetLevel(int newLevel, int isLoss) {
		if(newLevel < 0 || newLevel >= track.NumLevels() || newLevel == level) {
			return false;
		}


		int prevPattern = nextPattern - 1;
		if(prevPattern < 0) {
			prevPattern += track.NumBeats(level);
		}

        if (isLoss == 0)
        {
            Debug.Log("upgrade");
            upgrade.Play();
            StartCoroutine(Scale(true));
        }
        else if (isLoss == 1)
        {
            downgrade.Play();
            Debug.Log("downgrade");
            StartCoroutine(Scale(false));
        }
        else
        {
            Debug.Log("neither");
        }

        float prevTime = track.Get(level, prevPattern).Item1;

		level = newLevel;
        nextPattern = track.FindNextBeat(level, (lastTimeAdded + 0.3F) % track.audio.length);

		float nextTime = track.Get(level, nextPattern).Item1;

		if(prevTime > nextTime) {
			loops++;
		}

		return true;
	}

	public static void ToggleBeatSystem(bool play) {
		instance.onPausePlay.Invoke(play);
	}

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
