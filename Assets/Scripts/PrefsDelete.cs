using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrefsDelete : MonoBehaviour {

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PrefsDelete))]
    public class SnapToGroundEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PrefsDelete script = target as PrefsDelete;
            if (GUILayout.Button("CLEAR PLAYER PREFS"))
                PlayerPrefs.DeleteAll();
        }
    }
#endif
	
}
