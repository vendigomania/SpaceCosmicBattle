using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationSettings", menuName = "LocalizationSettings")]
public class LocalizationSettings : ScriptableObject
{
    [SerializeField] private SystemLanguage _defaultLanguage = SystemLanguage.English;
    [SerializeField] private SystemLanguage[] _supportedLanguages;
    [SerializeField] private bool _isTesting;
    [SerializeField] private SystemLanguage _testingLanguage;

    public SystemLanguage Default => _defaultLanguage;
    public SystemLanguage[] SupportedLanguages => _supportedLanguages.ToArray();
    public bool IsTesting => _isTesting;
    public SystemLanguage TestingLanguage => _testingLanguage;

    private void OnValidate()
    {
        if (_supportedLanguages.Length > 0)
        {
            SystemLanguage newItem = _supportedLanguages[^1];
            for (int i = 0; i < _supportedLanguages.Length - 1; i++)
            {
                if (newItem == _supportedLanguages[i] && newItem != SystemLanguage.Unknown)
                {
                    _supportedLanguages[^1] = SystemLanguage.Unknown;
                    break;
                }
            }
            if (!_supportedLanguages.Contains(_testingLanguage))
            {
                _testingLanguage = _defaultLanguage;
            }
        }
    }
}
