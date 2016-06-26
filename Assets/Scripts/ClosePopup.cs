using UnityEngine;
using System.Collections;

public class ClosePopup : MonoBehaviour {

	[SerializeField] GameObject blackScreen;

	public void ClosePopUp()
	{
		this.transform.parent.gameObject.SetActive(false);
		blackScreen.gameObject.SetActive(false);
	}
}
