using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableMole :MonoBehaviour, IPointerClickHandler {
	private Mole mole;

	private void Start() {
		mole = GetComponentInParent<Mole> ();
	}

	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ("You just hit a Mole for " + mole.scoreValue + " points.");
		mole.WhackMole ();
	}
}
