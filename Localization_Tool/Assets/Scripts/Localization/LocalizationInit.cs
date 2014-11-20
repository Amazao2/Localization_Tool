using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Localization;

public class LocalizationInit : MonoBehaviour {

    // where are the messages files being stored relative to the assets folder?
    public string messagesFolderPath;

    public Lang currentLanguage;
    public Lang defaultLanguage = new Lang("en");

    public List<Lang> supportedLanguages;
    
    // absolute path to the messages folder
    private string absoluteMessagesPath
    {
        get { print(Application.dataPath); return Application.dataPath + messagesFolderPath; }
    }

    // used to query messages from file
    private Messages messages;

	// Use this for initialization
	void Start () {
        // set language to default on initialization
        currentLanguage = defaultLanguage;
        messages = new Messages(currentLanguage, absoluteMessagesPath);

        supportedLanguages = getSupportedLanguages();
	}

    public void changeLanguage(Lang newLang)
    {
        var supported = getSupportedLanguages();

        if( supported.Exists( l => { return l == newLang; }) )
        {
            currentLanguage = newLang;
        }
        else
        {
            print("Language specified is not supported");
        }
    }

    public string get(string variableName)
    {
        return messages.get(variableName);
    }

    List<Lang> getSupportedLanguages()
    {
        return messages.supportedLanguages();
    }

}