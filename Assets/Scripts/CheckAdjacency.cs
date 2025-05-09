using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Clase --> CheckAdjacency
// Descripcion --> Determina adyacentes a ser destruidos

public class CheckAdjacency : MonoBehaviour {

	EnemyManager EM;

	SpriteManager SM;

	// Lista que almacena todos las naves enemigas a ser destruidas
	public List<GameObject> AdjacenciesToDestroy; 

	// Listas utilizadas para gestion de las adyacencias
	// Adjacencies --> Lista que almacena adyacencias (desde el primer elemento semilla disparado)
	// NewAdjacencies --> Lista que almacena las adyacencias coincidentes en color de los elementos de Adjacencies

	List<GameObject> Adjacencies, NewAdjacencies;

	// Almacenan indice fila y columna
	int RowIdx, ColIdx;

	// Almacena el codigo de color del sprite
	int SpColor;

	// Indica si se ha añadido un elemento adyacente
	bool AdjacentIsAdded;

	// Indica numero de veces que el metodo ManageAdjacents() es invocado
	int ManageAdjacentsGetCalled, Count;

	void Start() {

		// Instanciamos e inicializamos

		Adjacencies = new List<GameObject> ();

		NewAdjacencies = new List<GameObject> ();

		AdjacenciesToDestroy = new List<GameObject> ();

		EM = FindObjectOfType<EnemyManager> ();

		SM = GameObject.FindGameObjectWithTag ("EnemiesParent").GetComponent<SpriteManager> ();

		RowIdx = 0;

		ColIdx = 0;

		ManageAdjacentsGetCalled = 0;

		Count = 0;

	}


	// Encuentra adyacencias para la nave enemiga (GO)
	public void Find(GameObject GO) {

		for (int i = 0; i < EM.GetColumnSize(); i++) {

			for (int j = 0; j < EM.GetRowSize(); j++) {

				if (!(EM.DestroyedEnemy(i, j))) {

					if (EM.AllEnemies [i, j].Equals (GO)) {

						// Primer elemento añadido a la lista
						// Sera el primer elemento destruido
						// Se destruira este elemento con todos sus adyacentes del mismo color
						// Esto se realizara de forma recursiva para sus adyacentes del mismo colr			

						AdjacenciesToDestroy.Add (EM.AllEnemies [i, j]);

						// Obtenemos codigo color
						SpColor = SM.FindSpriteColor (EM.GetEnemySprite (i, j), EM, i, j, false);

						// Añadimos para gestion de adyacencias
						Adjacencies.Add(EM.AllEnemies [i, j]);
					
						// Llamamos al metodo de gestion de adyacencias
						ManageAdjacents (Adjacencies);

					}

				}

			}

		}

	}


	// Este metodo recorre la lista de adyacencias y encuentra nuevas adyacencias almacenandolas en otra lista
	// Este proceso se itera hasta que no existan mas nuevas adyacencias
	void ManageAdjacents(List<GameObject> AdjacencyVector) {

		while (true) {

			// El corte de control se da si no se añadieron mas adyacencias
			if ((Count > 0) | (ManageAdjacentsGetCalled == 0)) {

				ManageAdjacentsGetCalled++;

				// Recorremos lista de adyacencias
				foreach (GameObject GO in Adjacencies) {

					// Almacenamos nro fila y columna y evaluamos sus adyacencias
					RowIdx = EM.GetEnemyRowIndex (GO);

					ColIdx = EM.GetEnemyColIndex (GO);

					EvaluateAdjacents (RowIdx, ColIdx);

				}

				// Almacenamos numero de adyacencias
				Count = Adjacencies.Count;

				// Las nuevas adyacencias iran en una nueva lista
				Adjacencies = new List<GameObject>(NewAdjacencies);

				// Borramos elementos
				NewAdjacencies.Clear ();

			} else {
				
				return;

			}

		}

	}


	// Este metodo evalua cada uno de los adyacentes de los elementos de la lista Adjacencies
	// Si encuentra un adyacente del mismo color lo añade a la lista NewAdjacencies 
	void EvaluateAdjacents(int i, int j) {

		// Elemento superior
		if (i - 1 >= 0) {

			if (VerifyIfAdddingToAdjacency (i - 1, j)) {

				AddNewAdjacency (i - 1, j);
	
			}

		}

		// Elemento inferior
		if (i + 1 <= EM.GetColumnSize () - 1) {

			if (VerifyIfAdddingToAdjacency (i + 1, j)) {

				AddNewAdjacency (i + 1, j);

			}	

		}

		// Elemento posterior
		if (j + 1 <= EM.GetRowSize () - 1) {

			if (VerifyIfAdddingToAdjacency (i, j + 1)) {

				AddNewAdjacency (i, j + 1);

			}

		}

		// Elemento anterior
		if (j - 1 >= 0) {

			if (VerifyIfAdddingToAdjacency (i, j - 1)) {

				AddNewAdjacency (i, j - 1);

			}

		}

	}


	// Este metodo evalua si añadir el elemento a la lista Adjacency
	// Si coincide el color con el adyacente evalua si el adyacente ya no fue añadido
	// De no ser asi lo añade y devuelve true; false caso contrario
	bool VerifyIfAdddingToAdjacency(int i, int j) {

		AdjacentIsAdded = false;

		if (!(EM.DestroyedEnemy (i, j))) {

			if (SpColor == SM.FindSpriteColor (EM.GetEnemySprite (i, j), EM, i, j, false)) {

				if (!(AdjacenciesToDestroy.Contains (EM.AllEnemies [i, j]))) {

					AdjacenciesToDestroy.Add (EM.AllEnemies [i, j]);

					AdjacentIsAdded = true;

				}

			}

		}

		return AdjacentIsAdded;

	}
		

	// Este metodo añade el elemento de indices i, j a la lista de nuevas adyacencias
	void AddNewAdjacency(int i, int j) {

		NewAdjacencies.Add (EM.AllEnemies [i, j]);

	}

}
