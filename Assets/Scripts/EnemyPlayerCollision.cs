using UnityEngine;
using System.Collections;

// Clase --> EnemyPlayerCollision
// Descripcion --> Gestiona vidas y colisiones con nave jugador

public class EnemyPlayerCollision : MonoBehaviour {

	public GameObject PlayerBulletToDisable;

	// Representacion vidas con iconos nave jugador
	GameObject[] Lives;

	// Indica cantidad de vidas perdidas
	int LostLives;

	EnemyManager EM;

	public AudioClip PlayerShipGetsShot, GameOverSound;

	AudioManager AM;


	// Use this for initialization
	void Start () {

		// Inicializamos
		EM = FindObjectOfType<EnemyManager> ();

		AM = FindObjectOfType<AudioManager> ();

		Lives = GameObject.FindGameObjectsWithTag ("Lives");
	
		LostLives = 0;
	
	}


	// Acciona en caso de colision
	void OnCollisionEnter2D(Collision2D Col) {

		// Destruimos bala
		Destroy (Col.gameObject);

		LoseALife ();
		
	} 
		

	// Decremento de vidas
	void LoseALife() {

		AM.Play (PlayerShipGetsShot);

		LostLives += 1;

		// Si todavia sigue en juego
		if (LostLives <= 3) {

			bool Destroyed = false;

			foreach (GameObject GO in Lives) {

				if (!(Destroyed)) {

					if (!(GO.Equals (null))) {

						Destroy (GO);

						Destroyed = true;

						StartCoroutine (PlayerShipGotShot(LostLives));

					} 
			
				}

			}

		} 

		// Si perdio todas las vidas
		if (LostLives == 3){

			// Deshabilitamos renderizado de naves enemigas para visualizar game over
			for (int i = 0; i < EM.GetColumnSize(); i++) {

				for (int j = 0; j < EM.GetRowSize(); j++) {

					if (!(EM.DestroyedEnemy (i, j))) {

						EM.EnableEnemyRenderer (i, j, false);

					}

				}

			}

			gameObject.GetComponent<Renderer> ().enabled = false;

			AM.Play (GameOverSound);

			StartCoroutine (GameOverMethod ());
	
		}
			
	}


	//	Game over
	IEnumerator GameOverMethod() {

		EM.EnableAllRenderer (false);

		ImagesOnSceneManager.GameOverImageRendererEnabled (true);

		GameOver.GameOverDelay = true;

		yield return new WaitForSeconds (3f);

		GameOver.GameOverDelay = false;

		PlayerBulletToDisable.SetActive (true);

		//gameObject.GetComponent<Renderer> ().enabled = true;

		SwitchingScenes.LoadSceneMethod ("Intro");

	}


	// Co-rutina cuando nave jugador es disparada
	IEnumerator PlayerShipGotShot(int LostLives) {

		if (LostLives == 3) {

			PlayerBulletToDisable.SetActive (false);

			EnableRenderer (false);

		} else {

			// Efectos en renderizado
			EnableRenderer (false);

			yield return new WaitForSeconds (0.25f);

			EnableRenderer (true);

			yield return new WaitForSeconds (0.25f);

			EnableRenderer (false);

			yield return new WaitForSeconds (0.25f);

			EnableRenderer (true);

		}

	}


	// Setea renderizado de nave jugador
	void EnableRenderer(bool Val) {

		gameObject.GetComponent<Renderer> ().enabled = Val;

	}
		
}
