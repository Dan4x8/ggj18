﻿using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Emitter : MonoBehaviour
{
    public EmitterState State = EmitterState.Inactive;

    public void ChangeState(EmitterState s)
    {
        State = s;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().RegisterEmitter(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().UnregisterEmitter(this);
    }

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, .5f);
		Gizmos.DrawWireSphere(transform.position, GetComponent<AudioSource>().maxDistance);
	}
}

public enum EmitterState { Inactive = 0, Push = -1, Pull = 1 };

