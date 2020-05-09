using System.Collections.Generic;
using UnityEngine;

namespace WhackARmole {
	public class Board :MonoBehaviour {
		private GameObject boardGO;
		private List<Hole> holes = new List<Hole> ();
		private List<Hole> activeHoles = new List<Hole> ();
		private GameManager gm;
		private bool isFirstRound = true;

		private void OnEnable() {
			Debug.Log ("Enable Board");
			gm = GameManager.Instance;
			gm.onPhaseRoundChange += SpawnMolesRound;
			gm.onGameEnded += HideSpawnedMoles;
			SetBoardGo ();
			SetHoles ();
		}

		private void SetBoardGo() {
			boardGO = transform.GetChild (0).gameObject;
		}

		public void SetHoles() {
			holes = new List<Hole> (GetComponentsInChildren<Hole> ());
		}

		private void SpawnMolesRound() {
			Debug.Log ("Change Phase Round");
			if (!isFirstRound) {
				Debug.Log ("Hide Moles");
				HideSpawnedMoles ();
			}
			Debug.Log ("Show Moles");
			SelectHolesToSpawn ();
			isFirstRound = false;
		}

		public void SelectHolesToSpawn() {
			for (int n = 0; n < gm.phases[gm.currentPhase].activeMolesCount; n++) {
				int holeIndex = Random.Range (0, holes.Count);
				activeHoles.Add (holes[holeIndex]);
				Mole moleToSpawn = holes[holeIndex].GetMoleToSpawn ();
				moleToSpawn.ShowMole ();
			}
		}

		public void HideSpawnedMoles() {
			foreach(Hole h in activeHoles) {
				if (h.activeMole.isUp) {
					h.activeMole.HideMole ();
				}
			}
		}
	}
}
