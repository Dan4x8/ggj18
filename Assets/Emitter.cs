using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

    public AudioSource AudioSource;
    private float _range;

	// Use this for initialization
	void Start () {
        _range = AudioSource.maxDistance;
	}


    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnCollisionEnter(Collision c)
    {

    }

    private void OnCollisionExit(Collision c)
    {

    }
}
