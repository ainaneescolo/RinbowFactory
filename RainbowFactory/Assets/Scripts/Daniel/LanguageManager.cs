using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{

    private void Start()
    {
        if (PlayerPrefs.HasKey("Idioma"))
        {

            StartCoroutine(("cambiarIdioma"));
        }

          
    }


    public void SetLanguage(string newLanguageCode)
    {
        Dictionary<string, Locale> languageDic = new Dictionary<string, Locale>
        {
            
            {"ca", LocalizationSettings.AvailableLocales.Locales[0]},
            {"nl-BE", LocalizationSettings.AvailableLocales.Locales[1]},
            {"en", LocalizationSettings.AvailableLocales.Locales[2]},
            {"fr", LocalizationSettings.AvailableLocales.Locales[3]},
            {"es", LocalizationSettings.AvailableLocales.Locales[4]},
           
            

        };

        if (languageDic.ContainsKey(newLanguageCode))
        {
            LocalizationSettings.SelectedLocale = languageDic[newLanguageCode];
            PlayerPrefs.SetString("Idioma", newLanguageCode);
        }

    }

    IEnumerator cambiarIdioma()
    {
        yield return new WaitForSeconds(0.5f);
        SetLanguage(PlayerPrefs.GetString("Idioma"));
    }


} 
