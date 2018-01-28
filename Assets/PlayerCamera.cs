using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public GameObject Target;

	void FixedUpdate()
	{
		if (Target == null)
			return;

		var target = Target.transform.position;
		transform.position = new Vector3(target.x, target.y, transform.position.z);
	}
}
