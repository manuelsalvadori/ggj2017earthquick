﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottoni : MonoBehaviour {

	public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
