using UnityEngine;
using UnityEditor;
using WakARmole;

[CustomEditor(typeof(Board))]
public class BoardEditor:Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		Board board = (Board)target;
		if (GUILayout.Button("Generate Holes")) {
			board.GenerateBoard ();
		}

		if (GUILayout.Button ("Reset board")) {
			board.ResetBoard ();
		}
	}
}
