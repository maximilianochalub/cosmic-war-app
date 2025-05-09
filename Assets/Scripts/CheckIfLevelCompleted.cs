using UnityEngine;
using System.Collections;

// Clase --> CheckIfLevelCompleted
// Descripcion --> Determina si el nivel ha sido completado

public class CheckIfLevelCompleted : MonoBehaviour {

	public static bool Check;

	static int DestroyedEnemies;

	// Setter y getter para la variable Check
	public static void SetCheckVariable(bool Val) {

		Check = Val;

	}

	public static bool GetCheckVariable() {

		return Check;

	}

	// Ejecucion
	// Cuenta los enemigos destruidos 
	// Si todos se han destruido setea la variable Check en false, caso contrario en true
	public static void Execute(EnemyManager EM) {

		DestroyedEnemies = 0;

		for (int i = 0; i < EM.GetColumnSize(); i++) {

			for (int j = 0; j < EM.GetRowSize(); j++) {

				if (EM.DestroyedEnemy(i, j)) {

					DestroyedEnemies++;

				}

			}

		}

		if (DestroyedEnemies == EM.GetRowSize() * EM.GetColumnSize()) {

			SetCheckVariable (false);

		} else {

			SetCheckVariable (true);

		}

	}

}
	
