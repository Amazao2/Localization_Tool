using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;
using Assets.Scripts.Localization;

[CustomEditor(typeof (LocalizedText) )]
public class LocalizedTextEditor : Editor {

    private string defaultText;

	public override void OnInspectorGUI()
    {
        LocalizedText myTarget = (LocalizedText)target;
        Lang defaultLanguage;

        DrawDefaultInspector();

        EditorGUILayout.LabelField("Variable Name", myTarget.variableName);
        EditorGUILayout.HelpBox(
            "The Variable Name is referenced in the text object you've attached to this script. " +
            "Ensure you use a variable name that can be found in the messages files or add it below.", 
            MessageType.Info
            );

        if( myTarget.text )
        {
            defaultLanguage = myTarget.languageController.defaultLanguage;

            // provides the ability to add a variable to the default messages file automatically
            EditorGUILayout.PrefixLabel("Add a new Variable:");
            EditorGUI.indentLevel++;

            defaultText = EditorGUILayout.TextField("Default Lang Text", defaultText);
            EditorGUILayout.HelpBox(
                "The variable with name: " + myTarget.variableName +
                " and the provided text will be added to the default language: " +
                defaultLanguage.language,
                MessageType.Info
                );
            if (GUILayout.Button("Add"))
            {
                addVariableToDefaultLanguageFile(
                    myTarget.languageController,
                    defaultLanguage,
                    myTarget.variableName,
                    defaultText
                    );
            }
        }      
        
    }

    private void addVariableToDefaultLanguageFile(LocalizationController controller, Lang defaultLanguage, string variableName, string variableValue)
    {
        var path = controller.absoluteMessagesPath + "/messages." + defaultLanguage.code;
        var line = Environment.NewLine + variableName + "=" + variableValue;

        File.AppendAllText(@path, line);
    }
}
