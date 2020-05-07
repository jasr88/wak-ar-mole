using System.Collections.Generic;
using UnityEngine;

namespace WhackARmole {
	public class Board :MonoBehaviour {
		private GameObject boardGO;
		private List<Hole> holes = new List<Hole> ();

		private int currentPhase = 0;
		private float[] pahsesTestTimes = new float[] { 0.0f, 6.0f, 11.0f, 14.0f, 16.0f, 18.0f };
		private float[] pahsesTestLifetime = new float[] { 0.0f, 2.5f, 2.0f, 1.8f, 1.2f, 1f, 1f };
		private int[] pahsesTestMoles = new int[] { 0, 1, 2, 3, 5, 6, 8 };
		private List<Mole> currentMoles = new List<Mole> ();


		private GameManager gm;
		private float timeOnCurrentPhase = 0;

		private void OnEnable() {
			Debug.Log ("Enable Board");
			gm = GameManager.Instance;
			gm.onGamePlayUpdate += SelectRoundHolesToSpawn;
			SetBoardGo ();
			SetHoles ();
		}

		private void SetBoardGo() {
			Debug.Log (transform.GetChild (0).gameObject.name);
			boardGO = transform.GetChild (0).gameObject;
		}

		public void SetHoles() {
			holes = new List<Hole> (GetComponentsInChildren<Hole> ());
		}

		public void SelectRoundHolesToSpawn(float remainingTime) {
			if (gm.gameDuration == remainingTime) {
				//ShowMoles ();
				Debug.Log ("Show first Moles");
			}

			float elapsedLifetime = Mathf.Round ((Time.time - gm.phases[currentPhase].startedAtTime - timeOnCurrentPhase) * 100) / 100;
			Debug.LogFormat ("elapsedLifetime: {0}, moleLifetime: {1}", elapsedLifetime, Mathf.Round ((gm.phases[currentPhase].moleLifetime * 100) / 100));
			if (Mathf.Approximately (Mathf.Round ((gm.phases[currentPhase].moleLifetime * 100) / 100), elapsedLifetime)) {
				timeOnCurrentPhase += gm.phases[currentPhase].moleLifetime;
				Debug.Log ("Hide Moles at: " + elapsedLifetime);
				Debug.Log ("Show Moles at: " + elapsedLifetime);
			}

			if (remainingTime == 0) {
				Debug.Log ("Hide last Moles");
			}
		}

		private void ShowMoles() {
			for (int n = 0; n < GameManager.Instance.phases[GameManager.Instance.currentPhase].activeMolesCount; n++) {
				int holeIndex = Random.Range (0, holes.Count);
				holes[holeIndex].GetMoleToSpawn ().ShowMole ();
				currentMoles.Add (holes[holeIndex].GetMoleToSpawn ());
			}
		}

		private void HideMoles() {
			foreach (Mole m in currentMoles) {
				if (m.isUp) {
					m.HideMole ();
				}
			}
		}

	}
}
