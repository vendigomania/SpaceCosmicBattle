using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopScreen : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text coinsLable;

        private void Start()
        {
            backBtn.onClick.AddListener(Hide);
        }

        public void Show()
        {
            animator.Play("Show");
            coinsLable.text = IngameData.Coins.ToString();
        }

        public void Hide()
        {
            animator.Play("Hide");
        }
    }
}
