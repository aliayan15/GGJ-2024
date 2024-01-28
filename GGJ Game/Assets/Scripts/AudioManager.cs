using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get;private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void playSound(AudioClip clip)
    {

    }
}
