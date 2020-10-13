using UnityEditor;
using UnityEngine;

namespace WhackARmole {
	public class WhackARMoleEditor :EditorWindow {

		public GameObject BoardGo;
		public GameObject HolePrefab;
		public GameObject[] Moles;

		[MenuItem ("Window/Whack-AR-Mole")]
		public static void ShowWindow() {
			GetWindow<WhackARMoleEditor> ("Whack-AR-Mole");
		}

		void OnGUI() {
			ScriptableObject scriptableObj = this;
			SerializedObject serialObj = new SerializedObject (scriptableObj);
			SerializedProperty serialProp = serialObj.FindProperty ("Moles");

			GUILayout.Label ("Whack-AR-Mole Creator", EditorStyles.largeLabel);


			GUILayout.Label ("Prefabs Creators", EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create Board Prefab")) {
				GameCreator.CreateBoardInstance ();
			}

			if (GUILayout.Button ("Create Hole Prefab")) {
				GameCreator.CreateHoleInstance ();
			}

			if (GUILayout.Button ("Create Moles Prefabs")) {
				GameCreator.CreateMoleInstance ();
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

			GUILayout.Label ("Assing Prefabs", EditorStyles.boldLabel);
			BoardGo = (GameObject)EditorGUILayout.ObjectField ("Board Game Object",BoardGo, typeof (GameObject), true);
			HolePrefab = (GameObject)EditorGUILayout.ObjectField ("Hole Prefab",HolePrefab, typeof (GameObject), false);

			EditorGUILayout.PropertyField (serialProp, true);
			serialObj.ApplyModifiedProperties ();
			EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);

			GUILayout.Label ("Create & Populate Main Prefab", EditorStyles.boldLabel);

			if (GUILayout.Button ("Populate Board")) {
				GameCreator.GenerateBoard (BoardGo, HolePrefab, Moles);
			}

			if (GUILayout.Button ("Reset Board")) {
				GameCreator.ResetBoard (BoardGo);
			}
			EditorGUILayout.LabelField ("", GUI.skin.horizontalSlider);
		}
	}
}
