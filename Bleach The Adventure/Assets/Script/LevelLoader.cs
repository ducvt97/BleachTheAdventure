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
	private GameObject enemy;
	public void LoadSavedLevel(int savedLevel)
	{
		StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt("LastLevel", 0)));
	}
	void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			curLevel.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1).ToString();
			enemy = GameObject.Find("Enermy");
		}
		
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			if (ChildCount(enemy) == 0)
			{
				if (SceneManager.GetActiveScene().buildIndex != 13)
				LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
				else
				LoadLevel(0);
			}
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

	public int ChildCount(GameObject obj)
	{
		if (obj != null){
		int count = 0;

	    for (int i = 0; i < obj.transform.childCount; i++)
	    {
	        count++;
	        counter(obj.transform.GetChild(i).gameObject, ref count);
	    }
	    return count;
	}
	return 0;
	}
	private void counter(GameObject currentObj, ref int count)
{
    for (int i = 0; i < currentObj.transform.childCount; i++)
    {
        count++;
        counter(currentObj.transform.GetChild(i).gameObject, ref count);
    }
}
}
