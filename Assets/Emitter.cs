using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnCollisionEnter(Collision c)
    {

    }

    private void OnCollisionExit(Collision c)
    {

    }
}

public enum EmitterState { Inactive, Push, Pull };

