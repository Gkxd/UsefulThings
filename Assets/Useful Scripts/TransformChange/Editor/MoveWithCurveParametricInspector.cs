using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomEditor(typeof(MoveWithCurveParametric))]
    [CanEditMultipleObjects]
    public class MoveWithCurveParametricInspector : Editor {
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetTimeOnEnable"));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Movement Directions");
            EditorGUILayout.LabelField("X", GUILayout.Width(20));
            bool moveX = EditorGUILayout.Toggle(serializedObject.FindProperty("moveX").boolValue, GUILayout.Width(20));
            EditorGUILayout.LabelField("Y", GUILayout.Width(20));
            bool moveY = EditorGUILayout.Toggle(serializedObject.FindProperty("moveY").boolValue, GUILayout.Width(20));
            EditorGUILayout.LabelField("Z", GUILayout.Width(20));
            bool moveZ = EditorGUILayout.Toggle(serializedObject.FindProperty("moveZ").boolValue, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();

            serializedObject.FindProperty("moveX").boolValue = moveX;
            serializedObject.FindProperty("moveY").boolValue = moveY;
            serializedObject.FindProperty("moveZ").boolValue = moveZ;

            if (moveX) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveX"));
            }
            if (moveY) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveY"));
            }
            if (moveZ) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveZ"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}