using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource m_source;
    [SerializeField] private AudioSource m_Walkingsource;

    void Awake()
    {
        
        m_source = GetComponent<AudioSource>();
        
        // keep object
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } //destroy dupli
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        
    }

    public void playSound(AudioClip _sound)
    {
        m_source.PlayOneShot(_sound);
    }

    public void WalkSoundPlay()
    {
        m_Walkingsource.Play();
    }
    public void WalkSoundStop()
    {
        m_Walkingsource.Stop();
    }
    
    public bool WalkSoundStatus()
    {
        return m_Walkingsource.isPlaying;
    }

}