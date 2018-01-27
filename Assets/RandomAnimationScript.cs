using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimationScript : MonoBehaviour
{
	public List<AnimationClip> AnimationCollection = new List<AnimationClip>();
	public bool RandomRotation = true;

	private void Start()
	{

		var r = Random.Range(0, AnimationCollection.Count);
		var aoc = new AnimatorOverrideController(GetComponent<Animator>().runtimeAnimatorController);
		var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
		
		anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[0], AnimationCollection[r]));
		aoc.ApplyOverrides(anims);
		GetComponent<Animator>().runtimeAnimatorController = aoc;
		if (RandomRotation)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		}

	}
}
