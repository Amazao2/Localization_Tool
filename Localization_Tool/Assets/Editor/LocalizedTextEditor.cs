using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (LocalizedText) )]
public class LocalizedTextEditor : Editor {

	public override void OnInspectorGUI()
    {
        LocalizedText myTarget = (LocalizedText)target;

        DrawDefaultInspector();

        EditorGUILayout.LabelField("Variable Name", myTarget.variableName);

        EditorGUILayout.HelpBox(
            "The Variable Name is referenced in the text object you've attached to this script. " +
            "Ensure you use a variable name that can be found in the messages files.", 
            MessageType.Info);
        
    }
}
