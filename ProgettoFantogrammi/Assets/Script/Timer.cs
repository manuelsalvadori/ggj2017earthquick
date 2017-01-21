using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float counterTime;
    public bool isPaused;

	void Start () {
        counterTime = 0f;
        isPaused = true;
	}

    public void Pause()
    {
        isPaused = !isPaused;
    }
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(counterTime);
        if (isPaused == false)
        {
            counterTime = counterTime + Time.deltaTime;
        }
	}
}
