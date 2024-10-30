using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public abstract class BaseGoodVisualizator : MonoBehaviour
    {
        [SerializeField] private string prefsFieldKey;

        [SerializeField] protected Button buyBtn;
        [SerializeField] protected TMP_Text upgradeCostLable;

        protected int Cost => 150 + UpgradeValue * 50;

        protected int UpgradeValue
        {
            get => PlayerPrefs.GetInt(prefsFieldKey, 0);
            set => PlayerPrefs.SetInt(prefsFieldKey, value);
        }

        protected virtual void Start()
        {
            IngameData.OnChangeCoins += UpdateStats;

            buyBtn.onClick.AddListener(Buy);
        }

        private void OnEnable()
        {
            UpdateStats();
        }

        public virtual void UpdateStats()
        {
            upgradeCostLable.text = Cost.ToString();
        }

        private void Buy()
        {
            UpgradeValue++;
            IngameData.Coins -= Cost;

            AudioManager.Click();
        }
    }
}
