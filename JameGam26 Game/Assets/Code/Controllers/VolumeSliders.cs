using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSliders : MonoBehaviour
{   
    public AudioMixer mixer;
    
    public void SetMusicVolume(float volume) {
        mixer.SetFloat("MusicVolume", volume);
    }

    public void SetAudioVolume(float volume) {
        mixer.SetFloat("AudioVolume", volume);
    }


}
