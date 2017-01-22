using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectMenu;
    public GameObject models;
    public GameObject creditz;
    public GameObject[] worlds;
    public Text backButton;

    int selected = 0;

    public void startButton()
    {
        mainMenu.SetActive(false);
        selectMenu.SetActive(true);
        models.SetActive(true);
    }

    public void credits()
    {
        mainMenu.SetActive(false);
        creditz.SetActive(true);
    }

    public void back()
    {
        creditz.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void selectWorld()
    {
        switch (selected)
        {
            case 0: 
                SceneManager.LoadScene("Scena1");
                break;
            case 1:
                SceneManager.LoadScene("Scena2");
                break;
            case 2:
                SceneManager.LoadScene("Scena3");
                break;
            default:
                return;
        }
    }

    public void next()
    {
        worlds[selected].SetActive(false);

        if (++selected > worlds.Length -1)
            selected = 0;
        
        backButton.text = "WORLD " + (selected + 1);
        worlds[selected].SetActive(true);

    }

    public void previous()
    {
        worlds[selected].SetActive(false);

        if (--selected < 0)
            selected = worlds.Length -1;
        
        backButton.text = "WORLD " + (selected + 1);
        worlds[selected].SetActive(true);
    }
}
