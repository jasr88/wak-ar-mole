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

	public new void WhackMole() {
		base.WhackMole ();
		// Extra code to execute when a mole is whacked by the user...
	}

	protected new void OnEnable() {
		base.OnEnable ();
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

	protected new void OnDisable() {
		base.OnDisable ();
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