using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

// DO NOT MODIFY THIS CLAS ON YOUR IMPLEMENTATION, ONLY MODIFY IF YOU INTENT TO CREATE A PULL REQUEST
public class Timer :MonoBehaviour {

	private readonly WaitForSeconds gameTimeWaiting = new WaitForSeconds (1);
	private readonly WaitForSecondsRealtime realTimeWaiting = new WaitForSecondsRealtime (1);

	/// <summary>
	/// Countdown based on a custom UPDATE method simulated by a Coroutine, in this method the countdown is "smooth" trough every frame, so it can show miliseconds
	/// </summary>
	/// <param name="time">Number of seconds to countdown.</param>
	/// <param name="isRealSeconds">This parameter indicates if the countdown is on a game-scaled time or in real time</param>
	/// <param name="callback">Action called every frame during the countdown, this action recibes a float parameter that indicates the remaining time in the countdown</param>
	public Coroutine Countdown(
		float time,
		bool isRealSeconds = false,
		Action<float> callback = null
	) {
		return StartCoroutine (SmootCountDown (time, isRealSeconds, callback));
	}

	/// <summary>
	/// Countdown based Async methods, this is not the best aproach but it works, this method only show seconds and doesn't allow game scaled time.
	/// </summary>
	/// <param name="time">Number of seconds to countdown.</param>
	/// <param name="callback">Action called every second during the countdown, this action recibes a interger parameter that indicates the remaining time in the countdown</param>
	public static async void CountdownAsync(
		int time,
		Action<int> callbackBySecond = null
	) {
		int remainingTime = time;
		callbackBySecond?.Invoke (remainingTime);
		while (remainingTime > 0) {
			await Task.Delay (1000);
			remainingTime--;
			callbackBySecond?.Invoke (remainingTime);
		}
	}

	/// <summary>
	/// Countdown based on a coroutine, this IS the best aproach, this method only show seconds
	/// </summary>
	/// <param name="time">Number of seconds to countdown.</param>
	/// <param name="isRealSeconds">This parameter indicates if the countdown is on a game-scaled time or in real time</param>
	/// <param name="callback">Action called every second during the countdown, this action recibes a interger parameter that indicates the remaining time in the countdown</param>
	public Coroutine Countdown(
		int time,
		bool isRealSeconds,
		Action<int> callbackBySecond = null
	) {
		if (isRealSeconds) {
			return StartCoroutine (CountdownRealTime (time, callbackBySecond));
		} else {
			return StartCoroutine (CountdownGameTime (time, callbackBySecond));
		}
	}

	// Coroutine that simulates an Update loop only for the countdown
	private IEnumerator SmootCountDown(float time, bool isRealSeconds, Action<float> callback) {
		float remainingTime = time;
		callback?.Invoke (remainingTime);
		while (Math.Truncate (remainingTime * 100) > 0) {
			remainingTime -= isRealSeconds ? Time.unscaledDeltaTime : Time.deltaTime;
			callback?.Invoke (remainingTime);
			yield return null;
		}
		remainingTime = 0;
		callback?.Invoke (remainingTime);
	}

	// Coroutine that makes a countdown in real time and ignores the time scale in the game
	private IEnumerator CountdownRealTime(int time, Action<int> callbackBySecond) {
		int remainingTime = time;
		callbackBySecond?.Invoke (remainingTime);
		while (remainingTime > 0) {
			yield return realTimeWaiting;
			remainingTime--;
			callbackBySecond?.Invoke (remainingTime);
		}
	}

	// Coroutine that makes a countdown in a game time scale and ignores the real time (perfect if you need that the countdown pauses when the game does it)
	private IEnumerator CountdownGameTime(int time, Action<int> callbackBySecond) {
		int remainingTime = time;
		callbackBySecond?.Invoke (remainingTime);
		while (remainingTime > 0) {
			yield return gameTimeWaiting;
			remainingTime--;
			callbackBySecond?.Invoke (remainingTime);
		}
	}
}

