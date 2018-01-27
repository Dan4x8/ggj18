using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteManager : MonoBehaviour
{
	public bool RandomRotation = true;

	public List<Sprite> SpriteCollection = new List<Sprite>();

	public void Start()
	{
		var r = Random.Range(0, SpriteCollection.Count);
		GetComponent<SpriteRenderer>().sprite = SpriteCollection[r];

		if(RandomRotation)
		{
			transform.rotation = Quaternion.Euler(0f,0f,Random.Range(0f,360f));
		}
	}
}
