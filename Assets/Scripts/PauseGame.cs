using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Clase --> PauseGame
// Descripcion --> Se utiliza para pausar y reanudar el juego

public class PauseGame : MonoBehaviour {

	bool GamePaused = false;

	public Text PausedGameText;


	void Start() {

		PausedGameText.color = Color.black;

	}


	void Update () {
	
		// Si se presiona P se pausa o se reanuda segun variables
		if (Input.GetKeyUp (KeyCode.P)) {

			if (!(GamePaused)) {

				Pause(); 

			} else {

				Resume();

			}

		}

		if (Input.GetKeyUp (KeyCode.Escape)) {

			SwitchingScenes.LoadSceneMethod ("Intro");

		}

	}


	// Pausamos juego
	void Pause() {

		Time.timeScale = 0;

		GamePaused = true;

		PausedGameText.color = Color.white;

	}


	// Volvemos al juego
	void Resume() {

		Time.timeScale = 1;

		GamePaused = false;

		PausedGameText.color = Color.black;

	}

}
