using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [HideInInspector] public enum CharacterName { ABI, Uniqlo, Bitis, Vinamilk, KoBaYaShi, Ford, Vinfast, ToYoTa, Yamato, Biden, Biladen, Vodka, Yamaha, Honda, Suzuki, NiShiNo, Furuki }
    [HideInInspector]public enum weaponType { Arrow, Axe_0, Axe_1, boomerang, candy_0, candy_1, candy_2, candy_4, Hammer, knife, uzi, Z}
    [HideInInspector]public enum AddPowerType {addition, multiplication}
    [HideInInspector]public enum weaponMaterialsType 
    { 
        Arrow,
        Axe0, Axe0_2,
        Axe1, Axe1_2, 
        Boomerang_1, Boomerang_2, 
        Candy0_1, Candy0_2, 
        candy1_1, candy1_2, candy1_3, candy1_4, 
        Candy2_1, Candy2_2, 
        Candy4_1, Candy4_2, 
        Hammer_1, Hammer_2, 
        Knife_1, Knife_2, 
        Uzi_1, Uzi_2,
        Z,
        Azure, Black,
        Blue, Chartreuse,
        Cyan, Green,
        Magenta, Orange,
        Red, Rose,
        SpringGreen, Violet,
        White, Yellow
    }
    [HideInInspector] public enum clothesType 
    { 
        Arrow, Cowboy, Crown, Ear, Hat, Hat_Cap, Hat_Yellow, HeadPhone, Rau, Khien, Shield,
        Batman, Chambi, comy, dabao, onion, pokemon, rainbow, Skull, Vantim,
        Devil, Angel, Witch, Deadpool, Thor
    }
    [HideInInspector]
    public enum SetFullOrNormal{SetFull, Normal}
    public SetFullOrNormal lastClothes;
    [SerializeField] private Animator anim;
    public UnityAction OnAttack;
    public UnityAction OnRun;
    public UnityAction OnIdle;
    public UnityAction OnDeath;
    public UnityAction OnWin;
    public UnityAction OnDance;
    public UnityAction OnUlti;
    public UnityAction OnResetAllTrigger;
    public Attack attackScript;
    public float AttackRange;
    public float AttackSpeed;
    public float MoveSpeed;
  //  public ClothesInfo CharacterClothes;
  //  public ClothesPower clothesPower;
    public Transform ShieldPosition;
    public Transform LeftHandPosition;
    public Transform HeadPosition;
    public Transform TailPosition;
    public Transform BackPosition;
    public Renderer PantsPositionRenderer;
    public Renderer SkinPositionRenderer;
    public Transform weaponPosition;                        //GameObject chứa weapon trên tay Character.
    public GameObject[] weaponArray = new GameObject[12];   //Mảng dùng để quản lý weapon trên tay Character
    public Animator characterCanvasAnim;
    public Weapon _weapon;
   // public EnemyRandomSkin _enemySkin;
    public bool enableToAttackFlag=false;
    public float distanceToNearistEnemy;
    public Vector3 nearistEnemyPosition;
    public int opponentID;
    public int EnemySkinID;
    public bool IsDeath;
    public AudioSource audiosource;
    
    [SerializeField] private AudioClip DieAudio;
    [SerializeField] private AudioClip SizeUpAudio;
    [SerializeField] private AudioClip WinAudio;
    public Dictionary<weaponMaterialsType, Material[]> ListWeaponMaterial = new Dictionary<weaponMaterialsType, Material[]>();
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        _weapon = GetComponent<Weapon>();
        PantsPositionRenderer = GetComponent<Renderer>();
        SkinPositionRenderer = GetComponent<Renderer>();
    }
    public virtual void attack()
    { 

    }

    public virtual void move() 
    {

    }

    public virtual void AddLevel()
    {

    }

    public void PlayDieAudio()
    {
        if (GameManager.Instance.OpenSound) audiosource.PlayOneShot(DieAudio);
    }
    public void PlaySizeUpAudio()
    {
        if (GameManager.Instance.OpenSound) audiosource.PlayOneShot(SizeUpAudio);
    }
    public void PlayWinAudio()
    {
        if (GameManager.Instance.OpenSound) audiosource.PlayOneShot(WinAudio);
    }
    public void weaponListCreate() //Thêm vũ khí vào weaponList
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            weaponArray[i] = weaponPosition.GetComponent<Transform>().transform.GetChild(i).gameObject;
        }
    }
    
    public Vector3 FindNearistEnemy(float attackRange)
    {
        distanceToNearistEnemy = 1000f;
        for (int i = 0; i < GameManager.Instance.CharacterList.Count; i++)
        {
            if (GameManager.Instance.CharacterList[i].gameObject.GetInstanceID() != gameObject.GetInstanceID() && Vector3.Distance(GameManager.Instance.CharacterList[i].gameObject.transform.position, gameObject.transform.position) < attackRange && GameManager.Instance.CharacterList[i].gameObject.activeSelf)
            {
                if (Vector3.Distance(GameManager.Instance.CharacterList[i].gameObject.transform.position, gameObject.transform.position) < distanceToNearistEnemy && GameManager.Instance.CharacterList[i].IsDeath == false)
                {
                    distanceToNearistEnemy = Vector3.Distance(GameManager.Instance.CharacterList[i].gameObject.transform.position, gameObject.transform.position);
                    nearistEnemyPosition = GameManager.Instance.CharacterList[i].gameObject.transform.position;
                    opponentID = GameManager.Instance.CharacterList[i].gameObject.GetInstanceID(); //Lấy ID của đối phương
                }
            }
        }
        if (distanceToNearistEnemy > 900f) return Vector3.zero;
        else return nearistEnemyPosition;
    }

    public void AddWeaponPower()     //Đang cầm loại nào thì sẽ cộng thêm AttackRange và AttackSpeed tương ứng vào.
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (weaponArray[i].activeSelf)
            {
             //   AttackRange += _weapon.AddAttackRange[i];
              //  AttackSpeed += _weapon.AddAttackSpeed[i];
             //   Material[] CurrentWeaponMaterial = weaponArray[i].GetComponent<Renderer>().sharedMaterials;
             //reader   CurrentWeaponMaterial = _weapon.GetWeaponMaterial((weaponType)i, _weapon.GetWeaponMaterialType((weaponType)i));
                break;
            }
        }
    }
}
