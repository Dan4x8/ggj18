﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public List<ClipBehaviourPair> AudioClipBehaviourMatching;

	public float Speed = 1.0f;

	private List<Emitter> _emitterCollection;

	private List<Emitter> EmitterCollection
	{
		get { return _emitterCollection; }
	}

	private void Start()
	{
		_emitterCollection = new List<Emitter>();
	}

	public void RegisterEmitter(Emitter sender)
	{
		if (!EmitterCollection.Contains(sender))
			EmitterCollection.Add(sender);
		if (!_visualizerCollection.ContainsKey(sender))
		{
			var line = Instantiate(VisualizerTemplate) as LineRenderer;
			_visualizerCollection.Add(sender, line);
			line.gameObject.SetActive(false);
		}
	}

	public void UnregisterEmitter(Emitter sender)
	{
		EmitterCollection.Remove(sender);
		var clr = _visualizerCollection[sender].gameObject;
		_visualizerCollection.Remove(sender);
		Destroy(clr);
	}

	public void Move()
	{
		Vector3 target = CalculateDirection().Multiply(Speed);
		transform.position = Vector3.MoveTowards(transform.position,transform.position+target,Speed);

		if (EmitterCollection.Count != 2)
		{
			var bodyUp = transform.right;
			transform.rotation = Quaternion.FromToRotation(bodyUp, target) * transform.rotation;
		}
	}

	public Vector3 CalculateDirection()
	{
		var r = Vector3.zero;

		var ecount = EmitterCollection.Count;
		
		for(int i = 0; i< ecount;i++)
		{
			var emitter = EmitterCollection[i] as Emitter;
			if (emitter.State == EmitterState.Inactive)
				continue;

			var channel = emitter.CurrentChannel;
			var clip = emitter.RadioChannels[channel];

			var dx = (emitter.transform.position - transform.position);
			var astate = AudioClipBehaviourMatching.Find(p => p.AudioClip == clip).ActionState;
			r += (dx / dx.normalized.sqrMagnitude).Multiply((int)astate);

			VisualizeAction(astate, emitter);
		}

		if(r != Vector3.zero)
			return (r / r.normalized.sqrMagnitude);
		return Vector3.zero;
	}

	private void FixedUpdate()
	{
		if (!_frozen)
		{
			Move();
		}
	}

	public LineRenderer VisualizerTemplate;

	private void VisualizeAction(ActionState state, Emitter emitter)
	{
		var line = _visualizerCollection[emitter];
		if (state == ActionState.Push)
		{
			line.startColor = Color.red;
			line.endColor = Color.red;
		}
		else
		{
			//var dgreen = new Color(0, 62f/255f, 26f/255f, 1f);
			var dgreen = Color.green;
			line.startColor = dgreen;
			line.endColor = dgreen;
		}
		line.SetPositions(new Vector3[] { emitter.transform.position, transform.position });
		if(!line.gameObject.activeSelf)
		{
			line.gameObject.SetActive(true);
		}
	}

	private bool _frozen = false;

	public void SetFreeze(bool freeze)
	{
		_frozen = freeze;
	}

	private Dictionary<Emitter, LineRenderer> _visualizerCollection = new Dictionary<Emitter, LineRenderer>();
}

static public class CustomExtensions
{
	static public Vector3 Multiply(this Vector3 v, float multiplicator)
	{
		return new Vector3(v.x * multiplicator, v.y * multiplicator, v.z * multiplicator);
	}

	static public Vector2 Multiply(this Vector2 v, float multiplicator)
	{
		return new Vector2(v.x * multiplicator, v.y * multiplicator);
	}

	static public T Cycle<T>(this IEnumerable<T> arr, int pos_amount, int pos)
	{
		var en = arr.GetEnumerator();
		en.MoveNext();
		for(int i = 0; i < pos_amount + pos; i++)
		{
			if (!en.MoveNext())
			{
				en.Reset();
				en.MoveNext();
			}
		}
		return en.Current;
	}
}

[System.Serializable]
public struct ClipBehaviourPair
{
	public AudioClip AudioClip;
	public ActionState ActionState;
}