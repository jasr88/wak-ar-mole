using UnityEngine;

namespace WakARmole {
	public class Configuration :MonoBehaviour {
		[Header ("Time Configurations")]
		[Tooltip ("Initial countdown duration in seconds (Default Value: 3s)")]
		public int initialCountdown = 3;
		[Tooltip ("Gameplay Duration in seconds (Default Value: 35s)")]
		public float gameDuration = 35.0f;

		[Header ("Score Configurations")]
		public int startingScore = 0;
		public int minimunScore = -4;

		private void Awake() {
			GameManager.Instance.Timer = GetComponent<Timer> ();
			GameManager.Instance.gameState = GameStates.DEFAULT;
		}

		private void SetUp() {
			GameManager.Instance.initialCountdown = initialCountdown;
			GameManager.Instance.gameDuration = gameDuration;
			GameManager.Instance.score = startingScore;
			GameManager.Instance.minScore = minimunScore;
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