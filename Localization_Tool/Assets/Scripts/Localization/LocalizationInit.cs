using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Localization;
using System.Linq;

public class LocalizationInit : MonoBehaviour {

    // where are the messages files being stored relative to the assets folder?
    public string messagesFolderPath;

    public Lang currentLanguage;
    public Lang defaultLanguage = new Lang("en");

    public List<Lang> supportedLanguages;

    // used to query messages from file
    private Messages messages;
    
    // absolute path to the messages folder
    private string absoluteMessagesPath
    {
        get 
        {            
            string absolutePath = Application.dataPath + messagesFolderPath;
            print("Application Data Path:" + Application.dataPath);
            print("Absolute Path To Messages: " + absolutePath);

            return absolutePath;
        }
    }

	// Use this for initialization
	void Start () {
        // set language to default on initialization
        currentLanguage = defaultLanguage;
        messages = new Messages(currentLanguage, absoluteMessagesPath);
        supportedLanguages = getSupportedLanguages();
	}

    /** Switch the context of the scene to a new language.
     * 
     */ 
    public void changeLanguage(Lang newLang)
    {
        if (supportedLanguages.Exists(l => { return l == newLang; }))
        {
            currentLanguage = newLang;
            messages = new Messages(currentLanguage, absoluteMessagesPath);
        }
        else
        {
            print("Language specified is not supported");
        }
    }

    /** Fetch a string from the messages file.
     */ 
    public string get(string variableName)
    {
        return messages.get(variableName);
    }

    public List<Lang> getSupportedLanguages()
    {
        var supported = messages.supportedLanguages();
        print("Supported Languages: " + string.Join(", ", supported.Select(l => { return l.code; }).ToArray()));

        return supported;
    }

}