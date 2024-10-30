using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsScreen : MonoBehaviour
    {
        [SerializeField] private Toggle soundTgl;
        [SerializeField] private Toggle musicTgl;
        [SerializeField] private Button backBtn;


        // Start is called before the first frame update
        void Start()
        {
            soundTgl.onValueChanged.AddListener(SoundSwitch);
            musicTgl.onValueChanged.AddListener(MusicSwitch);

            backBtn.onClick.AddListener(Hide);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            AudioManager.Click();
        }

        public void Hide()
        {
            AudioManager.Click();
            gameObject.SetActive(false);
            MainController.Instance.BackToStartMenu();
        }

        private void SoundSwitch(bool _isOn)
        {
            AudioManager.SoundActive = _isOn;

            AudioManager.Click();
        }

        private void MusicSwitch(bool _isOn)
        {
            AudioManager.MusicActive = _isOn;

            AudioManager.Click();
        }
    }
}
