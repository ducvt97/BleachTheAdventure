using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
	Button btnLoad;
	private int savedLevel;
	void Start()
	{
		btnLoad = GameObject.Find("btnContinue").GetComponent<Button>();
		btnLoad.interactable = false;
		btnLoad.gameObject.SetActive(false);
		savedLevel = PlayerPrefs.GetInt("LastLevel", 0);
		if (savedLevel != 0)
		{
			btnLoad.interactable = true;
			btnLoad.gameObject.SetActive(true);
		}
	}
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
