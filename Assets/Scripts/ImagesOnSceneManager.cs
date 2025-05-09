using UnityEngine;
using System.Collections;

// Clase --> ImagesOnSceneManager
// Descripcion --> Gestiona imagenes que se mostraran en pantalla

public class ImagesOnSceneManager : MonoBehaviour {

	// Imagenes de victoria y game over
	static GameObject VictoryImage, GameOverImage;

	// Indica si se muestra una imagen en un cierto momento
	static bool ShowingImageOnScreen;


	void Start () {

		// Localizamos objetos e inicializamos
		VictoryImage = GameObject.FindGameObjectWithTag ("VictoryText");

		GameOverImage = GameObject.FindGameObjectWithTag ("GameOverImage");

	}


	// Renderizado
	public static void VictoryImageRendererEnabled(bool Val) {
		
		VictoryImage.GetComponent<Renderer> ().enabled = Val;

	}


	// Renderizado
	public static void GameOverImageRendererEnabled(bool Val) {
		
		GameOverImage.GetComponent<Renderer> ().enabled = Val;

	}


	// Indica si se muestra imagen en pantalla
	public static void SetShowingImageOnScreen(bool Val) {

		ShowingImageOnScreen = Val;

	}


	// Devuelve true en caso de mostrarse imagen en pantalla
	public static bool GetShowingImageOnScreen() {

		return ShowingImageOnScreen;

	}

}
