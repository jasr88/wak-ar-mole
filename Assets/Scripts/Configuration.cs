using UnityEngine;

namespace WhackARmole {
	public class Configuration :MonoBehaviour {
		[Header ("Time Configurations")]
		[Tooltip ("Initial countdown duration in seconds (Default Value: 3s)")]
		public int initialCountdown = 3;

		[Header ("Score Configurations")]
		public int startingScore = 0;
		public int minimunScore = -4;

		[Header ("Phases")]
		public Phase[] phases;

		private void Awake() {
			GameManager.Instance.Timer = GetComponent<Timer> ();
			GameManager.Instance.gameState = GameStates.DEFAULT;
		}

		private void SetUp() {
			GameManager.Instance.initialCountdown = initialCountdown;
			GameManager.Instance.score = startingScore;
			GameManager.Instance.minScore = minimunScore;
			GameManager.Instance.phases = phases;
			GameManager.Instance.gameDuration = 0;
			foreach (Phase p in phases) {
				GameManager.Instance.gameDuration += p.duration;
			}
			
		}

		#region Adding and Removing Delegates region
		private void OnEnable() {
			GameManager.Instance.onSetUp += SetUp;
		}

		private void OnDisable() {
			if (GameManager.Instance != null) {
				GameManager.Instance.onSetUp -= SetUp;
			}
		}
		#endregion
	}

	public enum GameStates {
		DEFAULT,
		STARTING,
		PLAYING,
		ENDED
	}
}