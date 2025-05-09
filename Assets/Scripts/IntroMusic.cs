using UnityEngine;
using System.Collections;

// Clase --> IntroMusic
// Descripcion --> Reproduce audio menu inicial

public class IntroMusic : MonoBehaviour {

	public AudioClip CosmicWarIntroSound;

	AudioManager AM;


	void Start () {

		AM = FindObjectOfType<AudioManager> ();

		AM.Play (CosmicWarIntroSound);

	}

}
