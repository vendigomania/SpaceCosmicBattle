using UnityEngine;

public class Localization
{
    private static bool _languageIsSet;
    private static SystemLanguage _lang;
    private static LocalizationSettings _settings;
    public static LocalizationSettings Settings
    {
        get
        {
            if (_settings != null)
            {
                return _settings;
            }
            LocalizationSettings settings = Resources.Load<LocalizationSettings>("LocalizationSettings");
            if (settings == null)
            {
                Debug.LogError("Can't find localization settings object");
                return null;
            }
            _settings = settings;
            return settings;
        }
    }

    public static void SetLang()
    {
        LocalizationSettings settings = Settings;
        if (settings == null)
        {
            Set(SystemLanguage.English);
            return;
        }

        if (settings.IsTesting)
        {
            Set(settings.TestingLanguage);
            return;
        }

        SystemLanguage userLanguage = Application.systemLanguage;
        if (settings.Default == userLanguage)
        {
            Set(userLanguage);
            return;
        }

        SystemLanguage[] supportedLanguages = settings.SupportedLanguages;
        foreach (SystemLanguage language in supportedLanguages)
        {
            if (userLanguage == language)
            {
                Set(userLanguage);
                return;
            }
        }

        Set(settings.Default);

        void Set(SystemLanguage language)
        {
            _lang = language;
            _languageIsSet = true;
            return;
        }
    }

    public static SystemLanguage GetLang()
    {
        if (!_languageIsSet)
        {
            SetLang();
        }
        return _lang;
    }
}
