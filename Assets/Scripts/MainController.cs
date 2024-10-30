using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Options;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreLable;

    [SerializeField] private Button playBtn;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button optionsBtn;

    [SerializeField] private GamePresenter gamePresenter;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private OptionsScreen optionsScreen;

    public static MainController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BackToStartMenu();

        playBtn.onClick.AddListener(StartGame);
        shopBtn.onClick.AddListener(OpenShop);
        optionsBtn.onClick.AddListener(OpenSettings);
    }

    private void StartGame()
    {
        gamePresenter.StartGame();
        startScreen.SetActive(false);
    }

    public void BackToStartMenu()
    {
        startScreen.SetActive(true);
        bestScoreLable.text = $"Best: {IngameData.Best}";
        gamePresenter.SetOff();
    }

    private void OpenShop()
    {
        shopScreen.Show();
    }

    private void OpenSettings()
    {
        startScreen.SetActive(false);
        optionsScreen.Show();
    }
}
