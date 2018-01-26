using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private List<Transform> _emitterCollection;

	private List<Transform> EmitterCollection
	{
		get { return _emitterCollection; }
	}

	private void Start()
	{
		_emitterCollection = new List<Transform>();
	}

	public void RegisterEmitter(GameObject sender)
	{
		if (!EmitterCollection.Contains(sender.transform))
			EmitterCollection.Add(sender.transform);
	}

	public void UnregisterEmitter(GameObject sender)
	{
		EmitterCollection.Remove(sender.transform);
	}
}
