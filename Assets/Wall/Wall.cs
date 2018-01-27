using UnityEngine;

public class Wall : MonoBehaviour
{
	private void Start()
	{
		gameObject.AddComponent(typeof(PolygonCollider2D));
	}
}
