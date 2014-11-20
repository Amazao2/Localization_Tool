using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Localization;

public class SwitchLanguage : MonoBehaviour {

    public LocalizationController languageController;
    public Button button;
    public Lang language;

    private Text buttonText;

    void Start()
    {
        buttonText = button.GetComponentInChildren<Text>();
        buttonText.text = language.language;

        button.onClick.AddListener(() => { languageController.changeLanguage(language); });
    }
}
