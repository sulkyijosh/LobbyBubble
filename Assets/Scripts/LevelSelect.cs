using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	[SerializeField] string setLevel;
	[SerializeField] GameObject prePlay;
	[SerializeField] GameObject blackScreen;

	public void Pressed()
	{
		//prePlay = GameObject.FindGameObjectWithTag("preplayui");
		SendScript.level = setLevel;
		prePlay.gameObject.SetActive(true);
		blackScreen.gameObject.SetActive(true);

	}
}
