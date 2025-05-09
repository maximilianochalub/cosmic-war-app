using UnityEngine;
using System.Collections;

// Clase --> AudioManager
// Descripcion --> Gestiona reproduccion de clips de audio

public class AudioManager : MonoBehaviour {

	public AudioSource Track1, Track2, Track3;

	public void Play(AudioClip Music) {

		// Para que no exista superposición en los clips de audio
		if (!(Track1.isPlaying)) {

			SetMusic (Track1, Music);

		} else if (!(Track2.isPlaying)) {
			
			SetMusic (Track2, Music);
		
		} else {

			SetMusic (Track3, Music);
		}

	}

	// Seteamos musica de fondo
	void SetMusic(AudioSource Track, AudioClip Music) {

		Track.clip = Music;

		Track.Play ();
	
	}
}
