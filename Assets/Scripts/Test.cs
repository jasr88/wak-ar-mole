using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WakARmole;

public class Test : MonoBehaviour
{
	public void AddToScore(int scoreModifier) {
		GameManager.Instance.UpdateScore (scoreModifier);
	}

}
