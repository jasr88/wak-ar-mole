using UnityEngine;
using UnityEngine.SceneManagement;

namespace WhackARmole {

	public class GameManager :Singleton<GameManager> {
		protected GameManager() { }

		// DELTE THIS!!!!!
		public Phase[] phases;

		// Review this variables
		public int currentPhase = 0;
		private float waitTime = 0;
		private float phaseStartedAt = 0;
		private float roundWaitTime = 0;
		public int CurrentMultiplier { get { return phases[currentPhase].scoreMultiplier;  } }
		public float gameDuration; // This migth be the sum of all phases durations....
		public float elapsedTime;

		// Time Configuration Values (initialized from GameData.cs)
		public GameStates gameState;
		public int initialCountdown;

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
		public delegate void OnGamePhaseChange();
		public delegate void OnPhaseRoundChange();

		// Delegates Implementations
		public OnSetUp onSetUp;
		public OnUpdateScore onUpdateScore;
		public OnGameEnded onGameEnded;
		public OnInitialCountdownChange onInitialCountdownChange;
		public OnGamePlayCountdownChange onGamePlayUpdate;
		public OnGamePhaseChange onGamePhaseChange;
		public OnPhaseRoundChange onPhaseRoundChange;

		// Custom Timer class
		private Timer timer;
		public Timer Timer { set => timer = value; }

		public void StartInitialCountdown() {
			currentPhase = 0;
			waitTime = 0;
			gameDuration = 0;

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
				onGamePlayUpdate += ChangePhase;
				onGamePhaseChange?.Invoke ();
				onPhaseRoundChange?.Invoke ();
				gameLoopCorroutine = timer.Countdown (gameDuration, false, CustomUpdate);
			}
		}

		public void UpdateScore(int scoreModificator) {
			if (gameState == GameStates.PLAYING) {
				Debug.Log ("Current Score: " + score);
				Debug.Log ("Score to add: " + scoreModificator);
				Debug.Log ("Total Score: " + score+ scoreModificator);
				score += scoreModificator;
				onUpdateScore?.Invoke (score);
			}
		}

		private void CustomUpdate(float remainingTime) {
			onGamePlayUpdate?.Invoke (remainingTime);
			if (remainingTime == 0 || score <= minScore) {
				gameState = GameStates.ENDED;
				StopCoroutine (gameLoopCorroutine);
				onGameEnded?.Invoke ();
			}
		}

		public void RestartGame() {
			score = 0;
			roundWaitTime = 0;
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		private void ChangePhase(float remainingTime) {
			if (gameState == GameStates.ENDED) {
				return;
			}
			elapsedTime = gameDuration - remainingTime;
			float remainingPhaseTime = Mathf.Round ((waitTime + phases[currentPhase].duration - elapsedTime)) * 100 / 100;
			if (Mathf.Approximately (remainingPhaseTime, 0) && currentPhase < phases.Length - 1) { // The phase is over and is not the last phase on the list
				waitTime += phases[currentPhase].duration;
				currentPhase++;
				phaseStartedAt = elapsedTime;
				onGamePhaseChange?.Invoke ();
			}

			float remainingTimeInPhase = Mathf.Round ( (roundWaitTime + phases[currentPhase].roundDuration - elapsedTime) * 100) / 100;
			if (remainingTimeInPhase <= 0.01f) {
				roundWaitTime += phases[currentPhase].roundDuration;
				onPhaseRoundChange?.Invoke ();
			}
		}
	}
}