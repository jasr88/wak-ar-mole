using System.Collections.Generic;
using UnityEngine;

namespace WakARmole {
	public class Board :MonoBehaviour {
		[SerializeField]
		private GameObject boardGO;
		[SerializeField]
		private Hole holePrefab;
		[SerializeField]
		[Range (3, 12)] private int holesCount = 6;
		[SerializeField]
		private bool autoArrangeHoles = true;
		[SerializeField]
		private bool isHoleSquared = true;

		private List<Hole> holes = new List<Hole> ();

		// REMOVE THISSSS ONLY FOR TEST
		private void Update() {

			Renderer boardMesh = boardGO.GetComponent<Renderer> ();

			Debug.Log ("Board: " + boardMesh.bounds.size);
			Debug.Log ("Hole: " + holePrefab.GetComponentInChildren<Renderer> ().bounds.size);

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holePrefab.GetComponentInChildren<Renderer> ().bounds.size.x);
			int zCount = isHoleSquared ? xCount : Mathf.FloorToInt (boardMesh.bounds.size.z / holePrefab.GetComponentInChildren<Renderer> ().bounds.size.z);

			Debug.LogFormat ("Max number of holes x: {0}\nMax number of holes y: {1}", xCount, zCount);
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

#if UNITY_EDITOR
			Debug.LogFormat (
				"Board Configuration" +
				"     Number of Holes: {0}\n" +
				"     AutoArrangeHoles: {1}\n" +
				"     IsHolesSquared: {2}\n" +
				"     boardGO Name: {3}",
				holesCount,
				autoArrangeHoles,
				isHoleSquared,
				boardGO.name
			);
#endif

			Mesh boardMesh = boardGO.GetComponent<Mesh> ();

			int xCount = Mathf.FloorToInt (boardMesh.bounds.size.x / holePrefab.Size.x);
			int yCount = isHoleSquared ? xCount : Mathf.FloorToInt (boardMesh.bounds.size.y / holePrefab.Size.y);

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
