using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAttenuationByDistance : MonoBehaviour
{ 
    private Transform Listener;
    private AudioSource[] audioSources;
    private float currentDistance;
    
    public float minDistance;
    public float maxDistance;


    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        Listener = FindObjectOfType<CharacterController>().transform;
    }
    void Update()
    {
        currentDistance = Vector3.Distance(transform.position, Listener.position);

        if (currentDistance < minDistance)
        {
            SetVolumeForAllSources(1f);
        }
        else if (currentDistance > maxDistance)
        {
            SetVolumeForAllSources(0f);

        }
        else
        {
            SetVolumeForAllSources(1 - ((currentDistance - minDistance) / (maxDistance - minDistance)));
        }
    }

    private void SetVolumeForAllSources(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}

