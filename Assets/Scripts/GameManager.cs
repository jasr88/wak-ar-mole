using UnityEngine;
using UnityEngine.SceneManagement;

namespace WakARmole {

	public class GameManager :Singleton<GameManager> {
		protected GameManager() {}

		// Time Configuration Values (initialized from GameData.cs)
		public GameStates gameState;
		public int initialCountdown;
		public float gameDuration;
		public Coroutine gameLoopCorroutine;

		// Player's Score variables
		public int score;
		public int minScore;

		// Delegates Templates
		public delegate void OnSetUp();
		public delegate void OnGameEnded();
		public delegate void OnUpdateScore(int score);
		public delegate void OnInitialCountdownChange(int currentSecond);
		public delegate void OnGamePlayCountdownChange(float remainingTime);

		// Delegates Implementations
		public OnSetUp onSetUp;
		public OnUpdateScore onUpdateScore;
		public OnGameEnded onGameEnded;
		public OnInitialCountdownChange onInitialCountdownChange;
		public OnGamePlayCountdownChange onGamePlayUpdate;

		// Custom Timer class
		private Timer timer;
		public Timer Timer { set => timer = value; }

		public void StartInitialCountdown() {
			gameState = GameStates.STARTING;
			onSetUp?.Invoke ();
			timer.Countdown (initialCountdown, false, CountingDown);
		}

		private void CountingDown(int timeLeft) {
			onInitialCountdownChange?.Invoke (timeLeft);
			if (timeLeft == 0) {
				// Game Starts, this timer execute the CustomUpdate Method every frame
				gameState = GameStates.PLAYING;
				UpdateScore (score);
				gameLoopCorroutine = timer.Countdown (gameDuration, false, CustomUpdate);
			}
		}

		public void UpdateScore(int scoreModificator) {
			if (gameState == GameStates.PLAYING) {
				score += scoreModificator;
				onUpdateScore?.Invoke(score);
			}
		}

		private void CustomUpdate(float remainingTime) {
			onGamePlayUpdate?.Invoke (remainingTime);
			if (remainingTime == 0 || score <= minScore) {
				StopCoroutine (gameLoopCorroutine);
				gameState = GameStates.ENDED;
				onGameEnded?.Invoke ();
			}
		}

		public void RestartGame() {
			score = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
		}
	}
}