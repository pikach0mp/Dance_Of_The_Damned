using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Audio/AudioTrack", order = 1)]
public class AudioTrack : ScriptableObject
{
    public AudioClip audio;
    public TextAsset[] beatData;

    private (float, BeatInfo)[][] beats;

    public void OnValidate() {
        beats = new (float, BeatInfo)[beatData.Length][];
        for(int track_num = 0; track_num < beats.Length; track_num++) {
        	string[] text = beatData[track_num].text.Split('\n');
        	beats[track_num] = new (float, BeatInfo)[text.Length-1];
        	int i=0;
        	foreach(string line in text) {
        		if(line == "") {
        			break;
        		}
        		string[] parts = line.Split(' ');
        		BeatInfo info = new BeatInfo();
        		info.noteInPattern = int.Parse(parts[1]);
        		beats[track_num][i++] = (float.Parse(parts[0]), info);
        	}

        	int measureStartN=0, measureEndN=0;
        	float measureStartT=0, measureEndT=0;
        	for(i=0; i<beats[track_num].Length; ++i) {
        		if(measureEndN == i) {
        			measureStartN = measureEndN;
        			measureStartT = beats[track_num][i].Item1;
        			for (int j = i+1; j < beats.Length; ++j) {
        				if(beats[track_num][j].Item2.noteInPattern == 1) {
        					measureEndN = j;
        					measureEndT = beats[track_num][measureEndN].Item1;
        					break;
        				}
        			}

        			if(measureStartN == measureEndN) {
        				measureEndT = audio.length;
        			}
        		}

        		BeatInfo info = beats[track_num][i].Item2;
        		info.proportionalLocation = (beats[track_num][i].Item1 - measureStartT)/(measureEndT - measureStartT);
        		beats[track_num][i].Item2 = info;
        	}

        	Debug.Assert(i == beats[track_num].Length);
        }
    }

    public int FindNextBeat(int level, float time) {
        for(int i=0; i<beats[level].Length; ++i) {
            if(beats[level][i].Item1 > time) {
                return i;
            }
        }
        return 0;
    }

    public (float, BeatInfo) Get(int level, int i) {
        if (level >= beats.Length || level < 0 || i >= beats[level].Length || i < 0) {
            Debug.Log("returning from here");
            //return (-1,new BeatInfo{});
    	}
    	return beats[level][i];
    }

    public int NumLevels() {
        return beats.Length;
    }

    public int NumBeats(int level) {
    	return beats[level].Length;
    }
}