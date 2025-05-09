using UnityEngine;
using System.Collections;

// Clase --> Timer
// Descripcion --> Temporizadores utilizados 

public class Timer {


	float T;


	// Constructor
	public Timer(float Time) {

		T = Time;

	}
		

	// Setter y Getter
	public void SetTime(float Time) {

		T = Time;
	
	}


	public float GetTime() {

		return T;

	}


	// Decrementa un cierto tiempo al timer
	public void DecTime(float Time) {

		T -= Time;

	}



}
