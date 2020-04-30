using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WakARmole {
	[ExecuteInEditMode]
	public class Board :MonoBehaviour {
		private GameObject boardGO;
		[SerializeField]
		private Hole holePrefab = null;
		[SerializeField]
		[Range (-1, 1)] private float paddingX = 0;
		[SerializeField]
		[Range (-1, 1)] private float paddingZ = 0;

		private List<Hole> holes = new List<Hole> ();

		private void OnEnable() {
			Debug.Log ("Enable Board");
			SetBoardGo ();
		}

		private void SetBoardGo() {
			Debug.Log (transform.GetChild (0).gameObject.name);
			boardGO = transform.GetChild (0).gameObject;
		}

		// This method is only for run on the custom editor
		public bool IsHolePrefabEmpty() {
			return holePrefab == null;
		}

		public void ResetBoard() {
			while (boardGO.transform.childCount != 0) {
				DestroyImmediate (boardGO.transform.GetChild (0).gameObject);
			}
		}
		
		public void GenerateBoard() {
			if (holePrefab == null) {
				Debug.LogError ("You must to assign a Hole Prefab to representante the virtual holes on the board, please verify your Board prefab and assign a suitable prefab for the holePrefab property");
				return;
			}
		
			ResetBoard ();
	
			Renderer boardMesh = boardGO.GetComponent<Renderer> ();
			Vector2 holeSize = new Vector2 (
				holePrefab.Size.x + paddingX,
				holePrefab.Size.y + paddingZ
			);

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holeSize.x);
			int zCount = Mathf.FloorToInt (boardMesh.bounds.size.z / holeSize.y);

			float cellWidth = holeSize.x - paddingX / 2.0f;
			float cellHeght = holeSize.y - paddingZ / 2.0f;

			float xOffset = holeSize.x / 2.0f;
			float yOffset = boardMesh.bounds.size.y + 0.01f;
			float zOffset = holeSize.y / 2.0f;

			for (int z = 0; z < zCount; z++) {
				for (int x = 0; x < xCount; x++) {
					GameObject hole = PrefabUtility.InstantiatePrefab (holePrefab.gameObject) as GameObject;
					hole.transform.localPosition = new Vector3 (((x - xCount / 2.0f) * cellWidth) + xOffset, yOffset, ((z - zCount / 2.0f) * cellHeght) + zOffset);
					hole.transform.parent = boardGO.transform;
					hole.name = "Hole (" + x + "," + z + ")";
				}
			}
		}

		public static void CreateBoardInstance() {
			GameObject board =Instantiate(Resources.Load ("BoardContainer",typeof(GameObject)), Vector3.zero,Quaternion.identity) as GameObject;
			board.name = "Board Template";
			board.GetComponent<Board> ().SetBoardGo ();
		} 

	}
}
