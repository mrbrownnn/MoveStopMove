using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasVictory : UICanvas
{
    private List<ClothType> _clothesType = new List<ClothType>();   //List Clothes chưa mua
    private List<weaponType> _weaponType = new List<weaponType>();  //List weapon chưa mua
    [SerializeField] private GameObject _congratulation;
    [SerializeField] private Animator _sliderAnim;
    [SerializeField] private GameObject _Button;
    [SerializeField] private Image[] presentWeaponImage;
    [SerializeField] private Image[] presentClothesImage;
    private bool weaponOrClothes;   //Nếu là true thì Present là weapon, nếu là false thì present là Clothes
    private ClothType _clothesPresent;
    private weaponType _weaponPresent;
    private int presentLoad;
    
    private void Awake()
    {
        presentLoad = 0;
    }
    private void OnEnable()
    {
        _congratulation.SetActive(false);
    }
    public override void OnInit()
    {
        _clothesType.Clear();   //Tạo List mới chứa các item mà Player chưa có
        _weaponType.Clear();

        if (PlayerPrefs.GetInt("weaponOrClothes") == 1) weaponOrClothes = true;
        else weaponOrClothes = false;

        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("WeaponShop" + (weaponType)i,99) != 99)
            {
                if (PlayerPrefs.GetInt("WeaponShop" + (weaponType)i) != 3 && PlayerPrefs.GetInt("WeaponShop" + (weaponType)i) != 4)
                {
                    _weaponType.Add((weaponType)i);
                }
            }
        }
        for (int i = 0; i < 25; i++)
        {
            if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i, 99) != 99)
            {
                if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) != 3 && PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) != 4)
                {
                    _clothesType.Add((ClothType)i);
                }
            }
        }

        if (PlayerPrefs.GetInt("presentLoad", 99) != 99) presentLoad = PlayerPrefs.GetInt("presentLoad");
        if (PlayerPrefs.GetInt("clothesPresent", 99) == 99 && PlayerPrefs.GetInt("weaponPresent", 99) == 99) RandomPresent();
        if (PlayerPrefs.GetInt("clothesPresent", 99) != 99)
        {
            _clothesPresent = (ClothType)PlayerPrefs.GetInt("clothesPresent");
        }
        if (PlayerPrefs.GetInt("weaponPresent", 99) != 99)
        {
            _weaponPresent = (weaponType)PlayerPrefs.GetInt("weaponPresent");
        }

        presentLoad += 25;
        PlayerPrefs.SetInt("presentLoad", presentLoad);
        PlayerPrefs.Save();
        StartCoroutine(PlayAnimation());
        ShowPresentImage();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("presentLoad" + presentLoad);
        Debug.Log("_weaponType" + _weaponType.Count);
        Debug.Log("_clothesType" + _clothesType.Count);
        Debug.Log("_clothesPresent" + _clothesPresent);
        Debug.Log("_weaponPresent" + _weaponPresent);
        Debug.Log("************************************");*/
        if (presentLoad > 100) GetPresent();
    }

    void GetPresent()   //Nếu đủ 100 điểm thì sẽ tự động nhận được present
    {
        if (!weaponOrClothes)
        {
            presentLoad = 0;
            PlayerPrefs.SetInt("presentLoad", presentLoad);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("ClothesShop" + (ClothType)GetItemID((int)_clothesPresent), 3);
            PlayerPrefs.Save();
        }
        else if (weaponOrClothes)
        {
            presentLoad = 0;
            PlayerPrefs.SetInt("presentLoad", presentLoad);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("WeaponShop" + _weaponPresent, 3);
            PlayerPrefs.Save();
        }
        RandomPresent();    //Sau khi nhận present thì lại Random chọn present mới
    }

    void RandomPresent()
    {
        if (Random.Range(0, 100) >= 50)
        {
            weaponOrClothes = true;
            PlayerPrefs.SetInt("weaponOrClothes", 1);
            PlayerPrefs.Save();
        }
        else
        {
            weaponOrClothes = false;
            PlayerPrefs.SetInt("weaponOrClothes", 0);
            PlayerPrefs.Save();
        }
        
        if (!weaponOrClothes)
        {
            _clothesPresent = (ClothType)(Random.Range(0, _clothesType.Count));
            PlayerPrefs.SetInt("clothesPresent", (int)_clothesPresent);
            PlayerPrefs.Save();
        }
        else
        {
            _weaponPresent = (weaponType)(Random.Range(0, _weaponType.Count));
            PlayerPrefs.SetInt("weaponPresent", (int)_weaponPresent);
            PlayerPrefs.Save();
        }

    }
    

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(1);
        _sliderAnim.SetTrigger(""+presentLoad+"%");
        if (presentLoad == 100)
        {
            GetPresent();
            _congratulation.SetActive(true);
        }
        StartCoroutine(ShowButton());
    }
    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(2);
        _Button.SetActive(true);
    }

    void ShowPresentImage()
    {
        if (weaponOrClothes)
        {
            for (int i = 0; i < 12; i++)
            {
                if (i == (int)_weaponPresent) presentWeaponImage[i].gameObject.SetActive(true);
                else presentWeaponImage[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 25; i++)
            {
                presentClothesImage[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 25; i++)
            {
                if (i == (int)_clothesPresent) presentClothesImage[i].gameObject.SetActive(true);
                else presentClothesImage[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 12; i++)
            {
                presentWeaponImage[i].gameObject.SetActive(false);
            }
        }
    }
    public void HomeButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void RestartButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void NextButton()
    {
        GameManager.Instance.LoadNewLevel();
        UIManager.Instance.OpenUI(UIName.GamePlay);
        GameManager.Instance.gameState = GameManager.GameState.gameStarted;
    }

    #region Get ID of Item
    int GetItemID(int _id)
    {
        switch (_id)
        {
            case 0:
                return 8;
            case 1:
                return 2;
            case 2:
                return 3;
            case 3:
                return 4;
            case 4:
                return 5;
            case 5:
                return 6;
            case 6:
                return 0;
            case 7:
                return 1;
            case 8:
                return 7;
            case 9:
                return 12;
            case 10:
                return 13;
            case 11:
                return 14;
            case 12:
                return 15;
            case 13:
                return 17;
            case 14:
                return 19;
            case 15:
                return 11;
            case 16:
                return 16;
            case 17:
                return 18;
            case 18:
                return 10;
            case 19:
                return 9;
            case 20:
                return 20;
            case 21:
                return 21;
            case 22:
                return 22;
            case 23:
                return 23;
            default:
                return 24;
        }
    }
    #endregion
}
