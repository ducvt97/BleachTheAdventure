using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool isPause = false;
	public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Escape))
    	{
    		if (isPause)
        	Resume();
        	else
        	Pause();
    	}
        
    }
    public void Resume()
    {
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPause = false;
    }
    void Pause()
    {
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
    	isPause = true;
    }
    public void GotoMenu()
    {
    	isPause = false;
    	Time.timeScale = 1f;
    }
    public void SaveGame()
    {
    	PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
    }
}
