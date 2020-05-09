using System.Collections;
using UnityEngine;

namespace WhackARmole {
	public abstract class BaseMole :MonoBehaviour {
		private Animator animator;

		public int scoreValue;
		public bool isUp;
		public float probability=0.5f;

		private readonly int showTrigger = Animator.StringToHash ("Show");
		private readonly int hideTrigger = Animator.StringToHash ("Hide");
		private readonly int whackTrigger = Animator.StringToHash ("Whack");

		private InteractableMole interactableMole;
		private Hole holeParent;

		private void Awake() {
			animator = GetComponent<Animator> ();
			interactableMole = GetComponentInChildren<InteractableMole> ();
			interactableMole.enabled = false;
		}

		public void SetHole(Hole parent) {
			holeParent = parent;
		}

		protected virtual void ShowMole() {
			interactableMole.enabled = true;
			isUp = true;
			animator.SetTrigger (showTrigger);
		}

		protected virtual void HideMole() {
			interactableMole.enabled = false;
			isUp = false;
			animator.SetTrigger (hideTrigger);
		}

		protected void WhackMole() {
			interactableMole.enabled = false;
			isUp = false;
			animator.SetTrigger (whackTrigger);
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

	}
}
