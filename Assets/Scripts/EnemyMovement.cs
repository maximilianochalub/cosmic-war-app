using UnityEngine;
using System.Collections;

// Clase --> EnemyMovement
// Descripcion --> Se encarga del desplazamiento de naves enemigas

public class EnemyMovement : MonoBehaviour {

	// Para acceder a las naves enemigas
	EnemyManager EM;

	// Para movimiento
	Vector3 GameObjectPosition;

	// Incrementos
	float XInc, YInc;


	void Start () {

		XInc = 0.15f;

		YInc = 0.8f;

		EM = FindObjectOfType <EnemyManager> ();

	}


	// Evalua la direccion y llama al metodo correspondiente
	public void Move(string Direction) {

		switch (Direction) {

		case "Down": 

			MoveDown ();

			//Debug.Log ("DOWN");

			break;

		case "Left":

			MoveLeft ();

			//Debug.Log ("LEFT");


			break;

		case "Right":

			MoveRight ();

			//Debug.Log ("RIGHT");


			break;
		}
	}


	// Movimiento hacia la derecha
	void MoveRight() {

		for (int i = 0; i < EM.GetColumnSize(); i++) {

			for (int j = 0; j < EM.GetRowSize(); j++) {

				GameObjectPosition = EM.GetEnemyPosition (i, j);

				GameObjectPosition.x += XInc;

				EM.SetEnemyPosition (i, j, GameObjectPosition);

			}

		}
			
	}


	// Movimiento hacia abajo
	void MoveDown() {
		
		for (int i = 0; i < EM.GetColumnSize(); i++) {

			for (int j = 0; j < EM.GetRowSize(); j++) {

				GameObjectPosition = EM.GetEnemyPosition (i, j);

				GameObjectPosition.y -= YInc;

				EM.SetEnemyPosition (i, j, GameObjectPosition);

			}

		}

	}


	// Movimiento hacia izquierda
	void MoveLeft() {
		
		for (int i = 0; i < EM.GetColumnSize(); i++) {

			for (int j = 0; j < EM.GetRowSize(); j++) {

				GameObjectPosition = EM.GetEnemyPosition (i, j);

				GameObjectPosition.x -= XInc;

				EM.SetEnemyPosition (i, j, GameObjectPosition);

			}

		}

	}

}
