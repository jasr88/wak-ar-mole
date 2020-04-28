using System.Collections.Generic;
using UnityEngine;

namespace WakARmole {
	public class Board :MonoBehaviour {
		[SerializeField]
		private GameObject boardGO;
		[SerializeField]
		private Hole holePrefab;
		[SerializeField]
		[Range (-1, 1)] private float paddingX = 0;
		[SerializeField]
		[Range (-1, 1)] private float paddingZ = 0;

		private List<Hole> holes = new List<Hole> ();

		// This method is only for run on the custom editor
		public void ResetBoard() {
			while (boardGO.transform.childCount != 0) {
				DestroyImmediate (boardGO.transform.GetChild (0).gameObject);
			}
		}
		
		public void GenerateBoard() {
			ResetBoard ();
			if (boardGO == null) {
				Debug.LogError ("You must to assign a GameObject to representante the virtual board for the game, please verify your Board prefab and assign a suitable 3D model for the boardGO property");
				return;
			}

			if (holePrefab == null) {
				Debug.LogError ("You must to assign a Hole Object to representante the virtual holes on the board, please verify your Board prefab and assign a suitable prefab for the holePrefab property");
				return;
			}
			Renderer boardMesh = boardGO.GetComponent<Renderer> ();
			Vector2 holeSize = new Vector2 (
				holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x + paddingX,
				holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z + paddingZ
				);

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holeSize.x);
			int zCount = Mathf.FloorToInt (boardMesh.bounds.size.z / holeSize.y);

			float cellWidth = holeSize.x - paddingX / 2.0f;
			float cellHeght = holeSize.y - paddingZ / 2.0f;

			float xOffset = holeSize.x / 2.0f;
			float yOffset = boardMesh.bounds.size.y + 0.01f;
			float zOffset = holeSize.y / 2.0f;

			for (int x = 0; x < xCount; x++) {
				for (int z = 0; z < zCount; z++) {
					GameObject hole = Instantiate (holePrefab.gameObject);
					hole.transform.localPosition = new Vector3 (((x - xCount / 2.0f) * cellWidth) + xOffset, yOffset, ((z - zCount / 2.0f) * cellHeght) + zOffset);
					hole.transform.parent = boardGO.transform;
					hole.name = "Hole (" + x + "," + z + ")";
				}
			}
		}

	}
}
