using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WhackARmole {
	public class Hole :MonoBehaviour {
		private List<Mole> molesTypes;

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

		public static void PopulateHole(GameObject hole, GameObject[] moles) {
			int index = 0;
			Hole holeBehaviour = hole.GetComponent<Hole> ();
			holeBehaviour.molesTypes = new List<Mole> ();

			foreach (GameObject mole in moles) {
				GameObject moleInstance = (GameObject)PrefabUtility.InstantiatePrefab (mole as GameObject);
				moleInstance.name = "Mole " + index;
				moleInstance.transform.SetParent (hole.transform);
				moleInstance.transform.localPosition = Vector3.zero;
				holeBehaviour.molesTypes.Add (moleInstance.GetComponent<Mole>());
				index++;
			}
			
		}

		public Mole GetMoleToSpawn() {
			foreach (Mole mole in molesTypes) {
				float random = Random.Range (0.0f, 1.0f);
				if (mole.probability <= random) {
					return mole;
				}
			}
			return molesTypes[0];
		}

	}
}
