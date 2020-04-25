using UnityEngine;

public class Singleton<T> :MonoBehaviour where T : MonoBehaviour {

	private static T _instance;

	private static object _lock = new object ();

	private static bool applicationIsQuitting = false;

	protected static bool enableForEditor = false;

	public static T Instance {
		get {
			if (applicationIsQuitting) {
#if UNITY_EDITOR
				Debug.LogWarning ("[Singleton] La instancia '" + typeof (T) + "' ya ha sido destruida al salir de la aplicación. No se creará de nuevo - se regresa null.");
#endif
				return null;
			}

			//Thread-Safe
			lock (_lock) {
				if (_instance == null) {

					_instance = (T)FindObjectOfType (typeof (T));

					if (FindObjectsOfType (typeof (T)).Length > 1) {
						Debug.LogError ("[Singleton] ¡Algo salió muy mal! - No debe de existir mas de una instancia del singleton de tipo: '" + typeof (T) + "' Reabrir la escena puede arreglar esto.");
						return _instance;
					}

					if (_instance == null) {
						GameObject singleton = new GameObject ();
						_instance = singleton.AddComponent<T> ();
						singleton.name = "(singleton) " + typeof (T).ToString ();
						DontDestroyOnLoad (singleton);
					}
				}

				return _instance;
			}
		}
	}

	private void OnApplicationQuit() {
		applicationIsQuitting = true;
	}

	private void OnDestroy() {
		applicationIsQuitting = true;
	}
}
