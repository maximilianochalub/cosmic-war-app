using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Clase --> EnemiesCreation
// Descripcion --> Crea naves enemigas y las carga en pantalla de manera paulatina. Asigna un color a c/u. 
// Se gestiona el desplazamiento de las mismas. Configura otros componentes iniciales en el juego

public class EnemiesCreation : MonoBehaviour {


	// Manejadores
    EnemyManager EM;

	SpriteManager SM;

	AudioManager AM;

	// T y D --> temporizador y latencia para carga de objetos inicial
	// MT y MD --> temporizador y latencia para movimiento de naves
	Timer T, D, MT, MD;

	// Indica el numero de naves cargadas en pantalla en un momento determinado
	int LoadedShips;

	// Indica si se cargaron todas las naves en pantalla
	bool LoadingComplete;

	// Indica si todas las naves ya tienen color asignado
	public bool SetRandomColorsHasCompleted;

	// Variables de control de desplazamiento
	// Indica ultima direccion o actual
	string NewMove;

	// Indica la direccion anterior a LastMove
	string LastMove;

	// Contador de desplazamientos horizontal y vertical
	int HorMovementsCounter;

	int VerMovementsCounter;

	// Numero maximo de desplazamientos horizontal y vertical
	int MaxHorMovements;

	int MaxVerMovements = 7;

	// Clips de audio
	public AudioClip LoadingShips, LoadingColors, OpenedEnemySound, ClosedEnemySound, Victory, GameOverSound;

	// Para configurar movimientos
	EnemyMovement EMov;

	// Puntaje en pantalla
	Text ScoreTxt, AddedScoreTxt;

	Text[] TextElements;

	// Indices y para carga de naves
	int RowSize, ColumnSize, RowIdx, ColIdx, RowIdxInv, ColIdxInv;

	// Para manejo de indices
	int i, j;

	// Nave jugador
	GameObject PlayerShip;

	public bool EndFirstMovement;


	// Use this for initialization
	void Start () {

		// Localizamos y seteamos objetos
		TextElements = Canvas.FindObjectsOfType<Text> ();

		AddedScoreTxt = TextElements [0];

		ScoreTxt = TextElements [1];

		ScoreTxt.text = "S C O R E     000000";

		AddedScoreTxt.color = Color.black;

		EM = FindObjectOfType <EnemyManager> ();

		AM = FindObjectOfType <AudioManager> ();

		SM = FindObjectOfType <SpriteManager> ();

		EMov = FindObjectOfType <EnemyMovement> ();

		PlayerShip = GameObject.FindGameObjectWithTag ("PlayerShip");


		// Inicializamos temporizadores
		T = new Timer (0.05f); // Timer

		D = new Timer (0.05f); // Delay

		MT = new Timer (1.2f); // Movements Timer

		MD = new Timer (1.2f); // Movements Delay


		// Seteamos demas variables
		RowSize = 10;

		ColumnSize = 4;

		SetVar ();


		// Reproducimos audio de carga de naves
		AM.Play (LoadingShips);

	}
		
	// Update is called once per frame
	void Update () {

		if (!(GameOver.GameOverDelay)) {

			// Evaluamos si no hay una imagen mostrandose en escena 
			if (!(ImagesOnSceneManager.GetShowingImageOnScreen ())) {

				// Evaluamos si el nivel esta completo (se destruyeron todas las navcees
				if (CheckIfLevelCompleted.GetCheckVariable ()) {

					T.DecTime (Time.deltaTime);

					if (T.GetTime () <= 0) {

						// Evaluamos si se cargaron todas las naves
						if (!(LoadingComplete)) {

							// Carga en pantalla de naves enemigas de manera paulatina
							LoadShips ();

							// Evaluamos si se asignaron colores a todas las naves
						} else if (!(SetRandomColorsHasCompleted)) {

							// Se cargaron todas las naves en pantalla
							// Asignacion aleatoria de colores 
							SetRandomColorsHasCompleted = SetRandomColor ();

							// Reproducimos audio de carga de coores
							AM.Play (LoadingColors);

						} 

						// Reseteamos timer
						T.SetTime (D.GetTime ());

					}
					
					// Si todas las naves tienen su color asignado
					if (SetRandomColorsHasCompleted) {

						// --Comienza el juego--

						// Descontamos tiempo
						MT.DecTime (Time.deltaTime);

						// Evaluamos si transcurrio el tiempo de movimientos
						if (MT.GetTime () <= 0) {

							// Preparamos nuevo desplazamiento
							PrepareMovement ();

							// Intercambiamos sprite (forma de nave)
							SM.SwitchSpriteShipShape (ColumnSize, RowSize, EM);

							// Desplazamos
							EMov.Move (NewMove);

							// Reestablecemos temporizador
							MT.SetTime (MD.GetTime ());

							// Chequeamos constantemente si se destruyeron todas las naves
							CheckIfLevelCompleted.Execute (EM);

						}

					}
					
				} else {

					// El jugador completo el nivel
					StartCoroutine (PlayerHasWon ());

				}

			}

		}

	}


	// Se ejecutara cuando el jugador complete el nivel
	IEnumerator PlayerHasWon() {

		// Reproducimos sonido
		AM.Play (Victory);

		// Ocultamos texto puntaje parcial adicional
		AddedScoreTxt.color = Color.black;

		// Insertamos imagen de victoria e indicamos que se muestra
		ImagesOnSceneManager.VictoryImageRendererEnabled (true);

		ImagesOnSceneManager.SetShowingImageOnScreen (true);

		// Desactivamos por el momento la nave del jugador
		PlayerShip.SetActive (false);

		yield return new WaitForSeconds (1.8f);

		// Luego de ciertos segundos descartamos imagen e indicamos
		ImagesOnSceneManager.VictoryImageRendererEnabled (false);

		ImagesOnSceneManager.SetShowingImageOnScreen (false);

		// Activamos nave jugador
		PlayerShip.SetActive (true);

		// Cargamos nueva matriz de naves enemigas y seteamos variables iniciales
		EM.SetElements ();

		AM.Play (LoadingShips);

		SetVar ();

	}
		

