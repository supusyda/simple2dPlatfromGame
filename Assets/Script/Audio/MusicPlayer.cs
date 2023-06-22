using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource intro, loop;
    void Start()
    {
        intro.Play();
        loop.PlayScheduled(AudioSettings.dspTime + intro.clip.length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
