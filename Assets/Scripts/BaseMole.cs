using UnityEngine;
using UnityEngine.EventSystems;

namespace WhackARmole {
	public abstract class BaseMole :MonoBehaviour, IPointerDownHandler {
		private Animator animator;

		public int scoreValue;
		public float lifeTime;
		public float probability=0.5f;

		private readonly int showTrigger = Animator.StringToHash ("Show");
		private readonly int hideTrigger = Animator.StringToHash ("Hide");
		private readonly int whackTrigger = Animator.StringToHash ("Whack");

		public delegate void OnWhackMole();
		public OnWhackMole onWhackMole;

		private void Awake() {
			animator = GetComponent<Animator> ();
		}

		protected virtual void ShowMole() {
			animator.SetTrigger (showTrigger);
		}

		protected virtual void HideMole() {
			animator.SetTrigger (hideTrigger);
		}

		private void WhackMole() {
			animator.SetTrigger (whackTrigger);
			onWhackMole?.Invoke ();
		}

		#region Animation delegates and listeners
		// Show Animations Delegates and listeners
		public delegate void OnShowAnimationStart();
		public delegate void OnShowAnimationEnd();
		public OnShowAnimationStart onShowAnimationStart;
		public OnShowAnimationEnd onShowAnimationEnd;

		public void ShowAnimationStartListener() {
			onShowAnimationStart?.Invoke ();
		}

		public void ShowAnimationEndListener() {
			onShowAnimationEnd?.Invoke ();
		}

		// Hide Animations Delegates and listeners
		public delegate void OnHideAnimationStart();
		public delegate void OnHideAnimationEnd();
		public OnHideAnimationStart onHideAnimationStart;
		public OnHideAnimationEnd onHideAnimationEnd;

		public void HideAnimationStartListener() {
			onHideAnimationStart?.Invoke ();
		}

		public void HideAnimationEndListener() {
			onHideAnimationEnd?.Invoke ();
		}

		// Whack Animations Delegates and listeners
		public delegate void OnWhackAnimationStart();
		public delegate void OnWhackAnimationEnd();
		public OnWhackAnimationStart onWhackAnimationStart;
		public OnWhackAnimationEnd onWhackAnimationEnd;

		public void WhackAnimationStartListener() {
			onWhackAnimationStart?.Invoke ();
		}

		public void WhackAnimationEndListener() {
			onWhackAnimationEnd?.Invoke ();
		}
		#endregion

		public void OnPointerDown(PointerEventData eventData) {
#if UNITY_EDITOR
			Debug.Log ("You just hit a Mole for " + scoreValue + " points.");
#endif
			WhackMole ();
		}

	}
}
