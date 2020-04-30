using UnityEngine;
using UnityEditor;

namespace WakARmole {
	[CustomEditor (typeof (Board))]
	public class BoardEditor :Editor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI ();

			Board board = (Board)target;
			if (GUILayout.Button ("Create Hole Prefab From Template") && board.IsHolePrefabEmpty()) {
				Hole.CreateHoleInstance ();
			}

			if (!board.IsHolePrefabEmpty ()) {
				if (GUILayout.Button ("Generate Holes")) {
					board.GenerateBoard ();
				}

				if (GUILayout.Button ("Reset board")) {
					board.ResetBoard ();
				}
			}

		}

		[MenuItem ("GameObject/WakARmole/Board", false, 10)]
		public static void CreateBoardInstance() {
			Board.CreateBoardInstance ();
		}


		[MenuItem ("GameObject/WakARmole/Hole", false, 10)]
		public static Hole CreateHoleInstance() {
			return Hole.CreateHoleInstance ();
		}
	}

}


