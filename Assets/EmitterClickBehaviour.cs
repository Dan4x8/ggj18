using UnityEngine;

public class EmitterClickBehaviour : ClickBehaviour
{
	public override void Click()
	{
		var e = transform.parent.GetComponent<Emitter>();
		var ctrl = FindObjectOfType<Controller>();

		var clip = e.RadioChannels.Cycle(1, e.CurrentKey);
		e.CurrentKey = e.RadioChannels.IndexOf(clip);

		var ns = ctrl.AudioClipBehaviourMatching.Find(p => p.AudioClip == clip).EmitterState;

		var audio = e.GetComponent<AudioSource>();
		audio.clip = clip;
		audio.Play();

		e.ChangeState(ns);
	}
}
