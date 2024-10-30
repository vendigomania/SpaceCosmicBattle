using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ProgressGoodVisualizator : BaseGoodVisualizator
    {
        [SerializeField] private GameObject maxUpgradedLable;

        [SerializeField] private Toggle[] cells;

        protected override void Start()
        {
            base.Start();
        }

        public override void UpdateStats()
        {
            base.UpdateStats();

            int maxCells = cells.Length;
            for(var i = 0; i < maxCells; i++)
            {
                cells[i].isOn = UpgradeValue > i;
            }

            maxUpgradedLable.SetActive(UpgradeValue == maxCells);

            buyBtn.gameObject.SetActive(UpgradeValue < maxCells && IngameData.Coins >= Cost);
            upgradeCostLable.gameObject.SetActive(UpgradeValue < maxCells);
        }
    }
}
