using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomEditor(typeof(ScaleWithCurve))]
    [CanEditMultipleObjects]
    public class ScaleWithCurveInspector : Editor {
        public override void OnInspectorGUI() {
            ScaleWithCurve scaleWithCurve = (ScaleWithCurve)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetTimeOnEnable"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("uniformScale"));

            if (scaleWithCurve.uniformScale) {
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