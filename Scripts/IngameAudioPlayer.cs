using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameAudioPlayer : MonoBehaviour {

    int musicaAleatoria;
    public AudioClip musica1, musica2;
    public AudioSource source;

	// Use this for initialization
	void Start () {
        musicaAleatoria = Random.Range(0, 2);

        if(musicaAleatoria == 1)
        {
            source.clip = musica2;
        } else
        {
            source.clip = musica1;
        }

        source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
