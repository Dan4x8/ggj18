using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Emitter : MonoBehaviour
{
	public List<AudioClip> RadioChannels;
	public int CurrentChannel = 0;
	
	public EmitterState State = EmitterState.Active;

	public AudioStation AudioStation;

	public void ChangeState()
    {
		if (!_inRange)
			return;

		var old = RadioChannels[CurrentChannel];
		var clip = RadioChannels.Cycle(1, CurrentChannel);

		CurrentChannel = RadioChannels.IndexOf(clip);

		AudioStation.ChangeChannel(GetComponent<Emitter>(),old);
    }

	private void Start()
	{
		AudioStation = FindObjectOfType<AudioStation>();
	}

	private bool _inRange = false;

	private void OnTriggerEnter2D(Collider2D other)
    {
		_inRange = true;
        other.gameObject.GetComponent<Player>().RegisterEmitter(this);
		AudioStation.RegisterEmitter(GetComponent<Emitter>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
		_inRange = false;
        other.gameObject.GetComponent<Player>().UnregisterEmitter(this);
		AudioStation.UnregisterEmitter(GetComponent<Emitter>());
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, .5f);
		//Gizmos.DrawWireSphere(transform.position, GetComponent<AudioSource>().maxDistance);
		
		/*
		GetComponents<CircleCollider2D>().First(p => p.isTrigger).radius = GetComponent<AudioSource>().maxDistance;
		GetComponent<AudioSource>().clip = RadioChannels[CurrentChannel];
		*/
	}
}

public enum ActionState { Push = -1, Pull = 1 };
public enum EmitterState { Active = 1, Inactive = 0};