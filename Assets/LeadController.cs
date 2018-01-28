using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadController : MonoBehaviour
{
	public List<Sprite> Backgrounds;

	public UnityEngine.UI.Image Background;
	public UnityEngine.UI.Button PlayButton;
	public Sprite Intro;
	public float Timer = 1;

	private void Start()
	{
		PlayButton.transform.position = new Vector2(Background.rectTransform.rect.width / 2f, 90f);
		StartCoroutine(WaitSecs(Timer));
	}

	private IEnumerator WaitSecs(float time)
	{
		for (int i = 0; i < Backgrounds.Count; i++)
		{
			yield return new WaitForSeconds(time+i*1.45f);
			Background.sprite = Backgrounds[i];
		}

		if (!PlayButton.interactable)
		{
			PlayButton.interactable = true;
		}
		else
		{
			PlayButton.interactable = false;
		}
	}

	public void LoadLevel()
	{
		PlayButton.interactable = false;
		StartCoroutine(GoToLevel());
	}

	private IEnumerator GoToLevel()
	{
		Background.sprite = Intro;
		yield return new WaitForSeconds(6);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level_One");
	}
}
