using System.Collections.Generic;
using UnityEngine;

namespace WhackARmole {
	public class Board :MonoBehaviour {
		private GameObject boardGO;
		private List<Hole> holes = new List<Hole> ();

		private void OnEnable() {
			Debug.Log ("Enable Board");
			GameManager.Instance.onGamePlayUpdate += SelectRoundHolesToSpawn;
			SetBoardGo ();
			SetHoles ();
		}

		private void SetBoardGo() {
			Debug.Log (transform.GetChild (0).gameObject.name);
			boardGO = transform.GetChild (0).gameObject;
		}

		public void SetHoles() {
			holes = new List<Hole>(GetComponentsInChildren<Hole>());
		}

		public void SelectRoundHolesToSpawn(float remainingTime) {
			for (int n = 0; n <= 3; n++) {
				int holeIndex = Random.Range (0, holes.Count);
				holes[holeIndex].GetMoleToSpawn ().ShowMole ();
			}
		}

	}
}
