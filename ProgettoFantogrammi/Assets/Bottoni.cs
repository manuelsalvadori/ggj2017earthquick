using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Bottoni : MonoBehaviour {

	public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void next() {
        SceneManager.LoadScene("Scena" + (Int32.Parse(SceneManager.GetActiveScene().name.Substring(5)) + 1));
    }

    public void back()
    {
        Scene sc = SceneManager.GetActiveScene();
        Camera.main.GetComponent<AudioListener>().enabled = false;
        SceneManager.LoadScene("Menu");
        StartCoroutine(aspetta());
    }

    IEnumerator aspetta()
    {
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MenuManager>().startButton();
    }
}
