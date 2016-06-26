using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour {
	bool settings;
	GameObject SettingsBar;

	void Awake()
	{
		settings = false;
		SettingsBar = this.transform.FindChild("SettingsBar").gameObject;
	}

	public void SettingsToggle()
	{
		Vector3 settingsOpen = new Vector3(80, 80, 0);
		Vector3 settingsClosed = new Vector3(-160, 80, 0);

		if(settings == false)
		{
			iTween.MoveTo(SettingsBar, iTween.Hash("position", settingsOpen, "time", 1f, "islocal", true));
			settings = true;
		}
		else
		{
			iTween.MoveTo(SettingsBar, iTween.Hash("position", settingsClosed, "time", 1f, "islocal", true));
			settings = false;
		}
	}

	public void QuitLevel()
	{
		SceneManager.LoadScene("2. MainMenu");
	}

	public void BGMToggle()
	{
		
	}

	public void SFXToggle()
	{
		
	}
}
