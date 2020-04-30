using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WakARmole {
	public class Hole :MonoBehaviour {
		private GameObject boardGO;
		private Bounds Bounds {
			get => GetComponentInChildren<Renderer> ().bounds;
		}

		public bool isReadyToSpawn = false;
		public Vector3 Size {
			get => new Vector2(Bounds.size.x, Bounds.size.z);
		}

		public static Hole CreateHoleInstance() {
			GameObject hole = Instantiate (Resources.Load ("HoleContainer", typeof (GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
			hole.name = "Hole Template";
			return hole.GetComponent<Hole> ();
		}

	}
}
