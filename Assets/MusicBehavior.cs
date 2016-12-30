using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour {

    FMODUnity.StudioEventEmitter emitter;

    void Start() {
		emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        SetVolume(PlayerPrefs.GetFloat("Volume"));
    }

    public void goFast() {
		emitter.SetParameter ("intensity", 1f);
    }

    public void goSlow() {
		emitter.SetParameter ("intensity", 0f);
    }

    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("Volume", vol);
        GameObject.FindGameObjectWithTag("Global").GetComponent<AudioSource>().volume = 0.1f * vol / 3.0f;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("volumeMusique", vol / 6f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().SetVolume(vol / 3.0f);
    }

}
