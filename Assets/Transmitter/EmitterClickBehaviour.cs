﻿using UnityEngine;

public class EmitterClickBehaviour : ClickBehaviour
{
	public override void Click()
	{
		var e = transform.parent.GetComponent<Emitter>();
		e.ChangeState();
	}
}
