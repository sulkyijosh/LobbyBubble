using UnityEngine;
using UnityEngine.SceneManagement;

public class SendScript : MonoBehaviour {

	[SerializeField] string whereTo;
	public static string level;
	
	public void goTo()
	{
		if(whereTo == "a")
		{
			whereTo = level;
		}
		SceneManager.LoadScene(whereTo);
	}
}
