using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource[] audios;
    public List<AudioClip> clips=new List<AudioClip>();

    private void Start()
    {
        audios = GetComponents<AudioSource>();
        foreach (var a in audios) { 
        a.playOnAwake=false;
            a.loop = false;    
        }
    }
    public void PlayOneShot(int index,float volume) {
        foreach (var a in audios) {
            if (a.isPlaying) continue;
            else {
                a.PlayOneShot(clips[index],volume);
                break;
            }
        }
    }
   

}
