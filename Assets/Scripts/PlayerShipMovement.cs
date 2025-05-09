using UnityEngine;
using System.Collections;

// Clase --> PlayerShipMovement
// Descripcion --> Gestiona movimiento nave jugador

public class PlayerShipMovement : MonoBehaviour {

	// Maxima velocidad desplazamiento 
	float MaxSpeed = 5f;


	void Update () {

		// Movimiento nave jugador
		Vector3 pos = transform.position;

		pos.x += Input.GetAxis("Horizontal") * MaxSpeed * Time.deltaTime;

		// Limites offscreen
		if ((pos.x <= 7) & (pos.x >= -7)) { 

			transform.position = pos;

		}
	
	}
}
