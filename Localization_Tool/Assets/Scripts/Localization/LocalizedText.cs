using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Localization;

public class LocalizedText : MonoBehaviour {

    public LocalizationController languageController;

    public Text text;

    // the variable that can be found in the message file
    public string variableName
    {
        // allows read-only view of the variable name in the editor
        get { if (variable == null) return text.text; else { return variable; } }
    }
    private string variable;

    private Lang lastLanguage;

	// Use this for initialization
	void Start () {

        lastLanguage = languageController.currentLanguage;
        variable = text.text; // set the variable name to allow future reference
        text.text = fetchContent(); // set text to the correct value from the messages file
	}
	
	// Update is called once per frame
	void Update () {
	
        // if language has been changed insert new value
        if( languageController.currentLanguage != lastLanguage )
        {
            text.text = fetchContent();
            lastLanguage = languageController.currentLanguage;
        }
	}

    private string fetchContent()
    {
        return languageController.get(variableName);
    }
}
