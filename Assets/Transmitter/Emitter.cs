using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Emitter : MonoBehaviour
{
	public List<AudioClip> RadioChannels;
	public int CurrentKey = 0;
	
	public EmitterState State = EmitterState.Active;

	public void ChangeState()
    {
		var clip = RadioChannels.Cycle(1, CurrentKey);
		CurrentKey = RadioChannels.IndexOf(clip);
		
		var audio = GetComponent<AudioSource>();
		audio.clip = clip;
		audio.Play();
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
		
		GetComponents<CircleCollider2D>().First(p => p.isTrigger).radius = GetComponent<AudioSource>().maxDistance;
	}
}

public enum ActionState { Push = -1, Pull = 1 };
public enum EmitterState { Active = 1, Inactive = 0};