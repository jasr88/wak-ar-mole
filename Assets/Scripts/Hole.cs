using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WhackARmole {
	public class Hole :MonoBehaviour {
		public List<Mole> molesTypes;
		public Mole activeMole;
		public bool isReady;

		private void OnEnable() {
			SetMoles ();
		}

		public void SetMoles() {
			molesTypes = new List<Mole>(GetComponentsInChildren<Mole> ());
		}

		private Bounds Bounds {
			get => GetComponentInChildren<Renderer> ().bounds;
		}

		public bool isReadyToSpawn = false;
		public Vector3 Size {
			get => new Vector2 (Bounds.size.x, Bounds.size.z);
		}

		public Mole GetMoleToSpawn() {
			isReady = false;
			foreach (Mole mole in molesTypes) {
				float random = Random.Range (0.0f, 1.0f);
				if (mole.probability <= random) {
					activeMole = mole;
				}
			}
			activeMole = molesTypes[0];
			return activeMole;
		}

	}
}
