using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource Stadium_Ambience;
    public AudioSource Shot;
    public AudioSource Jump;
    public AudioSource tensionSong;
    public AudioSource victorySong;

    // Use this for initialization
    void Start () {
        Stadium_Ambience.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayShot()
    {
        Shot.Play();
    }

    public void PlayJump()
    {
        Jump.Play();
    }

    public void VictorySong()
    {
        victorySong.Play();
    }
}
