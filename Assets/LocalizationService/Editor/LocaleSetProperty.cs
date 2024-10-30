using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Locale.LocaleSet))]
public class LocaleSetProperty : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float distanceBetweenFields = 10;
        float halfDistance = distanceBetweenFields / 2;
        float initialWidth = position.width;
        float langWidthPercent = 0.25f;
        float langWidth = initialWidth * langWidthPercent - halfDistance;
        float textWidth = initialWidth * (1 - langWidthPercent) - halfDistance;
        EditorGUI.PropertyField(
            new Rect(position.x, position.y, langWidth, EditorGUIUtility.singleLineHeight),
            property.FindPropertyRelative("language"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(position.x + langWidth + distanceBetweenFields, position.y, textWidth, EditorGUIUtility.singleLineHeight),
            property.FindPropertyRelative("text"), GUIContent.none);
    }
}
