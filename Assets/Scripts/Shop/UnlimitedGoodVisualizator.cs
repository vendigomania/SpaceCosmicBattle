using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class UnlimitedGoodVisualizator : BaseGoodVisualizator
    {
        [SerializeField] private TMP_Text profitLable;

        public override void UpdateStats()
        {
            base.UpdateStats();

            profitLable.text = $"{UpgradeValue * 5}% + 5%";
            buyBtn.gameObject.SetActive(IngameData.Coins >= Cost);
        }
    }
}
