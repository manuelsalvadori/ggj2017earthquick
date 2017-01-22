using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

    public bool start = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
