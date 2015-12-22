using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomEditor(typeof(MoveWithCurveParametric))]
    public class MoveWithCurveParametricInspector : Editor {
        public override void OnInspectorGUI() {
            MoveWithCurveParametric moveWithCurveParametric = (MoveWithCurveParametric)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetTimeOnEnable"));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Movement Directions");
            EditorGUILayout.LabelField("X", GUILayout.Width(20));
            moveWithCurveParametric.moveX = EditorGUILayout.Toggle(moveWithCurveParametric.moveX, GUILayout.Width(20));
            EditorGUILayout.LabelField("Y", GUILayout.Width(20));
            moveWithCurveParametric.moveY = EditorGUILayout.Toggle(moveWithCurveParametric.moveY, GUILayout.Width(20));
            EditorGUILayout.LabelField("Z", GUILayout.Width(20));
            moveWithCurveParametric.moveZ = EditorGUILayout.Toggle(moveWithCurveParametric.moveZ, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();

            if (moveWithCurveParametric.moveX) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveX"));
            }
            if (moveWithCurveParametric.moveY) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveY"));
            }
            if (moveWithCurveParametric.moveZ) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("curveZ"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}