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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		var p = collision.collider.GetComponent<Player>();
		var off = p.CalculateDirection()*-1.5f;
		p.transform.position += off.normalized;
	}
}
