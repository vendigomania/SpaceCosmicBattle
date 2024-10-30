using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Options
{
    public class OptionsToggleText : MonoBehaviour
    {
        [SerializeField] private string fieldName;
        private TMP_Text toggleLable;

        private void Start()
        {
            toggleLable = GetComponent<TMP_Text>();
            fieldName = toggleLable.text;
        }

        public void SetOn(bool _isOn)
        {
            if (_isOn)
            {
                toggleLable.text = $"{fieldName} on";
            }
            else
            {
                toggleLable.text = $"{fieldName} off";
            }
        }
    }
}
