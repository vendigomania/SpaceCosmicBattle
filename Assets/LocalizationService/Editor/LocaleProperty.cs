using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Locale))]
public class LocaleProperty : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(position, property, label, true);
        property.FindPropertyRelative("_localeSets").isExpanded = true;
        if (property.isExpanded)
        {
            if (GUILayout.Button("Fill"))
            {
                FillLocale(property);
            }
        }
    }

    private async void FillLocale(SerializedProperty property)
    {
        SerializedProperty localeProperty = property.FindPropertyRelative("_localeSets");
        SerializedProperty defaultProperty = property.FindPropertyRelative("_default");
        SerializedObject serializedObject = localeProperty.serializedObject;
        SystemLanguage[] supportedLanguages = Localization.Settings.SupportedLanguages;
        localeProperty.ClearArray();
        serializedObject.ApplyModifiedProperties();
        for (int i = 0; i < supportedLanguages.Length; i++)
        {
            localeProperty.InsertArrayElementAtIndex(i);
            serializedObject.ApplyModifiedProperties();
            string translation = await TranslationWrapper.TranslateOnline(
                supportedLanguages[i],
                defaultProperty.stringValue,
                Localization.Settings.Default);

            SerializedProperty arrayElement = localeProperty.GetArrayElementAtIndex(i);
            arrayElement.FindPropertyRelative("language").enumValueFlag = (int)(supportedLanguages[i]);
            arrayElement.FindPropertyRelative("text").stringValue = translation;
            serializedObject.ApplyModifiedProperties();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
}
