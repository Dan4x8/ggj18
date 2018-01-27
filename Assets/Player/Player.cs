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
			_visualizerCollection.Add(sender, Instantiate(VisualizerTemplate));
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
	}

	private Vector3 CalculateDirection()
	{
		var r = Vector3.zero;
		

		foreach (var emitter in EmitterCollection)
		{
			if (emitter.State == EmitterState.Inactive)
				continue;

			var channel = emitter.CurrentKey;
			var clip = emitter.RadioChannels[channel];

			var dx = (emitter.transform.position - transform.position);
			var astate = AudioClipBehaviourMatching.Find(p => p.AudioClip == clip).ActionState;
			r += (dx / dx.normalized.sqrMagnitude).Multiply((int)astate);

			VisualizeAction(astate);
		}

		if(r != Vector3.zero)
			return (r / r.normalized.sqrMagnitude);
		return Vector3.zero;
	}

	private void FixedUpdate()
	{
		Move();
	}

	public LineRenderer VisualizerTemplate;

	private void VisualizeAction(ActionState state)
	{
		for(int i = 0; i < EmitterCollection.Count; i++)
		{
			if (EmitterCollection[i].State == EmitterState.Inactive)
				continue;

			var line = _visualizerCollection[EmitterCollection[i]];
			if (state == ActionState.Push)
			{
				line.startColor = Color.red;
				line.endColor = Color.red;
			}
			else
			{
				line.startColor = Color.green;
				line.endColor = Color.green;
			}
				line.SetPositions(new Vector3[] { EmitterCollection[i].transform.position, transform.position });
		}
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