using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomEditor(typeof(ScaleWithCurve))]
    [CanEditMultipleObjects]
    public class ScaleWithCurveInspector : Editor {
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("uniformScale"));

            if (serializedObject.FindProperty("uniformScale").boolValue) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curve"));
            }
            else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveX"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveY"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveZ"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}