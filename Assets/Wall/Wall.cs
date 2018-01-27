using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Wall : MonoBehaviour
{
	private void Start()
	{
		gameObject.AddComponent(typeof(PolygonCollider2D));
	}

	private void OnDrawGizmos()
	{
		if (GetComponent<SpriteRenderer>().sprite == null)
		{
			Gizmos.DrawCube(transform.position, Vector3.one);
		}
	}
}
