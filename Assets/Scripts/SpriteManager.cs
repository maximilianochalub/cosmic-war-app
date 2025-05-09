using UnityEngine;
using System.Collections;

// Clase --> SpriteManager
// Descripcion --> Gestiona sprites de los GameObjects

public class SpriteManager : MonoBehaviour {

	// Arrays de sprites
	public Sprite[] OpenedEnemyShips, ClosedEnemyShips, DestructionEnemyShips;

	public AudioClip OpenedEnemySound, ClosedEnemySound;

	AudioManager AM;



	void Start() {

		AM = FindObjectOfType<AudioManager> ();

	}
		

	// Devuelve un sprite con un color aleatorio y forma
	public Sprite GetSpriteColor(int Rdm, string ShipShape) {

		if (ShipShape.Equals ("Opened")) {

			return OpenedEnemyShips [Rdm];

		} else {

			return ClosedEnemyShips [Rdm];
		}

	}


	// Cambia la forma de la nave 
	// Asigna un sprite con el mismo color y distinta forma
	public void SwitchSpriteShipShape(int ColumnSize, int RowSize, EnemyManager EM) {

		for (int i = 0; i < ColumnSize; i++) {

			for (int j = 0; j < RowSize; j++) {

				if (!(EM.DestroyedEnemy (i, j))) {

					// Recorremos vector de sprites para determinar que color tiene asignado cada nave
					for (int k = 1; k < OpenedEnemyShips.Length; k++) {

						// Si coincide el color switcheamos forma de nave
						if (EM.GetEnemySprite (i, j) == OpenedEnemyShips [k]) {

							EM.SetEnemySprite (i, j, ClosedEnemyShips [k]);

							AM.Play (ClosedEnemySound);

						} else if (EM.GetEnemySprite (i, j) == ClosedEnemyShips [k]) {
					
							EM.SetEnemySprite (i, j, OpenedEnemyShips [k]);

							AM.Play (OpenedEnemySound);

						} 

					}

				}
					
			}

		}

	}


	// Recibe el objeto colisionado y el manejador de naves enemigas
	// Determina que objeto (nave enemiga) de las almacenadas en el array es exactamente el mismo que el colisionado
	// Llama a otro metodo pasandole el sprite, el manejador de naves de enemigas y los indices
	public void SetDestructionSprite(GameObject GO, EnemyManager EM) {

		for (int i = 0; i < EM.GetColumnSize() ; i++) {

			for (int j = 0; j < EM.GetRowSize() ; j++) {

				if (EM.AllEnemies [i, j].gameObject.Equals (GO)) {

					FindSpriteColor (GO.GetComponent<SpriteRenderer>().sprite, EM, i, j, true);

				}

			}

		}

	}


	// Recibe el sprite actual del objeto colisionado y sus indices
	// Averigua el color del sprite del objeto colisionado
	// Le asigna el sprite de destruccion correspondiente si ForDestruction es verdadero
	// Devuelve el numero del color del sprite
	public int FindSpriteColor (Sprite Sp, EnemyManager EM, int i, int j, bool ForDestruction) {

		int ColorNumber = 0;

		for (int k = 1; k < OpenedEnemyShips.Length; k++) {

			if ((OpenedEnemyShips [k] == Sp) | (ClosedEnemyShips [k] == Sp)) {

				ColorNumber = k;

				if (ForDestruction) {

					EM.SetEnemySprite (i, j, DestructionEnemyShips [k - 1]);

				} 
					
			}

		}

		return ColorNumber;

	}

}


