﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //Used tutorial: https://kylewbanks.com/blog/unity-2d-detecting-gameobject-clicks-using-raycasts

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, (1 <<9));
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
                hit.collider.GetComponent<EmitterClickBehavior>().Click();
            }
        }
	}
}
