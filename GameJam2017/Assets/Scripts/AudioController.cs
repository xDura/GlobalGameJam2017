using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource Stadium_Ambience = GetComponent<AudioSource>();
        Stadium_Ambience.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
