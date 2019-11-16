using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Audio/AudioTrack", order = 1)]
public class AudioTrack : ScriptableObject
{
    public AudioClip audio;
    public TextAsset beatData;

    private (float, BeatInfo)[] beats;

    public void OnValidate() {
    	if(beatData == null) {
    		beats = new (float, BeatInfo)[0];
    		return;
    	}
    	string[] text = beatData.text.Split('\n');
    	beats = new (float, BeatInfo)[text.Length-1];
    	int i=0;
    	foreach(string line in text) {
    		if(line == "") {
    			break;
    		}
    		string[] parts = line.Split(' ');
    		BeatInfo info = new BeatInfo();
    		info.noteInPattern = int.Parse(parts[1]);
    		beats[i++] = (float.Parse(parts[0]), info);
    	}

    	int measureStartN=0, measureEndN=0;
    	float measureStartT=0, measureEndT=0;
    	for(i=0; i<beats.Length; ++i) {
    		if(measureEndN == i) {
    			measureStartN = measureEndN;
    			measureStartT = beats[i].Item1;
    			for (int j = i+1; j < beats.Length; ++j) {
    				if(beats[j].Item2.noteInPattern == 1) {
    					measureEndN = j;
    					measureEndT = beats[measureEndN].Item1;
    					break;
    				}
    			}

    			if(measureStartN == measureEndN) {
    				measureEndT = audio.length;
    			}
    		}

    		BeatInfo info = beats[i].Item2;
    		info.proportionalLocation = (beats[i].Item1 - measureStartT)/(measureEndT - measureStartT);
    		beats[i].Item2 = info;
    	}

    	Debug.Assert(i == beats.Length);
    }

    public (float, BeatInfo) Get(int i) {
    	if (i >= beats.Length || i < 0) {
    		return (-1,new BeatInfo{});
    	}
    	return beats[i];
    }

    public int NumBeats() {
    	return beats.Length;
    }
}