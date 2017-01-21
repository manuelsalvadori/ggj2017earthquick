using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectMenu;
    public GameObject models;
    public GameObject[] worlds;

    public void startButton()
    {
        mainMenu.SetActive(false);
        selectMenu.SetActive(true);
        models.SetActive(true);
    }

    public void credits()
    {
        
    }

    public void selectWorld()
    {
        
    }

    public void next()
    {

    }

    public void previous()
    {
        
    }
}
