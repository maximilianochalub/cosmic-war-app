using UnityEngine;
using System.Collections;

// Clase --> BulletWallBlockCollision
// Descripcion --> Destruye bloques de barreras cuando son colisionadas

public class BulletWallBlockCollision : MonoBehaviour {

	// Al haber colision se destruye el bloque correspondiente y la bala enemiga o del jugador
	void OnCollisionEnter2D(Collision2D Col) {

		Destroy (gameObject);

		Destroy (Col.gameObject);

	}
}
