using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        [SerializeField] private TMP_Text killed;
        [SerializeField] private TMP_Text coinsEarned;
        [SerializeField] private TMP_Text abilitiesUsed;

        [SerializeField] private Button playAgainBtn;
        [SerializeField] private Button menuBtn;

        private void Start()
        {
            playAgainBtn.onClick.AddListener(GamePresenter.Instance.StartGame);
            menuBtn.onClick.AddListener(BackToMenu);
        }

        public void Show(int _killed, int _coinsEarned, int _abilitiesUsed)
        {
            gameObject.SetActive(true);

            if(_killed > IngameData.Best)
            {
                IngameData.Best = _killed;
                title.text = "New record";
            }
            else
            {
                title.text = "Game end";
            }

            killed.text = _killed.ToString();
            coinsEarned.text = _coinsEarned.ToString();
            abilitiesUsed.text = _abilitiesUsed.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void BackToMenu()
        {
            MainController.Instance.BackToStartMenu();
            Hide();
        }
    }
}
