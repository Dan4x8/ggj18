using UnityEngine;

public class EmitterClickBehaviour : ClickBehaviour
{
	public override void Click()
	{
		var e = transform.parent.GetComponent<Emitter>();
		e.ChangeState(NextState(e.State));
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
