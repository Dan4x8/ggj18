using UnityEngine;

public class EmitterClickBehaviour : ClickBehaviour
{
	public override void Click()
	{
		var e = transform.parent.GetComponent<Emitter>();
		e.ChangeState(NextState(e.State));
		//GetMatchingState(e);
	}

	EmitterState GetMatchingState(Emitter e)
	{
		var ctrl = FindObjectOfType<Controller>();
		var channel = e.RadioChannels.GetEnumerator();
		for (int cur = e.CurrentKey; cur >= 0; cur--)
		{
			Debug.Log(channel.MoveNext());
		}
		Debug.Log(channel.Current);

		return EmitterState.Inactive;
	}

	private EmitterState NextState(EmitterState estate)
	{
		var r = (int)estate;
		r++;
		if (r > 1)
			r = -1;
		return (EmitterState)r;
	}
}
