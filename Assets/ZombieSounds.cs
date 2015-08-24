using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ZombieSounds : MonoBehaviour
{
    public AudioSource gunSource;
    public bool awaken = false;

    public void Awake()
    {
        awaken = true;
        gunSource.Play();
    }

    public void Update()
    {
        gunSource.Play();
    }
    public void OnDestroy()
    {
        if (awaken)
        {
            Debug.Log("Should trigger sound");
            gunSource.Play();
        }
    }


}