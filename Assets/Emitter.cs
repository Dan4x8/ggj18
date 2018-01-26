using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Emitter : MonoBehaviour {

    private float _range;

    public EmitterState State = EmitterState.Inactive;

	// Use this for initialization
	void Start () {
        _range = GetComponent<AudioSource>().maxDistance;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().RegisterEmitter(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().UnregisterEmitter(this);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("COLLIDE!");
	}
}

public enum EmitterState { Inactive = 0, Push = -1, Pull = 1 };

