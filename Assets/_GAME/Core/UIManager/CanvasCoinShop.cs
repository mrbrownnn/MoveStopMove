using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasCoinShop : UICanvas
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    public override void OnInit()
    {
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
    }
    public void XButton()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.MainMenu);
    }

    public void BuyCoin()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.coinAmount += 1000;
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
    }

    public void NoAds()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.coinAmount = 0;
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
    }

    public void ResetItem()    //Reset trạng thái mua đồ về chưa mua
    {
        GameManager.Instance.PlayClickSound();
        for (int i = 0; i < 25; i++)
        {
            PlayerPrefs.SetInt("ClothesShop" + (ClothType)i, 1);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("WeaponShop" + (weaponType)i, 1);
            PlayerPrefs.Save();
        }
    }
}
