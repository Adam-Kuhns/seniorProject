using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    public AudioSource level;
    public AudioSource boss; 

    // Start is called before the first frame update
    void Start()
    {
        level.Play();
    }

    public void changeMusic()
    {
        level.Stop();
        boss.Play();
    }
}
