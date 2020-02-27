using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	private AudioSource sound;
	[SerializeField] private bool PlayOnStart; 

    void Start() {
        sound = gameObject.GetComponent<AudioSource>();
        sound.volume = Settings.AUDIO_VOLUME;
        if (PlayOnStart)
        	sound.Play(0);
    }
}
