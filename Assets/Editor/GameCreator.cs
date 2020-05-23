using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WhackARmole {
	[ExecuteInEditMode]
	public class GameCreator :MonoBehaviour {

		public static void ResetBoard(GameObject board) {
			while (board.transform.childCount != 0) {
				DestroyImmediate (board.transform.GetChild (0).gameObject);
			}
		}

		public static void GenerateBoard(GameObject board, GameObject hole, GameObject[] moles) {
			if (hole == null) {
				Debug.LogError ("You must to assign a Hole Prefab to representante the virtual holes on the board, please assign a suitable prefab for the Hole Prefab property");
				return;
			}

			if (moles.Length < 1) {
				Debug.LogError ("You must to assign, at least, one Mole Prefab to representante the virtual Moles in the hole, please assign some suitables prefabs for the Moles Prefab property");
				return;
			}

			GameObject boardGO = board.transform.GetChild (0).gameObject;
			Board boardInstance = board.GetComponent<Board> ();
			Vector2 holeSize = hole.GetComponent<Hole> ().Size;
			ResetBoard (boardGO);

			Renderer boardMesh = boardGO.GetComponent<Renderer> ();

			int xCount = Mathf.RoundToInt (boardMesh.bounds.size.x / holeSize.x);
			int zCount = Mathf.RoundToInt (boardMesh.bounds.size.z / holeSize.y);

			float xOffset = holeSize.x / 2.0f;
			float zOffset = holeSize.y / 2.0f;

			for (int z = 0; z < zCount; z++) {
				for (int x = 0; x < xCount; x++) {
					GameObject holeInstance = (GameObject)PrefabUtility.InstantiatePrefab (hole as GameObject);
					holeInstance.transform.localPosition = new Vector3 (((x - xCount / 2.0f) * holeSize.x) + xOffset, 0, ((z - zCount / 2.0f) * holeSize.y) + zOffset);
					holeInstance.transform.parent = boardGO.transform;
					holeInstance.transform.localPosition = new Vector3 (holeInstance.transform.localPosition.x, 0.5001f, holeInstance.transform.localPosition.z);
					holeInstance.name = "Hole (" + x + "," + z + ")";

					Hole.PopulateHole (holeInstance, moles);
				}
			}
		}

		public static void CreateBoardInstance() {
			GameObject board = Instantiate (Resources.Load ("BoardContainer", typeof (GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
			board.name = "Board Template";
		}

		public static void CreateHoleInstance() {
			GameObject hole = Instantiate (Resources.Load ("HoleContainer", typeof (GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
			hole.name = "Hole Template";
		}

		public static void CreateMoleInstance() {
			GameObject mole = Instantiate (Resources.Load ("MoleContainer", typeof (GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
			mole.name = "Mole Template";
		}

	}
}
