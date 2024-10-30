using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngameData : MonoBehaviour
{
    public static UnityAction OnChangeCoins;
    public static int Coins
    {
        get => PlayerPrefs.GetInt("Coins", 0);
        set {
            PlayerPrefs.SetInt("Coins", value);
            OnChangeCoins?.Invoke();
        }
    }

    public static int Best
    {
        get => PlayerPrefs.GetInt("Best", 0);
        set => PlayerPrefs.SetInt("Best", value);
    }
}
