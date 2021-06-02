using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource BGMAudio;
    public AudioClip BGMSound;

    // Start is called before the first frame update
    void Start()
    {
        this.BGMAudio = this.gameObject.AddComponent<AudioSource>();
        this.BGMAudio.loop = true;
        this.BGMAudio.clip = this.BGMSound;
        this.BGMAudio.volume = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.BGMAudio.isPlaying == false) // replay 중이 아니면
        {
            this.BGMAudio.Play(); // audio clip 음원을 재생.
        }
    }
}
