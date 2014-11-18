using UnityEngine;
using System.Collections;
using Assets.Scripts.Localization;

public class LocalizationInit : MonoBehaviour {

    // where are the messages files being stored relative to the assets folder?
    public string messagesFolderPath;
    
    // absolute path to the messages folder
    private string absoluteMessagesPath
    {
        get { return Application.dataPath + messagesFolderPath; }
    }

    public Lang defaultLanguage = new Lang("English", "en");
    private Lang currentLanguage;

    //private Messages messages = new Messages(messagesFolderPath);

	// Use this for initialization
	void Start () {

        // set language to default on initialization
        currentLanguage = defaultLanguage;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void changeLanguage(Lang newLang)
    {

    }

}