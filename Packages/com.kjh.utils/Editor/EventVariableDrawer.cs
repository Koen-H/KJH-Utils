using UnityEditor;
using UnityEngine;

namespace KJH.Utils.Editor
{
    [CustomPropertyDrawer(typeof(EventVariable<>))]
    public class EventVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");

            EditorGUI.PropertyField(position, valueProperty, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty, label, true);
        }
    }
}
