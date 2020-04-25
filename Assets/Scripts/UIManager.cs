using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WakARmole {
	public class UIManager :MonoBehaviour {
		[Header ("UI Components")]
		[Header ("Dinamyc Text (TextMeshPro UI)")]
		[SerializeField]
		private TextMeshProUGUI timer = null;
		[SerializeField]
		private TextMeshProUGUI countDown = null;
		[SerializeField]
		private TextMeshProUGUI score = null;

		[Header ("Buttons")]
		[SerializeField]
		private Button startButton = null;
		[SerializeField]
		private Button restartButton = null;

		private decimal formatedTimeLeft;

		// Start is called before the first frame update
		void Start() {
			ResetUITexts ();
			ButtonsSetUp ();
		}

		public void ButtonsSetUp() {
			startButton.onClick.AddListener (
				() => {
					GameManager.Instance.StartInitialCountdown ();
				});
			restartButton.onClick.AddListener (
				() => {
					GameManager.Instance.RestartGame ();
				});
		}

		public void ResetUITexts() {
			timer.SetText ("");
			score.SetText ("");
			countDown.SetText ("");
		}

		public void EnableRestartButton() {
			restartButton.gameObject.SetActive (true);
		}

		public void UpdateCountDown(int timeLeft) {
			countDown.text = timeLeft.ToString ();
			if (timeLeft == 0) {
				Destroy (countDown.gameObject);
			}
		}

		public void UpdateRemainingTime(float timeLeft) {
			formatedTimeLeft = (decimal)(Math.Truncate (timeLeft * 100) / 100);
			timer.text = formatedTimeLeft.ToString ("F2");
			// TODO: Agregar animaciones y sonidos para los ultimos 10 segundos
		}

		public void UpdateScore(int score) {
			this.score.text = score.ToString ();
			// TODO: Agregar animaciones y sonidos cuando el puntaje suba
			// TODO: Agregar animaciones y sonidos cuando el puntaje baje
		}

		#region Adding and Removing Delegates region
		private void OnEnable() {
			GameManager.Instance.onInitialCountdownChange += UpdateCountDown;
			GameManager.Instance.onGamePlayUpdate += UpdateRemainingTime;
			GameManager.Instance.onGameEnded += EnableRestartButton;
			GameManager.Instance.onUpdateScore += UpdateScore;
		}

		private void OnDisable() {
			if (GameManager.Instance != null) {
				GameManager.Instance.onInitialCountdownChange -= UpdateCountDown;
				GameManager.Instance.onGamePlayUpdate -= UpdateRemainingTime;
				GameManager.Instance.onGameEnded -= EnableRestartButton;
				GameManager.Instance.onUpdateScore -= UpdateScore;
			}
		}
		#endregion
	}
}
