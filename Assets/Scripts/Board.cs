using System.Collections.Generic;
using UnityEngine;

namespace WakARmole {
	public class Board :MonoBehaviour {
		[SerializeField]
		private GameObject boardGO;
		[SerializeField]
		private Hole holePrefab;
		[SerializeField]
		[Range (0, 10)] private float paddingX = 0;
		[SerializeField]
		[Range (0, 10)] private float paddingZ = 0;
		[SerializeField]
		private bool autoArrangeHoles = true;

		private List<Hole> holes = new List<Hole> ();

		// REMOVE THISSSS ONLY FOR TEST
		private void Start() {

			Renderer boardMesh = boardGO.GetComponent<Renderer> ();

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x);
			int zCount = Mathf.FloorToInt (boardMesh.bounds.size.z / holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z);
			Debug.LogFormat ("boardMesh.bounds.size.x: {0}      holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x: {1}", boardMesh.bounds.size.x, holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x);
			Debug.LogFormat ("boardMesh.bounds.size.z: {0}      holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z: {1}", boardMesh.bounds.size.z, holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z);

			float xOffset = boardMesh.bounds.size.x / (float)xCount + paddingX;
			float yOffset = boardMesh.bounds.size.y;
			float zOffset = boardMesh.bounds.size.z / (float)zCount + paddingZ;

			float holeXSize = holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x;
			float holeZSize = holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z;

			for (int x = 0; x < xCount; x++) {
				for (int z = 0; z < zCount; z++) {
					//TODO: Change for a object Pooler
					GameObject hole = Instantiate (holePrefab.gameObject);
					hole.transform.localPosition = new Vector3 (((x - xCount / 2.0f) * (holeXSize+paddingX/2)) + (holeXSize / 2.0f), yOffset, ((z - zCount / 2.0f) * (holeZSize + paddingZ / 2)) + (holeZSize / 2.0f));
					hole.transform.parent = boardGO.transform;
					hole.name = "Hole (" + x + "," + z + ")";
				}
			}
		}

		public void PopulateBoard() {
			if (boardGO == null) {
#if UNITY_EDITOR
				Debug.LogError ("You must to assign a GameObject to representante the virtual board for the game, please verify your Board prefab and assign a suitable 3D model for the boardGO property");
#endif
				return;
			}

			if (holePrefab == null) {
#if UNITY_EDITOR
				Debug.LogError ("You must to assign a Hole Object to representante the virtual holes on the board, please verify your Board prefab and assign a suitable prefab for the holePrefab property");
#endif
				return;
			}

			Mesh boardMesh = boardGO.GetComponent<Mesh> ();

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holePrefab.Size.x);
			int yCount = Mathf.FloorToInt (boardMesh.bounds.size.y / holePrefab.Size.y);

			for (int x = 0; x >= xCount; x++) {
				for (int y = 0; y >= yCount; y++) {
					//TODO: Change for a object Pooler
					GameObject hole = Instantiate (holePrefab.gameObject, boardGO.transform);
					// TE QUEDASTE AQUIIIIIIIIIIII
					hole.transform.SetPositionAndRotation (holePrefab.Size * 0.5f + new Vector3 (x, y, 0), Quaternion.identity);
				}
			}

		}


	}
}
