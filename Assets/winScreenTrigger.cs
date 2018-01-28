using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScreenTrigger : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D c)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Win");
	}
}
