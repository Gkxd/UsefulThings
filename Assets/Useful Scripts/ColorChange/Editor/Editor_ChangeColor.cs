using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UsefulThings {
    [CustomEditor(typeof(_ChangeColor))]
    public class Editor_ChangeColor : Editor {
        public override void OnInspectorGUI() {
            _ChangeColor changeColor = (_ChangeColor)target;
        }
    }
}