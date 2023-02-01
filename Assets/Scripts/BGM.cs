using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour{

    public AudioSource AudioSource;
    private float musicVolume = 1f;
    public static float staticVol = 1f;

    // Start is called before the first frame update
    void Start(){
        AudioSource.Play();
        AudioSource.loop = true;
    }

    // Update is called once per frame
    void Update(){
        AudioSource.volume = musicVolume;
    }


    public void updateVolume(float volume){
        musicVolume = volume;
        staticVol = volume;
    }

}
