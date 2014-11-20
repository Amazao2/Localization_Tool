using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Localization;

public class LocalizedText : MonoBehaviour {

    public LocalizationInit languageController;

    public Text text;

    public string variableName;

    private Lang lastLanguage;

	// Use this for initialization
	void Start () {

        lastLanguage = languageController.currentLanguage;
        text.text = fetchContent();
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
