using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		ListOf.ToDoLater.Remove();
		SceneManager.LoadScene(sceneName);
	}
}
