using UnityEngine;
using System.Collections;

// Clase --> Score
// Descripcion --> Gestiona display de score en pantalla

public static class Score {

	static int Scr = 0;

	static int ScoreSize = 6;

	// Ceros adicionales en display
	static bool ZerosSet;

	public static int AddedScore;


	// Acumulamos score
	public static void AddToScore(int Val) {

		Scr += Val;

	}


	// Score parcial
	public static void SetAddedScore(int Added) {

		AddedScore = Added;

	}


	// Obtenemos score
	public static string GetScore() {

		string Zeros = null;

		ZerosSet = false;
	
		int i = 1;

		while (!(ZerosSet)) {

			if (Scr < Mathf.Pow(10, i)) {

			    Zeros = new string ('0', ScoreSize - i);

				ZerosSet = true;

			}

			i++;

		}
			
		return Zeros + Scr;

	}


	// Obtenemos score parcial
	public static string GetAddedScore() {

		return AddedScore.ToString();

	}

}
