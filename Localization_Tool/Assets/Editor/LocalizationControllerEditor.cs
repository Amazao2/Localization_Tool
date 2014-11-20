using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;
using Assets.Scripts.Localization;
using System.Linq;

[CustomEditor(typeof (LocalizationController) )]
public class LocalizationControllerEditor : Editor
{
    private string languageCode;

	public override void OnInspectorGUI()
    {
        LocalizationController myTarget = (LocalizationController)target;
        

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("Press play to view supported languages." + 
            "While in play mode you can add new languages below.", 
            MessageType.Info
            );

        // allow adding a new language file
        EditorGUILayout.PrefixLabel("Add a new Language:");
        EditorGUI.indentLevel++;

        languageCode = EditorGUILayout.TextField("Language Code", languageCode);
        if (GUILayout.Button("Add"))
        {
            addNewLanguageMessageFile(myTarget, new Lang(languageCode));
        }

    }

    private void addNewLanguageMessageFile(LocalizationController controller, Lang newLang)
    {
        // only create the file if one doesn't already exist
        if( !controller.getSupportedLanguages().Exists( l => { return l == newLang; } ) )
        {
            File.Create(controller.absoluteMessagesPath + "/messages." + newLang.code).Dispose();
        }
    }

}
