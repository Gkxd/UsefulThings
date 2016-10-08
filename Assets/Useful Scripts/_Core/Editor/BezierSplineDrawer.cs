using UnityEditor;
using UnityEngine;
using System.Collections;

namespace UsefulThings
{
    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineDrawer : Editor
    {
        private int selectedIndex = -1;

        private void OnSceneGUI()
        {
            BezierSpline curve = (BezierSpline)target;

            Transform t = curve.transform;
            Quaternion r = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;

            for (int i = 0; i < curve.points.Count; i++)
            {
                Vector3 p = t.TransformPoint(curve.points[i]);

                Handles.color = Color.white;

                float size = HandleUtility.GetHandleSize(p);
                if (Handles.Button(p, r, size * 0.04f, size * 0.06f, Handles.DotCap))
                {
                    selectedIndex = i;
                    Repaint();
                }

                if (selectedIndex == i)
                {
                    EditorGUI.BeginChangeCheck();
                    p = Handles.DoPositionHandle(p, r);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(curve, "Move Point");
                        curve.SetControlPoint(i, t.InverseTransformPoint(p));
                        EditorUtility.SetDirty(curve);
                    }
                }
            }

            Handles.color = curve.tangentColor;
            for (int i = 0; i < curve.points.Count - 1; i++)
            {
                if (i % 3 == 1) continue;

                Vector3 p0 = t.TransformPoint(curve.points[i]);
                Vector3 p1 = t.TransformPoint(curve.points[i + 1]);
                Handles.DrawLine(p0, p1);
            }

            Handles.color = curve.curveColor;
            for (int i = 0; i < curve.points.Count / 3; i++)
            {
                Vector3 p0 = t.TransformPoint(curve.points[3 * i]);
                Vector3 p1 = t.TransformPoint(curve.points[3 * i + 1]);
                Vector3 p2 = t.TransformPoint(curve.points[3 * i + 2]);
                Vector3 p3 = t.TransformPoint(curve.points[3 * i + 3]);

                Handles.DrawBezier(p0, p3, p1, p2, curve.curveColor, null, 2f);
            }

            Handles.color = curve.uniformSamplingColor;
            if (Camera.current)
            {
                Vector3 up = Camera.current.transform.up;
                Vector3 right = Camera.current.transform.right;

                for (float i = 0; i < curve.points.Count / 3; i += 0.02f)
                {
                    Vector3 p0 = curve.EvaluateUniform(i);
                    float size = HandleUtility.GetHandleSize(p0) * 0.02f;

                    Vector3[] rect = new Vector3[] { p0 - right * size, p0 - up * size, p0 + right * size, p0 + up * size };

                    Handles.DrawSolidRectangleWithOutline(rect, curve.uniformSamplingColor, curve.uniformSamplingColor);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            BezierSpline curve = (BezierSpline)target;
            serializedObject.Update();
            /*
            EditorGUILayout.PropertyField(points, new GUIContent("Segments"));
            if (points.isExpanded)
            {
                EditorGUI.indentLevel += 1;
                for (int i = 0; i < points.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent("P" + i), GUILayout.Width(55));
                    EditorGUILayout.PropertyField(points.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(250));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel -= 1;
            }
            */

            EditorGUILayout.LabelField(new GUIContent("Selected Point"), EditorStyles.boldLabel);
            SerializedProperty points = serializedObject.FindProperty("points");
            SerializedProperty continuity = serializedObject.FindProperty("continuity");

            if (selectedIndex > -1 && selectedIndex < curve.points.Count)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("P" + selectedIndex), GUILayout.Width(35));
                EditorGUILayout.PropertyField(points.GetArrayElementAtIndex(selectedIndex), GUIContent.none);
                EditorGUILayout.EndHorizontal();

                int cornerIndex = selectedIndex;
                if (selectedIndex % 3 == 1)
                {
                    cornerIndex = selectedIndex - 1;
                }
                else if (selectedIndex % 3 == 2)
                {
                    cornerIndex = selectedIndex + 1;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Continuity Type:"), GUILayout.Width(100));
                EditorGUILayout.PropertyField(continuity.GetArrayElementAtIndex(cornerIndex), GUIContent.none);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField(new GUIContent("No Point Selected"));
            }

            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Number of Segments: "), EditorStyles.boldLabel, GUILayout.Width(150));
            EditorGUILayout.LabelField(new GUIContent("" + curve.points.Count / 3), GUILayout.Width(25));
            if (GUILayout.Button("+", EditorStyles.miniButtonLeft))
            {
                Undo.RecordObject(curve, "Add Segment");
                curve.AddSegment();
                EditorUtility.SetDirty(curve);
            }
            if (GUILayout.Button("-", EditorStyles.miniButtonRight))
            {
                if (curve.points.Count >= 7)
                {
                    Undo.RecordObject(curve, "Remove Segment");
                    curve.RemoveSegment();
                    EditorUtility.SetDirty(curve);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Recompute Curve Approximation"))
            {
                Undo.RecordObject(curve, "Recompute Curve Approximation");
                curve.RecomputeApproximateCurve();
                EditorUtility.SetDirty(curve);
            }


            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(new GUIContent("Display Options"), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("curveColor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tangentColor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("uniformSamplingColor"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}