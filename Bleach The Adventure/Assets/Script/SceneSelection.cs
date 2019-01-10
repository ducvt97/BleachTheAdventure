using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    public void selectScene()
    {
    	string tmp = this.gameObject.name;
    	if (tmp.StartsWith("btnLevel"))
    	{
    		if (tmp.Length == 8)
    		{
    			SceneManager.LoadScene(1);
    		}
    		else
    		{
    			int x = System.Convert.ToInt32(tmp.Substring(8)) + 1;
    			SceneManager.LoadScene(x);
    		}
    	}
    }
}
