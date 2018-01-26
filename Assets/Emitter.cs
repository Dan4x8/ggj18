using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Emitter : MonoBehaviour {

    public AudioSource AudioSource;
    private float _range;

    public EmitterState State = EmitterState.Inactive;

	// Use this for initialization
	void Start () {
        _range = AudioSource.maxDistance;
	}


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().RegisterEmitter(this);
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Player>().UnregisterEmitter(this);
    }
}

public enum EmitterState { Inactive = 0, Push = -1, Pull = 1 };

