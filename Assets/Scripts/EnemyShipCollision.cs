using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Clase --> EnemyShipCollision
// Descripcion --> Gestiona colisiones con naves enemigas

public class EnemyShipCollision : MonoBehaviour {

	// Variables
	SpriteManager SM;

	EnemyManager EM;

	CheckAdjacency CA;

	EnemiesCreation EC;

	Text[] TextElements;

	Text ScoreTxt, AddedScoreTxt;

	public AudioClip EnemyDestroyed;

	AudioManager AM;

	int ColorCode;


	void Start () {

		// Localizamos e inicializamos componentes
		TextElements = Canvas.FindObjectsOfType<Text> ();

		ScoreTxt = TextElements [1];

		AddedScoreTxt = TextElements [0];

		AM = FindObjectOfType<AudioManager> ();

		SM = FindObjectOfType<SpriteManager> ();

		EC = FindObjectOfType<EnemiesCreation> ();

		EM = GameObject.FindGameObjectWithTag ("EnemiesParent").GetComponent<EnemyManager> ();

		CA = FindObjectOfType<CheckAdjacency> ();

		// Seteamos score inicial
		SetScore (Score.GetScore ());
	
	}

		
	// En caso de colision con nave enemiga
	void OnCollisionEnter2D(Collision2D Col) {

		// Destruimos bala
		Destroy (gameObject);

		// Si colisiono con nave enemiga
		if (Col.gameObject.tag == "Enemy") {

			AM.Play (EnemyDestroyed);

			CA.Find (Col.gameObject);

			// Llamamos al metodo pasandole como paramentro la nave disparada 
			// Siempre primer elemento del array AdjacenciesToDestroy
			GotShot (CA.AdjacenciesToDestroy [0]);

			CA.AdjacenciesToDestroy.Clear ();

			SetText (Score.GetScore (), Score.GetAddedScore ());

			Score.SetAddedScore (0);

		} else if (Col.gameObject.tag == "EnemyBullet") {

			// Colisionan ambas balas
			Destroy (gameObject);

			Destroy (Col.gameObject);

		}
			
	}


	// Recibe como paramentro la nave enemiga disparada
	void GotShot(GameObject GO) {

		SetAddedScoreColor (GO);

		// Cuantas veces la nave fue disparada
		int ShotCounter;

		// Asignamos
		ShotCounter = EM.ShotCounter (GO);

		// Evaluamos si la nave debe ser destruida segun color
		if (EM.HasToBeDestroyed (GO, ShotCounter)) {

			// La destruimos con sus adyacentes y recursivas adyacentes
			foreach (GameObject Adj in CA.AdjacenciesToDestroy) {

				Score.AddToScore (50);

				// Asignamos sprite de destruccion
				SM.SetDestructionSprite (Adj, EM);

				// Destruimos objetos bala y nave
				Destroy (Adj, 0.15f);

			}

			Score.AddedScore = 50 * CA.AdjacenciesToDestroy.Count;
				
		} else {

			Score.AddToScore (50);

			Score.SetAddedScore (50);

		}

	}


	// Añade color al score parcial
	void SetAddedScoreColor(GameObject GO) {

		ColorCode = SM.FindSpriteColor (EM.GetEnemySprite(EM.GetEnemyRowIndex(GO), EM.GetEnemyColIndex(GO)), EM, 0, 0, false);

		switch (ColorCode) {

		case 1: 
			
			AddedScoreTxt.color = Color.blue;

			break;

		case 2: 
			
			AddedScoreTxt.color = Color.green;
	
			break;

		case 3:
			
			AddedScoreTxt.color = Color.red;

			break;

		case 4:
			
			AddedScoreTxt.color = Color.yellow;

			break;

		}

		// Desactivamos score parcial despues de ciertos segs
		EC.HideAddedScore ();

	}
		

	// Setea texto
	void SetText(string Score, string AddedScore) {

		ScoreTxt.text = "S C O R E     " + Score;

		AddedScoreTxt.text = "+ " + AddedScore;

	}


	// Setea score
	void SetScore(string Score) {

		ScoreTxt.text = "S C O R E     " + Score;

	}

}
