using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Observer_Main : MonoBehaviour
{
    public Button BtnToTimeProvider;
    public Button BtnToCurrencyChanged;

    public GameObject UIGroupMain;
    public GameObject UIGroupTimeProvider;
    public GameObject UIGroupCurrencyChanged;

    void Awake()
    {
        InitUI();
        ToMain();
    }

    private void InitUI()
    {
        BtnToTimeProvider?.onClick.RemoveAllListeners();
        BtnToTimeProvider?.onClick.AddListener(ToTimeProvider);

        BtnToCurrencyChanged?.onClick.RemoveAllListeners();
        BtnToCurrencyChanged?.onClick.AddListener(ToCurrencyChange);
    }

    private void ToTimeProvider()
    {
        UIGroupMain?.SetActive(false);
        UIGroupTimeProvider?.SetActive(true);
    }

    private void ToCurrencyChange()
    {
        UIGroupMain?.SetActive(false);
        UIGroupCurrencyChanged?.SetActive(true);
    }

    private void ToMain()
    {
        UIGroupMain?.SetActive(true);
        UIGroupTimeProvider?.SetActive(false);
        UIGroupCurrencyChanged?.SetActive(false);
    }
}
