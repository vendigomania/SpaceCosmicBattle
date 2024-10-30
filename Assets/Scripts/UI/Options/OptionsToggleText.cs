using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Options
{
    public class OptionsToggleText : MonoBehaviour
    {
        [SerializeField] private Locale localeOn;
        [SerializeField] private Locale localeOff;
        private TMP_Text toggleLable;

        private void Start()
        {
            toggleLable = GetComponent<TMP_Text>();
        }

        public void SetOn(bool _isOn)
        {
            if (_isOn)
            {
                toggleLable.text = $"{localeOn.GetText()}";
            }
            else
            {
                toggleLable.text = $"{localeOff.GetText()}";
            }
        }
    }
}
