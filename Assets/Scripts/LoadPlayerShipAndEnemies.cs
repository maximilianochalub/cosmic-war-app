using UnityEngine;
using System.Collections;

public class LoadPlayerShipAndEnemies : MonoBehaviour {

	public GameObject PlayerShip;

	GameObject[] Enemies;

	// Use this for initialization
	void Start () {

		Enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		PlayerShip.SetActive (true);

		for (int i = 0; i < Enemies.Length; i++) {

			Enemies [i].SetActive (true);

		}

	}

}
