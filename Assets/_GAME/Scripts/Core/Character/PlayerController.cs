using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Character, IInitializeVariables, IHit
{
    public static PlayerController instance;
    public FloatingJoystick _Joystick;
    [SerializeField] private GameObject _Cycle;
    [SerializeField] private GameObject Reticle;
    [SerializeField] private Material[] CupMaterial;
    private Vector3 positionToAttack;
    public int Level;
    public CharacterName KillerName;
    private IStatePlayer currentState;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
        GameManager.Instance.CharacterList.Add(this);    //Thêm player vào trong list Character để quản lý
    }
    // Update is called once per frame
    void Update()
    {
        ShowReticle();
        ObstacleFading();
        if (!IsDeath && GameManager.Instance.gameState == GameManager.GameState.gameStarted)
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
        else if (GameManager.Instance.gameState == GameManager.GameState.gameWin) OnWin();
        _Cycle.transform.position = transform.position;
        if (GameManager.Instance.IsAliveAmount == 1 && !IsDeath) StartCoroutine(CheckGameVictory());
    }

    public override void move()
    {
        if (GameManager.Instance.gameState==GameManager.GameState.gameStarted)
        {
            if (_Joystick.Horizontal != 0 || _Joystick.Vertical != 0)       //Nếu vị trí joystick được di chuyển thì Move Player
            {
                Vector3 temp = transform.position;
                temp.x -= _Joystick.Vertical * Time.deltaTime * MoveSpeed;
                temp.z += _Joystick.Horizontal * Time.deltaTime * MoveSpeed;
                Vector3 moveDirection = new Vector3(temp.x - transform.position.x, 0, temp.z - transform.position.z);
                moveDirection.Normalize();
                Quaternion toRotate = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 720 * Time.deltaTime);
                transform.position = temp;
                enableToAttackFlag = true;      //Bật cờ Attack để khi Character dừng lại thì sẽ tấn công nếu có Enemy ở trong bán kính tấn công.
            }
        }
    }

    public void CheckIdleToPatrol()
    {
        if ((_Joystick.Horizontal != 0 || _Joystick.Vertical != 0)&&!IsDeath) ChangeState(new StatePlayerPatrol());
    }
    public void CheckPatrolToIdle()
    {
        if ((_Joystick.Horizontal == 0 && _Joystick.Vertical == 0)&&!IsDeath) ChangeState(new StatePlayerIdle());
    }
    public void CheckIdletoAttack()
    {
        
        if (enableToAttackFlag && FindNearistEnemy(AttackRange) != Vector3.zero&&!IsDeath)
        {
            ChangeState(new StatePlayerAttack());
        }
    }
    public override void attack()
    {
        transform.LookAt(positionToAttack);
        enableToAttackFlag = false;
        attackScript.SetID(gameObject.GetInstanceID(), opponentID);
        StartCoroutine(TurntoIdle());
    }
    IEnumerator TurntoIdle()
    {
        yield return new WaitForSeconds(0.5f);
        if(GameManager.Instance.gameState == GameManager.GameState.gameStarted&& _Joystick.Horizontal == 0 && _Joystick.Vertical == 0&&!IsDeath) ChangeState(new StatePlayerIdle());
    }

    void changeAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        _Cycle.transform.localScale = new Vector3(AttackRange, 1f, AttackRange);
    }

    #region Reticle
    void ShowReticle() //Hiện mục tiêu của Player
    {
        positionToAttack = FindNearistEnemy(AttackRange);
        if (positionToAttack != Vector3.zero)
        {
            Reticle.transform.position = new Vector3(positionToAttack.x, 0.1f, positionToAttack.z);
            Reticle.SetActive(true);
        }
        else
        {
            Reticle.SetActive(false);
        }
    }
    #endregion  
    #region Singleton
    void IInitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public void InitializeVariables()
    {
        AttackRange = 5f;
        AttackSpeed = 10;
        MoveSpeed = 6f;
        weaponListCreate();                 //Khởi tạo danh sách vũ khí
        CreateListOfWeaponMaterial();       //Khởi tạo danh sách Material của vũ khí
        weaponSwitching(weaponType.Hammer, new weaponMaterialsType[] {weaponMaterialsType.Hammer_1});
        UpdatePlayerItem();
        IInitializeSingleton();
        changeAttackRange(AttackRange);             
        IsDeath = false;
        Level = 0;
        ChangeState(new StatePlayerIdle());
    }

    public void OnHit()
    {
        currentState.OnExit(this);
        OnDeath();
        Reticle.SetActive(false);
        IsDeath = true;
        GameManager.Instance.KilledAmount++;
        GameManager.Instance.gameState = GameManager.GameState.gameOver;
        PlayDieAudio();
    }

    void ObstacleFading()
    {
        foreach (Transform _obstacle in GameManager.Instance.Obstacle)
        {
            if (Vector3.Distance(transform.position, _obstacle.position) < 8f)
            {
                _obstacle.GetComponent<Renderer>().sharedMaterial = CupMaterial[1];
            }
            else
            {
                _obstacle.GetComponent<Renderer>().sharedMaterial = CupMaterial[0];
            }
        }
    }
    public override void AddLevel()                                                                     //Mỗi lần bắn hạ đối thủ thì sẽ gọi hàm AddLevel
    {
        characterCanvasAnim.SetTrigger("AddLevel");                                                     //Chạy Anim +1 khi giết được 1 enemy
        Level++;
        transform.localScale = new Vector3(1f + 0.1f * Level, 1f + 0.1f * Level, 1f + 0.1f * Level);    //Khi tăng 1 level thì sẽ tăng Scale của Player thêm 10% so với kích thước khi Start game
        MoveSpeed = (1f + 0.05f * Level) * 5f;                                                          //Tốc độ di chuyển của Player tăng 5% so với khi Start game.
        changeAttackRange(1.05f * AttackRange);                                                         //Tăng 5% tầm bắn
        PlaySizeUpAudio();
    }

    public void weaponSwitching(weaponType _weaponType, weaponMaterialsType[] _weaponMaterial)
    {
        AttackRange = 5f;
        AttackSpeed = 10;
        MoveSpeed = 5f;
        Material[] CurrentWeaponMaterial = new Material[_weaponMaterial.Length];
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (i == (int)_weaponType)
            {
                CurrentWeaponMaterial = weaponArray[i].GetComponent<Renderer>().sharedMaterials;
                CurrentWeaponMaterial = GetWeaponMaterial(_weaponType,_weaponMaterial);
                weaponArray[i].GetComponent<Renderer>().sharedMaterials = CurrentWeaponMaterial;
                weaponArray[i].SetActive(true);
            }
            else
            {
                weaponArray[i].SetActive(false);
            }
        }
        AddWeaponPower();
    }

    Material[] GetWeaponMaterial(weaponType _weaponType, weaponMaterialsType[] _weaponMaterial)
    {
        Material[] desireMaterials = new Material[_weaponMaterial.Length];
        if (_weaponMaterial.Length == 1)
        {
            Material[] desireMaterial = ListWeaponMaterial[_weaponMaterial[0]];
            return desireMaterial;
        }
        else
        {
            for (int i = 0; i < _weaponMaterial.Length; i++)
            {
                desireMaterials[i] = ListWeaponMaterial[_weaponMaterial[i]][0];
            }
        }
        return desireMaterials;
    }

    
    public void ChangeState(IStatePlayer state)
    {
        if (state != currentState)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }
            currentState = state;
            if (currentState != null)
            {
                currentState.OnEnter(this);
            }
        }
    }

    public void UpdatePlayerItem()  //Trang bị clothes và weapon cho Player khi tắt đi bật lại
    {
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("WeaponShop" + (weaponType)i) == 4)
            {
                weaponSwitching((weaponType)i, new weaponMaterialsType[] { weaponMaterialsType.Arrow });
            }
        }
        for (int i = 0; i < 25; i++)
        {
            if (PlayerPrefs.GetInt("ClothesShop" + (ClothType)i) == 4)
            {
                ChangeClothes((clothesType)i);
            }
        }
    }
    IEnumerator CheckGameVictory()
    {
        yield return new WaitForSeconds(1);
        if (GameManager.Instance.IsAliveAmount == 1 && !IsDeath)
        {
            GameManager.Instance.gameState = GameManager.GameState.gameWin;    //Chỉ còn 1 character còn sống và Player vẫn sống thì Victory
            PlayWinAudio();
        }
        else GameManager.Instance.gameState = GameManager.GameState.gameOver;
    }
}
