using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Clase --> SwitchingScenes
// Descripcion --> Gestiona cambio de escenas

public class SwitchingScenes : MonoBehaviour {


	// Carga escena segun boton presionado
	public void OnClickLoadScene(int Scene) {

		SceneManager.LoadScene (Scene);

	}


	// Carga escena segun nombre de escena
	public static void LoadSceneMethod(string SceneName) {

		switch (SceneName) {

		case "Intro":

			SceneManager.LoadScene (0);

			break;

		case "Main": 

			SceneManager.LoadScene (1);

			break;

		}

	}

}
