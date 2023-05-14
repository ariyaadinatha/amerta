using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeafController : MonoBehaviour
{
    public AudioClip normalTrack;
    public AudioClip deafTrack;
    private AudioSource audioSource;
    private bool isPlayingNormalTrack = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = normalTrack;
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isPlayingNormalTrack)
            {
                audioSource.clip = deafTrack;
                isPlayingNormalTrack = false;
            }
            else
            {
                audioSource.clip = normalTrack;
                isPlayingNormalTrack = true;
            }

            audioSource.Play();
        }
    }
}
