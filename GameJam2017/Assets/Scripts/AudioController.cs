using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource Stadium_Ambience;
    public AudioSource Shot;
    public AudioSource Jump;
    public AudioSource tensionSong;
    public AudioSource victorySong;
    public AudioSource countDown;
    public AudioSource horn;

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

    public void PlayCountDown()
    {
        countDown.Play();
    }

    public void PlayHorn()
    {
        horn.Play();
    }
}
