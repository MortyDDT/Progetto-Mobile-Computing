using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBGM : MonoBehaviour
{
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start() {
        AudioSource.Play();
        AudioSource.loop = true;
    }

    private void Update() {
        AudioSource.volume = BGM.staticVol;
    }
}
