using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _zonebest;
    [SerializeField] private Transform OpenSound, NoSound;
    [SerializeField] private Transform vibration, noVibration;
    [SerializeField] private Transform woodRank, silverRank, goldRank;
    [SerializeField] private Slider playerEXPSlider;
    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.playerEXP > 100) UpdatePlayerRank(25);
    }

    public override void OnInit()
    {
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.GamePlay);
        GameManager.Instance.gameState = GameManager.GameState.gameStarted;
    }

    public void OpenWeaponShop()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.WeaponShop);
    }

    public void OpenSkinShop()
    {
        UIManager.Instance.OpenUI(UIName.SkinShop);
    }

    public void OpenCoinShop()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.CoinShop);
    }

    public void ChangeOpenSoundState()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.SoundState = !UIManager.Instance.SoundState;
        if (UIManager.Instance.SoundState)
        {
            OpenSound.gameObject.SetActive(true);
            NoSound.gameObject.SetActive(false);
            GameManager.Instance.OpenSound = true;
        }
        else
        {
            OpenSound.gameObject.SetActive(false);
            NoSound.gameObject.SetActive(true);
            GameManager.Instance.OpenSound = false;
        }
    }
    public void ChangeVibrationState()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.vibrationState = !UIManager.Instance.vibrationState;
        if (UIManager.Instance.vibrationState)
        {
            vibration.gameObject.SetActive(true);
            noVibration.gameObject.SetActive(false);
        }
        else
        {
            vibration.gameObject.SetActive(false);
            noVibration.gameObject.SetActive(true);
        }
    }

    public void UpdatePlayerRank(int EXP)
    {
        woodRank.gameObject.SetActive(true);
        silverRank.gameObject.SetActive(false);
        goldRank.gameObject.SetActive(false);
        UIManager.Instance.playerEXP += EXP;
        if (UIManager.Instance.playerEXP > 100)
        {
            UIManager.Instance.playerEXP -= 100;
            if (UIManager.Instance.playerRank == PlayerRank.Wood)
            {
                UIManager.Instance.playerRank = PlayerRank.Silver;
                woodRank.gameObject.SetActive(false);
                silverRank.gameObject.SetActive(true);
            }
            else if (UIManager.Instance.playerRank == PlayerRank.Silver)
            {
                UIManager.Instance.playerRank = PlayerRank.Gold;
                silverRank.gameObject.SetActive(false);
                goldRank.gameObject.SetActive(true);
            }
        }
        playerEXPSlider.value = UIManager.Instance.playerEXP;
    }

    public void UpdateBest(int zone, int best)
    {
        _zonebest.text = "ZONE: " + zone + " - BEST:#" + best;
    }
}
