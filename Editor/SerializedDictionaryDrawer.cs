using UnityEditor;
using UnityEngine;

namespace KJH.Utils.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        const float REMOVE_BUTTON_WIDTH = 20;
        const float HORIZIONTAL_PADDING = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Adjust the initial position to account for the label
            position.height = EditorGUIUtility.singleLineHeight;
            Rect foldoutRect = new Rect(position.x, position.y, position.width, position.height);

            // Draw the foldout to show or hide the dictionary contents
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            if (!property.isExpanded) return;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel++;

            SerializedProperty keysProperty = property.FindPropertyRelative("keys");
            SerializedProperty valuesProperty = property.FindPropertyRelative("values");

            // Check if keysProperty and valuesProperty are not null
            if (keysProperty != null && valuesProperty != null)
            {
                // Draw each key-value pair
                for (int i = 0; i < keysProperty.arraySize; i++)
                {
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                    Rect removeButtonRect = new Rect(position.x + position.width - REMOVE_BUTTON_WIDTH, position.y, REMOVE_BUTTON_WIDTH, EditorGUIUtility.singleLineHeight);
                    if (GUI.Button(removeButtonRect, "-"))
                    {
                        // Remove the key-value pair at the current index
                        keysProperty.DeleteArrayElementAtIndex(i);
                        valuesProperty.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    // Calculate rects for key and value fields
                    float halfWidth = position.width / 2;
                    SerializedProperty keyProperty = keysProperty.GetArrayElementAtIndex(i);
                    float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty, true);
                    Rect keyRect = new Rect(position.x, position.y, halfWidth - HORIZIONTAL_PADDING, EditorGUIUtility.singleLineHeight);

                    // Get the height of the value property to correctly allocate space
                    SerializedProperty valueProperty = valuesProperty.GetArrayElementAtIndex(i);
                    float valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty, true);
                    Rect valueRect = new Rect(position.x + halfWidth + HORIZIONTAL_PADDING - REMOVE_BUTTON_WIDTH, position.y, halfWidth - HORIZIONTAL_PADDING, valuePropertyHeight);

                    EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none, true);
                    EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none, true);

                    float pairHeight = valuePropertyHeight > keyPropertyHeight ? valuePropertyHeight : keyPropertyHeight;

                    position.y += pairHeight - EditorGUIUtility.singleLineHeight;
                }
            }

            // Add a button to add new entries
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect addButtonRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            if (GUI.Button(addButtonRect, "Add Entry"))
            {
                // Add a new key-value pair with default values
                keysProperty.arraySize++;
                valuesProperty.arraySize++;

                // Ensure new elements are initialized to default values
                keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1);
                valuesProperty.GetArrayElementAtIndex(valuesProperty.arraySize - 1);

                // Apply the changes
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
            {
                SerializedProperty keysProperty = property.FindPropertyRelative("keys");
                SerializedProperty valuesProperty = property.FindPropertyRelative("values");

                // Check if keysProperty and valuesProperty are not null
                if (keysProperty != null && valuesProperty != null)
                {
                    // Iterate over all keys and values to calculate total height
                    for (int i = 0; i < keysProperty.arraySize; i++)
                    {
                        SerializedProperty keyProperty = keysProperty.GetArrayElementAtIndex(i);
                        SerializedProperty valueProperty = valuesProperty.GetArrayElementAtIndex(i);

                        float valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty, true);
                        float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty, true);
                        float pairHeight = valuePropertyHeight > keyPropertyHeight ? valuePropertyHeight : keyPropertyHeight;
                        height += pairHeight;

                        // Add standard spacing between each entry
                        height += EditorGUIUtility.standardVerticalSpacing;
                    }
                }
                // Add the height for the "Add Entry" button
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
            return height;
        }

    }
}
