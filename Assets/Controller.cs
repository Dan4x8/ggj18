using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, (1 <<9));
			
			if (hit.collider != null)
            {
                hit.collider.GetComponent<ClickBehaviour>().Click();
            }
        }
	}
}
