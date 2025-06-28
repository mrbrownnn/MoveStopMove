using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ShopType { HatShop, PantShop, ShieldShop, SetShop }
public enum ClothState { CantBuy, CanBuy, Select, Equipped }
public enum ClothLockState { Lock, UnLock }
public enum ClothType { Rau, Crown, Ear, Hat, HatCap, HatYellow, Arrow, Cowboy, Headphone,
                        Chambi, Comy, Dabao, Onion, RainBow, Vantim, Batman, Pikachu, Skull,
                        Shield, Khien,
                        Devil, Angel, Witch, Deadpool, Thor}
public class CanvasSkinShop : UICanvas
{
    #region UI Variables
    [Header("Skin Shop")]
    public int[] ClothesPrice ={
                        100, 120, 150, 160, 180, 200, 220, 250, 280,
                        250, 290, 300, 330, 350, 380, 400, 450, 550,
                        700, 850,
                        1500, 2000, 2500, 3000, 3500};
    // UI function
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _CantBuypriceText;
    [SerializeField] private TextMeshProUGUI _CanBuypriceText;
    [SerializeField] private Transform _Shop;
    [SerializeField] private Transform[] _shopBackGround;
    [SerializeField] private Transform[] _shopScrollView;   //Scroll View
    [SerializeField] private Image[] _Frame;
    [SerializeField] private Image[] _Lock;
    [SerializeField] private GameObject[] _stateButton;
    Dictionary<ClothType, ClothState> ClothesShopInfo = new Dictionary<ClothType, ClothState>();
    Dictionary<ClothType, ClothLockState> ClothesLockInfo = new Dictionary<ClothType, ClothLockState>();
    public int ClothesID;
    #endregion
    private void Awake()
    {
        ClothesID = 0;
        for (int i = 0; i < ClothesPrice.Length; i++)    //Tạo List để quản lý trạng thái của từng sản phẩm
        {
            ClothesShopInfo.Add((ClothType)i, ClothState.CantBuy);
            ClothesLockInfo.Add((ClothType)i, ClothLockState.Lock);
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) == 1)
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.CantBuy);
            }
            else if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) == 3)
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.Select);
            }
            else if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) == 4)
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.Equipped);
            }
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.shopCamera.gameObject.SetActive(false);
        GameObject.FindObjectOfType<PlayerController>().OnIdle();
    }
    public override void OnInit()
    {
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
        HatShopClick();
        GetRauID();
        GameManager.Instance.shopCamera.gameObject.SetActive(true);
        GameObject.FindObjectOfType<PlayerController>().OnDance();
    }
    public void XButton()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.MainMenu);
    }
    #region Show Shop
    public void HatShopClick()
    {
        GetRauID();
        OpenShop(ShopType.HatShop);
    }
    public void PantShopClick()
    {
        GetChambiID();
        OpenShop(ShopType.PantShop);
    }
    public void ShieldShopClick()
    {
        GetShieldID();
        OpenShop(ShopType.ShieldShop);
    }
    public void SetShopClick()
    {
        GetDevilID();
        OpenShop(ShopType.SetShop);
    }
    void OpenShop(ShopType _shopType)
    {
        for (int i = 0; i < _Shop.childCount; i++)
        {
            if (i == (int)_shopType)
            {
                _Shop.GetChild(i).gameObject.SetActive(true);
                _shopBackGround[i].gameObject.SetActive(false);
                _shopScrollView[i].gameObject.SetActive(true);
            }
            else
            {
                _shopBackGround[i].gameObject.SetActive(true);
                _shopScrollView[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Get ID
    public void GetRauID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 8;
        ChangePrice();
    }

    public void GetCrownID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 2;
        ChangePrice();
    }

    public void GetEarID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 3;
        ChangePrice();
    }

    public void GetHatID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 4;
        ChangePrice();
    }

    public void GetHatCapID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 5;
        ChangePrice();
    }

    public void GetHatYellowID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 6;
        ChangePrice();
    }

    public void GetArrowID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 0;
        ChangePrice();
    }

    public void GetCowboyID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 1;
        ChangePrice();
    }

    public void GetHeadphoneID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 7;
        ChangePrice();
    }

    public void GetChambiID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 12;
        ChangePrice();
    }

    public void GetComyID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 13;
        ChangePrice();
    }

    public void GetDabaoID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 14;
        ChangePrice();
    }

    public void GetOnionID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 15;
        ChangePrice();
    }

    public void GetRainBowID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 17;
        ChangePrice();
    }

    public void GetVantimID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 19;
        ChangePrice();
    }

    public void GetBatmanID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 11;
        ChangePrice();
    }

    public void GetPikachuID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 16;
        ChangePrice();
    }

    public void GetSkullID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 18;
        ChangePrice();
    }

    public void GetShieldID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 10;
        ChangePrice();
    }

    public void GetKhienID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 9;
        ChangePrice();
    }

    public void GetDevilID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 20;
        ChangePrice();
    }

    public void GetAngelID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 21;
        ChangePrice();
    }

    public void GetWitchID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 22;
        ChangePrice();
    }

    public void GetDeadpoolID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 23;
        ChangePrice();
    }

    public void GetThorID()
    {
        GameManager.Instance.PlayClickSound();
        ClothesID = 24;
        ChangePrice();
    }
    #endregion

    void ChangePrice()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if(i== ClothesID)
            {
                _CantBuypriceText.text = "" + ClothesPrice[i];
                _CanBuypriceText.text = "" + ClothesPrice[i];
                _Frame[i].gameObject.SetActive(true);
            }
            else
            {
                _Frame[i].gameObject.SetActive(false);
            }
        }
        UpdateButtonState();
    }

    public void BuyClothes()
    {
        GameManager.Instance.PlayClickSound();
        if (UIManager.Instance.coinAmount>= ClothesPrice[ClothesID])
        {
            UIManager.Instance.coinAmount -= ClothesPrice[ClothesID];
            ClothesShopInfo.Remove((ClothType)ClothesID);
            ClothesShopInfo.Add((ClothType)ClothesID, ClothState.Select);

            ClothesLockInfo.Remove((ClothType)ClothesID);
            ClothesLockInfo.Add((ClothType)ClothesID, ClothLockState.UnLock);

            PlayerPrefs.SetInt("ClothesShop" + (ClothType)ClothesID, 3);
            PlayerPrefs.Save();
        }
        UpdateButtonState();
        UpdateCoinAmount();
        UpdateUnLockState();
        PlayerPrefs.SetInt("Score", UIManager.Instance.coinAmount);
        PlayerPrefs.Save();
        
    }
    public void Equip()
    {
        GameManager.Instance.PlayClickSound();
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if (ClothesShopInfo[(ClothType)i] == ClothState.Equipped)   //Chuyển tất cả những vật phẩm đang ở trạng thái Equipped sang trạng thái Select
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.Select);
                PlayerPrefs.SetInt("ClothesShop" + (ClothType)i, 3);
                PlayerPrefs.Save();
            }
        }
        if (ClothesShopInfo[(ClothType)ClothesID] == ClothState.Select) //Chuyển trạng thái của vật phẩm đang được chọn sang Equipped
        {
            ClothesShopInfo.Remove((ClothType)ClothesID);
            ClothesShopInfo.Add((ClothType)ClothesID, ClothState.Equipped);
            GameObject.FindObjectOfType<PlayerController>().ChangeClothes((PlayerController.clothesType)ClothesID);
            PlayerPrefs.SetInt("ClothesShop" + (ClothType)ClothesID, 4);
            PlayerPrefs.Save();
        }
        UpdateButtonState();
    }

    void UpdateButtonState()  
    {
        for (int i = 0; i < 25; i++)
        {
            if (UIManager.Instance.coinAmount < ClothesPrice[i] && ClothesShopInfo[(ClothType)i] == ClothState.CanBuy)      //So sánh số tiền với giá. Nếu giá lớn hơn mà trạng thái vẫn để Canbuy thì đổi thành ContBuy
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.CantBuy);
            }
            else if (UIManager.Instance.coinAmount >= ClothesPrice[i] && ClothesShopInfo[(ClothType)i] == ClothState.CantBuy)//So sánh số tiền với giá. Nếu giá nhỏ hơn mà trạng thái vẫn để Cantbuy thì đổi thành ContBuy
            {
                ClothesShopInfo.Remove((ClothType)i);
                ClothesShopInfo.Add((ClothType)i, ClothState.CanBuy);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if(ClothesShopInfo[(ClothType)ClothesID] == (ClothState)i)
            {
                _stateButton[i].SetActive(true);
            }
            else
            {
                _stateButton[i].SetActive(false);
            }
        }
    }

    void UpdateUnLockState()
    {
        for (int i = 0; i < ClothesPrice.Length; i++)
        {
            if(ClothesLockInfo[(ClothType)i]== ClothLockState.UnLock)
            {
                _Lock[i].gameObject.SetActive(false);
            }
        }
    }
    void UpdateCoinAmount()
    {
        _coinAmountText.text = "" + UIManager.Instance.coinAmount;
    }
}
