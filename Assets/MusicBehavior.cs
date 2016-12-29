using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour {

    FMODUnity.StudioEventEmitter emitter;

    void Start() {
		emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void goFast() {
		emitter.SetParameter ("intensity", 1f);
    }

    public void goSlow() {
		emitter.SetParameter ("intensity", 0f);
    }

}
