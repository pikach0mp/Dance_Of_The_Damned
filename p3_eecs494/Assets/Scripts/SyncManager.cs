using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SyncManager : MonoBehaviour {
    public float sync_bpm;

    private float[] offsets;

    private AudioSource source;
    private double startTime;

    private int i;

    void Awake() {
        startTime = AudioSettings.dspTime + 2;
        source = GetComponent<AudioSource>();
        source.PlayScheduled(startTime);

        offsets = new float[10];
        i=0;
    }

    void Update() {
        float t = (float)(AudioSettings.dspTime - startTime);
        float freq = 60/sync_bpm;

        Gamepad gp = Gamepad.current;

        if(Input.GetKeyDown(KeyCode.Space) || (gp!=null && gp.buttonSouth.wasPressedThisFrame)) {
            float offset = t % freq;
            if(offset > freq*9/10) {
                offset -= freq;
            }
            offsets[i++] = offset;
            if(i==10) {
                i=0;
                float mean = 0;
                foreach(float f in offsets) {
                    mean += f;
                }
                mean /= offsets.Length;

                float std = 0;
                foreach(float f in offsets) {
                    std += Mathf.Pow(f-mean, 2);
                }
                std /= (offsets.Length - 1);
                std = Mathf.Sqrt(std);

                // Good cutoff point
                if(std <= 0.04F) {
                    PlayerPrefs.SetFloat("AudioOffset", mean);
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
}