	// Carga de variables iniciales para comienzo en cada nivel
	void SetVar() {

		LoadedShips = 0;

		RowIdx = 0;

		ColIdx = 0;

		NewMove = "Down";

		LastMove = "Left";

		MaxHorMovements = 25;

		HorMovementsCounter = 0;

		VerMovementsCounter = 0;

		i = 0;

		j = 0;

		LoadingComplete = false;

		SetRandomColorsHasCompleted = false;

		EndFirstMovement = true;

		PlayerShip.SetActive (false);

		CheckIfLevelCompleted.SetCheckVariable (true);

		MT.SetTime (1.2f);

		MD.SetTime (1.2f);

		GameOver.GameOverDelay = false;

	}


	// Asigna los colores a las naves en forma secuencial e inversa
	// Devuelve true cuando finaliza la asignacion o false en caso contrario
	public bool SetRandomColor() {

		if (ColIdx < RowSize) {

			//Obtenemos valor aleatorio
			int RandomIdx = Random.Range (1, 5);
			 
			int RandomIdxInv = Random.Range (1, 5);

			//Asignamos el sprite correspondiente
			EM.SetEnemySprite(RowIdx, ColIdx, SM.GetSpriteColor(RandomIdx, "Closed"));

			EM.SetEnemySprite(ColumnSize - 1 - RowIdx, RowSize - 1 - ColIdx, SM.GetSpriteColor(RandomIdxInv, "Closed"));

			ColIdx++;

			return false;

		} else if (RowIdx < ColumnSize/2 - 1) {

			ColIdx = 0;

			RowIdx++;

			return false;

		} else {

			// Cuando todas las naves tienen color asignado habilitamos para disparo nave jugador
			PlayerShip.SetActive(true);

			return true;
		
		}

	}


	// Prepara el movimiento de naves
	// Contiene un valor numerico distinto para cada direccion
	void PrepareMovement() {

		//Evaluamos si el ultimo desplazamiento fue hacia abajo
		if (NewMove == "Down") {

			//Evaluamos cual fue el anteultimo desplazamiento
			//Si fue derecha
			if (LastMove == "Right") {

				LastMove = NewMove;

				//Switcheamos a izquierda
				NewMove = "Left";

				HorMovementsCounter++;

				//Sino fue izquierda
			} else {

				LastMove = NewMove;

				//Switcheamos a derecha
				NewMove = "Right";

				HorMovementsCounter++;
			}

		} else if (HorMovementsCounter == MaxHorMovements) {
			
			// Si termino el primer desplazo se incrementan maximos movimientos horizontales

			if (EndFirstMovement) {

				EndFirstMovement = false;

				MaxHorMovements = 45;

			} 
				
			LastMove = NewMove;

			//Desplazamiento hacia abajo
			NewMove = "Down";

			//Aceleramos movimiento
			MT.DecTime (0.25f);

			MD.DecTime (0.25f);

			//Incrementamos contador en vertical
			VerMovementsCounter++;

			//Cereamos contador en horizontal
			HorMovementsCounter = 0;

		} else if (MaxVerMovements == VerMovementsCounter) {

			AM.Play (GameOverSound);

			StartCoroutine (GameOverMethod ());

		} 	else {

			//Continuamos en la misma direccion
			HorMovementsCounter++;

		}

	}


	//	Game over
	IEnumerator GameOverMethod() {

		EM.EnableAllRenderer (false);

		PlayerShip.SetActive (false);

		ImagesOnSceneManager.GameOverImageRendererEnabled (true);

		GameOver.GameOverDelay = true;

		yield return new WaitForSeconds (3f);

		GameOver.GameOverDelay = false;

		//gameObject.GetComponent<Renderer> ().enabled = true;

		SwitchingScenes.LoadSceneMethod ("Intro");

	}


	// Maneja indices e indica si la carga de naves enemigas en pantalla se ha completado
	// Llama al metodo LoadingShipOnScreen(int i, int j)
	void LoadShips() {

		// Evaluamos si quedan naves enemigas por cargar
		if (LoadedShips < RowSize * ColumnSize) {

			//if (!(EM.DestroyedEnemy (i, j))) {

				// Cargamos una nave enemiga en pantalla
				LoadShipOnSreen (i, j);
 
				// Incrementamos contador de carga
				LoadedShips++;

				// Evaluamos si se inserta la ultima nave de la fila corriente
				if (j == RowSize - 1) {

					j = 0;

					i++;

				} else {

					j++;

				}

				LoadingComplete = false;

			//}

		} else if (LoadedShips == RowSize * ColumnSize) {

			LoadingComplete = true;

		} 

	}


	// Carga una nave enemiga en pantalla 
	// La carga se realiza habilitando la visibilidad de renderizado
	void LoadShipOnSreen(int i, int j) {

		//Hacemos visible el elemento
		EM.EnableEnemyRenderer(i, j, true);

	}

	// Llamado para ocultar puntaje parcial adicional
	public void HideAddedScore() {

		StartCoroutine (Hide());

	}



	IEnumerator Hide() {

		yield return new WaitForSeconds (0.35f);

		AddedScoreTxt.color = Color.black;

	}

}
