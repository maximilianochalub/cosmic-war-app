using UnityEngine;
using System.Collections;

// Clase --> PlayerBulletShooting
// Descripcion --> Gestiona disparo jugador

public class PlayerBulletShooting : MonoBehaviour {

	// Bala jugador y fuerza de esta
	public Rigidbody2D BulletPrefab;

	int BulletForceSpeed = 500;

	// Sonido
	public AudioClip PlayerShooting;

	AudioManager AM;


	void Start () {

		AM = FindObjectOfType <AudioManager> ();
		
	}


	void Update () {
		
		//Disparar bala al presionar barra espaciadora
		if (Input.GetKeyUp (KeyCode.Space)) {

			//Añadimos fuerza
			Rigidbody2D BulletInstance;

			BulletInstance = Instantiate (BulletPrefab, transform.position, Quaternion.identity) as Rigidbody2D;

			BulletInstance.AddForce(Vector3.up * BulletForceSpeed);

			// Reproducimos
			AM.Play (PlayerShooting);

		}
			
	}

}