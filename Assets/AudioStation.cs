using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioStation : MonoBehaviour
{
	public List<AudioClip> AudioClips;

	private Dictionary<AudioClip, SourceStatePair> _map = new Dictionary<AudioClip, SourceStatePair>();

	private void Start()
	{
		AudioSource asrc;

		foreach (var ac in AudioClips)
		{
			asrc = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
			asrc.clip = ac;
			asrc.loop = true;
			asrc.mute = true;
			asrc.Play();
			_map.Add(ac, new SourceStatePair(asrc));
		}
	}

	public void RegisterEmitter(Emitter e)
	{
		_map[e.RadioChannels[e.CurrentChannel]].RegisterEmitter(e);
	}

	public void UnregisterEmitter(Emitter e)
	{
		_map[e.RadioChannels[e.CurrentChannel]].UnregisterEmitter(e);
	}

	public void ChangeChannel(Emitter e, AudioClip old)
	{
		_map[old].UnregisterEmitter(e);
		RegisterEmitter(e);
	}

	public class SourceStatePair
	{
		public SourceStatePair(AudioSource src)
		{
			Source = src;
		}

		public AudioSource Source;
		private List<Emitter> EmitterList = new List<Emitter>();

		public void RegisterEmitter(Emitter e)
		{
			EmitterList.Add(e);
			Source.mute = false;
		}

		public void UnregisterEmitter(Emitter e)
		{
			EmitterList.Remove(e);
			if (EmitterList.Count == 0)
				Source.mute = true;
		}
	}
}
