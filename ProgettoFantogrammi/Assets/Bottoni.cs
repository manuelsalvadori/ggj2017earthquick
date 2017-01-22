using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Bottoni : MonoBehaviour
{

	public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void next()
    {
        int sceneToLoad = (Int32.Parse(SceneManager.GetActiveScene().name.Substring(5)) + 1);
        if (sceneToLoad < 4)
        {
            SceneManager.LoadScene("Scena" + sceneToLoad);
        }
        else
        {
            back();
        }
    }

    public void back()
    {
        SceneManager.LoadScene("Menu");
    }
}
