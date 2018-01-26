using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
	}

	public void UnregisterEmitter(Emitter sender)
	{
		EmitterCollection.Remove(sender);
	}

	public void Move(float weight = 1f)
	{
		Vector3 target = Vector3.up * weight;
		transform.position = Vector3.MoveTowards(transform.position,target,weight*Speed);
	}

	private Vector3 CalculateDirection()
	{
		return Vector3.zero;
	}

	private void FixedUpdate()
	{
		Move();
	}

	/*
	public class Emitter
	{

	}
	*/
}
