using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Clase --> EnemyBulletShooting
// Descripcion --> Se encarga de establecer los disparos de naves enemigas

public class EnemyBulletShooting : MonoBehaviour {

	// Tiempo entre disparos
	Timer T;

	// Acceso a otras clases
	EnemyManager EM;

	EnemiesCreation EC;

	// Fuerza de la bala y velocidad
	public Rigidbody2D BulletPrefab;

	// Numeros aleatorios
	int Rdm;

	float RdmSeconds;

	// Indices
	int RowIdx, ColIdx;

	// Naves enemigas habilitadas para disparar
	List<GameObject> EnemiesToShoot;

	// Ultima nave en haber disparado
	GameObject LastShipToShoot;


	// Use this for initialization
	void Start () {

		// Seteamos timer y variables
		T = new Timer (3f);

		EC = GameObject.FindGameObjectWithTag ("EnemiesParent").GetComponent<EnemiesCreation> ();

		EM = GameObject.FindGameObjectWithTag ("EnemiesParent").GetComponent<EnemyManager> ();

		EnemiesToShoot = new List<GameObject> ();

		LastShipToShoot = new GameObject ();
	
	}


	// Update is called once per frame
	void Update () {

		if (!(GameOver.GameOverDelay)) {

			// Verificamos si la asignacion de colores ha finalizado y si no hay imagen mostrandose en pantalla
			if ((EC.SetRandomColorsHasCompleted) & (!(ImagesOnSceneManager.GetShowingImageOnScreen ()))) {

				// Decrementamos tiempo
				T.DecTime (Time.deltaTime);

				// Recorremos array bidimensional de naves enemigas
				for (int i = 0; i < EM.GetColumnSize (); i++) {

					for (int j = 0; j < EM.GetRowSize (); j++) {

						// Si la nave no fue destruida y es la ultima en su columna
						if ((!(EM.DestroyedEnemy (i, j))) & (ColumnLastShip (EM.GetAllEnemies () [i, j]))) {
						
							// Si ya no se encuentra disparando
							if (!(EnemiesToShoot.Contains (EM.GetAllEnemies () [i, j]))) {

								// La añadimos
								EnemiesToShoot.Add (EM.GetAllEnemies () [i, j]);

							}

						}

					}

				}

				// Evaluamos si es tiempo de lanzar un disparo enemigo
				if (T.GetTime () < 0) {

					// Verificamos si el nivel no se ha completado y no hay imagenes mostrandose 
					if ((CheckIfLevelCompleted.GetCheckVariable ()) & (!(ImagesOnSceneManager.GetShowingImageOnScreen ()))) {

						// Obtenemos un indice aleatorio para las naves habilitadas a disparar
						Rdm = Random.Range (0, EnemiesToShoot.Count - 1);

						int i = EM.GetEnemyRowIndex (EnemiesToShoot [Rdm]);

						int j = EM.GetEnemyColIndex (EnemiesToShoot [Rdm]);

						// Evaluamos si la seleccionada no es la que disparo anteriormente
						if ((!(LastShipToShoot.Equals (EnemiesToShoot [Rdm]))) & (!(EM.DestroyedEnemy (i, j)))) {

							// Detectamos nave y disparamos
							Vector3 pos = EnemiesToShoot [Rdm].transform.position;

							pos.y -= 0.5f;

							LastShipToShoot = EnemiesToShoot [Rdm];

							// Instanciamos bala
							Rigidbody2D BulletInstance;

							BulletInstance = Instantiate (BulletPrefab, pos, Quaternion.identity) as Rigidbody2D;

							BulletInstance.AddForce (Vector3.down);

						}

						// Limpiamos la lista
						EnemiesToShoot.Clear ();

						// Asignamos un tiempo arbitrario entre 0.5 y 3 segundos para el proximo disparo
						RdmSeconds = Random.Range (0.5f, 3);

						T.SetTime (RdmSeconds);

					}
						
				}
				
			}

		}

	}


	// Se encarga de verificar si la nave dada por GO es la ultima en su columna
	bool ColumnLastShip(GameObject GO) {

		bool Val = true;

		// Obtenemos indice fila y columna del objeto en cuestion
		RowIdx = EM.GetEnemyRowIndex (GO);

		ColIdx = EM.GetEnemyColIndex (GO);

		// Recorremos el array de naves enemigas y asignamos
		for (int i = 0; i < EM.GetColumnSize (); i++) {

			for (int j = 0; j < EM.GetRowSize (); j++) {

				if ((i > RowIdx) & (j == ColIdx)) {

					if (!(EM.DestroyedEnemy (i, j))) {

						Val = false;

					}
				
				}
					
			}

		}
			
		return Val;

	}

}
