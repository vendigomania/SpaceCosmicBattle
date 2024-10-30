using System;
using UnityEngine;

[Serializable]
public class Locale
{
    [SerializeField] private string _default;
    [SerializeField] private LocaleSet[] _localeSets;
    public string Default { get => _default; set => _default = value; }

    public Locale(string defaultText)
    {
        _default = defaultText;
    }

    public Locale()
    {

    }

    public string GetText(SystemLanguage language)
    {
        if (language == Localization.Settings.Default) return _default;
        foreach (LocaleSet localeSet in _localeSets)
        {
            if (localeSet.language == language)
            {
                return localeSet.text;
            }
        }
        return null;
    }

    public string GetText()
    {
        return GetText(Localization.GetLang());
    }

    public override string ToString()
    {
        return GetText();
    }

    [Serializable]
    public struct LocaleSet
    {
        public SystemLanguage language;
        public string text;
    }
}
