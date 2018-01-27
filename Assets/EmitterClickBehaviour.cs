using System.Collections;
using System.Collections.Generic;
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
		var r = (EmitterState)Mathf.Clamp((int)estate + 1f, -1, 1f);
		Debug.Log(r);
		return r;
	}
}

public class ClickBehaviour : MonoBehaviour
{
	public virtual void Click() { }
}
