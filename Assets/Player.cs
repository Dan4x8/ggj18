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

	public void Move()
	{
		Vector3 target = CalculateDirection().Multiply(Speed);
		transform.position = Vector3.MoveTowards(transform.position,transform.position+target,Speed);
	}

	private Vector3 CalculateDirection()
	{
		var r = Vector3.zero;

		foreach(var emitter in EmitterCollection)
		{
			if (!((emitter.State == EmitterState.Push) || (emitter.State == EmitterState.Pull)))
				continue;

			var dx = (emitter.transform.position - transform.position);
			r += (dx / dx.normalized.sqrMagnitude).Multiply((int)emitter.State);
		}

		if(r != Vector3.zero)
			return (r / r.normalized.sqrMagnitude);
		return Vector3.zero;
	}

	private void FixedUpdate()
	{
		Move();
	}
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
