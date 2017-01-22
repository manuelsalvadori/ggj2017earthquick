using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    public float counterTime;
    public bool isPaused;

    public Text testo;

	void Start () {
        counterTime = 0f;
        isPaused = false;
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
            var intTime = counterTime;
            int minutes = (int)Mathf.Floor(intTime) / 60;
            int seconds = (int) Mathf.Floor(intTime) % 60;
            float fraction = Mathf.Floor((counterTime - Mathf.Floor(counterTime)) * 100);
            fraction = fraction % 1000;
            //testo.text = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
            testo.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + fraction.ToString("00");
        }
	}
}
