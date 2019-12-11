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

	public float audioVolume;

	private int currentAudio;
	private AudioSource[] audioSources;

    public AudioSource upgrade;
    public AudioSource downgrade;
    // private AudioSource music;

    // Specifies time between beats
    public AudioTrack[] tracks;
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

		audioSources = new AudioSource[tracks.Length];
		for(int i=0; i<tracks.Length;i++) {
			audioSources[i] = gameObject.AddComponent<AudioSource>();
			audioSources[i].volume = 0;
			audioSources[i].clip = tracks[i].audio;
			audioSources[i].loop = true;
		}
		audioSources[0].volume = 1;
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

			foreach(AudioSource source in audioSources) {
				source.PlayScheduled(startTime);
			}	
		}

        while (lastTimeAdded < BeatGenerator.GetTime() + lookAheadTime) {

			(float, BeatInfo) next = tracks[currentAudio].Get(level, nextPattern);
			Debug.Assert(next.Item1 != -1);

			lastTimeAdded = next.Item1 + tracks[0].audio.length * loops;

			if(generateBeats) {
				times.Enqueue((next.Item2, lastTimeAdded));
				onBeatAddedToQueue.Invoke((next.Item2, lastTimeAdded));
			}

			nextPattern++;
			if(nextPattern == tracks[currentAudio].NumBeats(level)) {
				loops++;
				nextPattern = 0;
			}
		}

		if(times.Count > 0 && times.Peek().Item2 < BeatGenerator.GetTime()) {
			onBeat.Invoke(times.Dequeue().Item1);
		}
	}

	public static void CrossFadeAudio(int toTrack) {
		instance.StartCoroutine(instance._CrossFadeAudio(toTrack, instance.audioVolume));
	}

	private IEnumerator _CrossFadeAudio(int toTrack, float toVolume) {
		float t = 0;
		float currentVolume = audioSources[currentAudio].volume;
		while(t < 1) {
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
			audioSources[currentAudio].volume = Mathf.Max(currentVolume - currentVolume*t, 0);
			audioSources[toTrack].volume = Mathf.Min(toVolume*t, toVolume);
			Debug.Log(t);
		}
		currentAudio = toTrack;
	}

	public static void StartAudio(int i) {
		StopAudio();
		CrossFadeAudio(i);
	}

	public static void StopAudio() {
		instance.audioSources[instance.currentAudio].volume = 0;
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
		if(newLevel < 0 || newLevel >= tracks[currentAudio].NumLevels() || newLevel == level) {
			return false;
		}

        if (isLoss == 0)
        {
            // Debug.Log("upgrade");
            upgrade.Play();
            StartCoroutine(Scale(true));
        }
        else if (isLoss == 1)
        {
            downgrade.Play();
            // Debug.Log("downgrade");
            StartCoroutine(Scale(false));
        }
        else
        {
            Debug.Log("neither");
        }


        int prevLevel = level;
		int prevPattern = nextPattern - 1;

        Debug.Log(prevPattern);

		level = newLevel;
        nextPattern = tracks[currentAudio].FindNextBeat(level, (lastTimeAdded + 0.3F) % tracks[currentAudio].audio.length);

        Debug.Log(nextPattern);

        if(prevPattern != -1) {
	        float prevTime = tracks[currentAudio].Get(prevLevel, prevPattern).Item1;
			float nextTime = tracks[currentAudio].Get(level, nextPattern).Item1;
        	Debug.Log(prevTime);
        	Debug.Log(nextTime);

			if(prevTime > nextTime) {
				loops++;
			}
		} else {
			nextPattern = 0;
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
}
