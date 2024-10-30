using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    [SerializeField] private Button skillBtn;

    [SerializeField] private Image cooldownImg;
    [SerializeField] private float defaultCooldown;

    public UnityAction OnClickAction;

    float remainCooldown; //0-1

    private void Start()
    {
        skillBtn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        AudioManager.Click();

        skillBtn.enabled = false;

        remainCooldown = 1f;

        OnClickAction?.Invoke();
    }

    private void Update()
    {
        remainCooldown = Mathf.Max(0f, remainCooldown - Time.deltaTime / defaultCooldown);

        cooldownImg.fillAmount = remainCooldown;

        if(remainCooldown == 0f)
        {
            skillBtn.enabled = true;
        }
    }
}
