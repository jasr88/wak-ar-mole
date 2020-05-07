using UnityEngine;
using UnityEngine.SceneManagement;

namespace WhackARmole {
	public struct Phase {
		public float duration;
		public float moleLifetime;
		public float scoreMultiplier;
		public float startedAtTime;
		public int activeMolesCount;
	}

	public class GameManager :Singleton<GameManager> {
		protected GameManager() { }

		// DELTE THIS!!!!!
		public Phase[] phases = new Phase[3];
		private void PopulatePhases() {
			int i = 0;
			foreach (Phase p in phases) {
				Phase aux = new Phase ();
				aux.duration = Random.Range (5.0f, 10.0f);
				aux.moleLifetime = Random.Range (1.0f, 2.0f);
				aux.scoreMultiplier = Random.Range (1, 2.5f);
				aux.activeMolesCount = Random.Range (2, 6);
				aux.startedAtTime = -1.0f;
				phases[i] = aux;
				i++;
				gameDuration += aux.duration;
			}
		}

		// Review this variables
		public int currentPhase = 0;
		private float waitTime = 0;
		public float gameDuration; // This migth be the sum of all phases durations....

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
			currentPhase = 0;
			waitTime = 0;
			gameDuration = 0;
			PopulatePhases ();

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
				gameLoopCorroutine = timer.Countdown (gameDuration, false, CustomUpdate);
			}
		}

		public void UpdateScore(int scoreModificator) {
			if (gameState == GameStates.PLAYING) {
				score += scoreModificator;
				onUpdateScore?.Invoke (score);
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
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			onGamePlayUpdate -= ChangePhase;
		}

		private void ChangePhase(float remainingTime) {
			if (gameDuration == remainingTime) { // If is the beggining of the game
				phases[currentPhase].startedAtTime = Time.time;
				// Debug.LogFormat ("Phase: {0} at remainingTime: {1}", currentPhase, remainingTime);
			}

			float elapsedTime = gameDuration - remainingTime;
			float remainingPhaseTime = Mathf.Round ((waitTime + phases[currentPhase].duration - elapsedTime)) * 100 / 100;

			if (Mathf.Approximately (remainingPhaseTime, 0) && currentPhase < phases.Length - 1) { // The phase is over and is not the last phase on the list
				waitTime += phases[currentPhase].duration;
				currentPhase++;
				phases[currentPhase].startedAtTime = Time.time;
				// Debug.LogFormat ("Phase: {0} at remainingTime: {1}", currentPhase, remainingTime);
			}
		}
	}
}