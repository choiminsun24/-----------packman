using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip inGame;
    public AudioClip intro;

    private AudioSource audio;
    
    public void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void change()
    {
        audio.clip = inGame;
        audio.Play();
    }
}
