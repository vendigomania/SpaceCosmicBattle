using UnityEngine;
using TMPro;

public class LocaleObject : MonoBehaviour
{
    [SerializeField] private Locale _locale;

    private void OnEnable()
    {
        TMP_Text textObj = GetComponent<TMP_Text>();
        string text = _locale.GetText();
        if (!string.IsNullOrEmpty(text)) textObj.text = text;
    }

    private void OnValidate()
    {
        if (_locale == null)
        {
            _locale = new Locale();
        }
        _locale.Default = GetComponent<TMP_Text>().text;
    }
}
