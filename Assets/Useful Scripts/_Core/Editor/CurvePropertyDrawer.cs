using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomPropertyDrawer(typeof(Curve))]
    public class CurvePropertyDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return property.isExpanded ? 34f : 16f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            label = EditorGUI.BeginProperty(position, label, property);

            position.height = 16;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("curve"));
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, new GUIContent(""), true);

            if (!property.isExpanded) {
                EditorGUI.EndProperty();
                return;
            }


            position = EditorGUI.IndentedRect(position);
            position.y += 18;
            position.width -= 20;
            position.width /= 3;
            int oldIndentLevel = EditorGUI.indentLevel;

            EditorGUI.indentLevel = 0;
            EditorGUIUtility.LookLikeControls(32f);

            EditorGUI.PropertyField(position, property.FindPropertyRelative("amplitude"), new GUIContent("AMP"));
            position.x += position.width + 10;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("frequency"), new GUIContent("FRQ"));
            position.x += position.width + 10;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("phase"), new GUIContent("PHS"));

            EditorGUI.EndProperty();

            EditorGUI.indentLevel = oldIndentLevel;
        }
    }
}