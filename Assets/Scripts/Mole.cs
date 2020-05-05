using UnityEngine;
using WhackARmole;

public class Mole :BaseMole {
	
	public new void ShowMole() {
		base.ShowMole ();
		// Sentences to execute when a mole is showed
	}

	public new void HideMole() {
		base.HideMole ();
		// Sentences to execute when a mole is hidden
	}

	private void WhackMole() {
		// Extra code to execute when a mole is whacked by the user...
	}

	private void OnEnable() {
		onWhackMole += WhackMole;
		/*********************************************************************************
		 * Subscribe to other delegates (Subscribe only to the ones you are going to use)
		 *********************************************************************************/

		// onShowAnimationStart += MyCoolMethod();
		// onShowAnimationEnd += MyCoolMethod();

		// onHideAnimationStart += MyCoolMethod();
		// onHideAnimationEnd += MyCoolMethod();

		// onWhackAnimationStart += MyCoolMethod();
		// onWhackAnimationEnd += MyCoolMethod(); 
	}

	private void OnDisable() {
		onWhackMole -= WhackMole;
		/**********************************
		 * Unsubscribe to other delegates 
		 **********************************/
		// Unsubscribe to other delegates
		// onShowAnimationStart -= MyCoolMethod();
		// onShowAnimationEnd -= MyCoolMethod();

		// onHideAnimationStart -= MyCoolMethod();
		// onHideAnimationEnd -= MyCoolMethod();

		// onWhackAnimationStart -= MyCoolMethod();
		// onWhackAnimationEnd -= MyCoolMethod(); 
	}

}