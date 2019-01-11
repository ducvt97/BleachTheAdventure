using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	public GameObject loadScreen;
	public Slider slider;
	public Text progressText;
	public Text curLevel;
	public void LoadSavedLevel(int savedLevel)
	{
		StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt("LastLevel", 0)));
	}
	void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			curLevel.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1).ToString();
		}
		
	}
	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}
	IEnumerator LoadAsynchronously (int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		loadScreen.SetActive(true);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			slider.value = progress;
			progressText.text = (int)(progress * 100f) + "%";
			yield return null;
		}
	}
}
