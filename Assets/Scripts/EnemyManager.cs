using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Clase --> EnemyManager
// Descripcion --> Se realizan todas las operaciones correspondientes a las naves enemigas

public class EnemyManager : MonoBehaviour {

	// Array unidimensional de naves enemigas
	public GameObject[] Enemy, EnemyPrefabs;

	// Array bidismensional de naves enemigas
	public GameObject[,] AllEnemies;

	// Array bidimensional que almacena cuantas veces una nave enemiga fue disparada
	public int[,] EnemiesTimesShot;

	// Manejador de sprites
	SpriteManager SM;

	//Numero de naves enemigas por fila y por columna
	int RowSize = 10;

	int ColumnSize = 4;



	void Start() {

		// Inicializamos elementos
		SM = FindObjectOfType<SpriteManager> ();

		Enemy = new GameObject[RowSize * ColumnSize];

		SetElements ();

	}


	// Incializacion de elementos
	public void SetElements() {

		EnemiesTimesShot = new int[ColumnSize, RowSize];

		AllEnemies = new GameObject[ColumnSize, RowSize];

		SetPrefabsActive ();

		LoadEnemyArray ();

		SetAllEnemies ();

		EnableAllRenderer (false);

	}


	// Carga el array unidimensional de naves enemigas
	void LoadEnemyArray() {

		for (int i = 0; i < RowSize * ColumnSize; i++) {

			Enemy [i] = Instantiate (EnemyPrefabs [i], EnemyPrefabs [i].transform.position, Quaternion.identity) as GameObject;

			EnemyPrefabs [i].SetActive (false);

		}

	}


	// Activa prefabs
	void SetPrefabsActive() {

		for (int i = 0; i < RowSize * ColumnSize; i++) {

			if (!(EnemyPrefabs [i].activeSelf)) {

				EnemyPrefabs [i].SetActive (true);
			}

		}

	}
		


	// Devuelve nave enemiga segun posicion
	public GameObject GetEnemy(int i, int j) {

		return AllEnemies [i, j];

	}
		

	// Devuelve array naves enemigas
	public GameObject[,] GetAllEnemies() {

		return AllEnemies;
	}


	// Habilita o deshabilita el renderizado en la escena
	public void EnableEnemyRenderer(int i, int j, bool Val) {

		AllEnemies [i, j].GetComponent<Renderer> ().enabled = Val;

	}


	// Habilita o deshabilita todos los objetos en la escena
	public void EnableAllRenderer(bool Val) {

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (!(DestroyedEnemy(i, j))) {

					AllEnemies [i, j].GetComponent<Renderer> ().enabled = Val;

				}

			}

		}

	}


	// Asigna un sprite al objeto de una cierta posicion
	public void SetEnemySprite(int i, int j, Sprite sprite) {
			
		AllEnemies [i, j].GetComponent<SpriteRenderer> ().sprite = sprite;

	}


	// Devueleve el sprite del objeto de una cierta posicion
	public Sprite GetEnemySprite(int i, int j) {
		
		return AllEnemies [i, j].GetComponent<SpriteRenderer> ().sprite;

	}


	// Devuelve el sprite del objeto 
	public Sprite GetGameObjectSprite(GameObject GO) {

		Sprite Sp = null;

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (!(DestroyedEnemy(i, j))) {

					if (AllEnemies [i, j].Equals (GO)) {

						Sp = GetEnemySprite(i, j);

					}

				}

			}

		}

		return Sp;

	}


	// Devuelve numero de naves por cargadas por fila
	public int GetRowSize() {

		return AllEnemies.GetLength (1);

	}


	// Devuelve numero de naves cargadas por columna
	public int GetColumnSize() {

		return AllEnemies.GetLength (0);

	}


	// Asigna una posicion en el espacio al objeto de una posicion determinada
	public void SetEnemyPosition(int i, int j, Vector3 Pos) {

		if (!(DestroyedEnemy(i,j))) {

			AllEnemies [i, j].transform.position = Pos;

		}

	}


	// Devuelve la posicion en el espacio de un objeto en la posicion determinada
	public Vector3 GetEnemyPosition(int i, int j) {

		Vector3 Pos = new Vector3 ();

		if (!(DestroyedEnemy(i,j))) {
		
			Pos = AllEnemies [i, j].transform.position;

		}

		return Pos;

	}


	// Prepara el array bidimensional para ser utilizado
	// Carga los objetos de naves enemigas en el mismo a partir del array unidimensional
	public void SetAllEnemies() {

		int ColumnCounter = 0;

		//Array que almacena enemigos por fila
		GameObject[] RowEnemies = new GameObject[RowSize];

		while (ColumnCounter < ColumnSize) {

			// Asignamos valores vector fila
			for (int i = 0; i < RowSize; i++) {

				RowEnemies [i] = Enemy [(ColumnCounter * RowSize) + i];

			}

			// Vamos insertando por fila en el array bidimensional 
			for (int k = 0; k < RowSize; k++) {

				AllEnemies [ColumnCounter, k] = RowEnemies [k];

			}

			ColumnCounter++;

		}

	}

	// Devuelve el indice fila del objeto
	public int GetEnemyRowIndex(GameObject GO) {

		int idx = 0;

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (AllEnemies [i, j].gameObject.Equals(GO)) {

					idx = i;

				}

			}

		}

		return idx;
			
	}


	// Devuelve el indice columna del objeto
	public int GetEnemyColIndex(GameObject GO) {

		int idx = 0;

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (AllEnemies [i, j].gameObject.Equals(GO)) {

					idx = j;

				}

			}

		}

		return idx;

	}
		

	// Verifica si la nave enemiga con posicion i, j fue destruida
	public bool DestroyedEnemy(int i, int j) {
		
		if (AllEnemies [i, j].Equals(null)) {

			return true;

		} else {

			return false;

		}
			
	}


	// Devuelve el numero de veces que una nave fue disparada
	// Recibe como paramentro la nave enemiga 
	public int ShotCounter(GameObject GO) {

		int TimesShot = 0;

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (AllEnemies [i, j].gameObject.Equals(GO)) {

					EnemiesTimesShot [i, j] += 1;

					TimesShot = EnemiesTimesShot [i, j];

				}
					
			}

		}

		return TimesShot;

	}


	// Devuelve si una nave debe ser destruida
	public bool HasToBeDestroyed(GameObject GO, int TimesShot) {

		bool IsBlue = false;

		bool IsGreen = false;

		bool IsRed = false; 

		bool IsYellow = false;

		// Localizamos el color del sprite de la nave con el sprite manager y asignamos variable segun corresponda
		switch (SM.FindSpriteColor (GetGameObjectSprite (GO), this, 0, 0, false)) {

		case 1:

			IsBlue = true;

			break;

		case 2:

			IsGreen = true;

			break;

		case 3:

			IsRed = true;

			break;

		case 4:

			IsYellow = true;

			break;

		
		}

	
		// Consultamos cuantas veces fue disparado el objeto
		switch (TimesShot) {

		case 1: 

			// Una vez y es azul o verde debe ser destruida
			if ((IsBlue) | (IsGreen)) {

				return true;

			} else {

				return false;

			}
				
		case 2: 

			// Dos veces y es roja o amarilla debe ser destruida
			if ((IsRed) | (IsYellow)) {

				return true;

			} else {

				return false;

			}

		default:
			
			return false;
		
		}

	}

}
