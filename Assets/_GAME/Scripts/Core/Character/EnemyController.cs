using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : Character, IInitializeVariables, IHit
{
    #region Parameter
    [SerializeField] private Animator enemyAnimation;
    public NavMeshAgent agent;
    public Vector3 EnemyDestination;
    private float timeLimit;
    private float timeCouting;
    private Vector3 positionToAttack;
    public int Level;
    public CharacterName enemyName;
    private IState currentState;
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        enemyName = (CharacterName)Random.Range(0, 16);
        ChangeClothes((clothesType)Random.Range(0, 24));
    }
    void Start()
    {
        InitializeVariables();
        GameManager.Instance.CharacterList.Add(this); //Tất cả các enemy được sinh ra sẽ được Add vào trong CharacterList này để quản lý.
    }
    // Update is called once per frame
    void Update()
    {
        timeCouting += Time.deltaTime;
        if (!IsDeath&& GameManager.Instance.gameState == GameManager.GameState.gameStarted|| GameManager.Instance.gameState == GameManager.GameState.gameOver)
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
    }
    public void EnemyMovement()
    {
        OnRun();
        if (GameManager.Instance.gameState==GameManager.GameState.gameStarted|| GameManager.Instance.gameState == GameManager.GameState.gameOver)
        {
            agent.SetDestination(EnemyDestination);
            OnRun();
            enableToAttackFlag = true;
        }
    }

    public void EnemyStopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public void FindNextDestination()
    {
        EnemyDestination = new Vector3(Random.Range(-24f, 24f), 0, Random.Range(-18.5f, 18.5f)); //Find the random position
    }

    public void CheckArriveDestination()
    {
        if (Vector3.Distance(transform.position, EnemyDestination)<0.3f)
        {
            ChangeState(new StateEnemyIdle());
        }
    }
    public void ChangeState(IState state)   //Hàm chuyển đổi trạng thái State
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
    public void RestartTimeCounting()
    {
        timeCouting = 0;
        timeLimit = Random.Range(1.5f, 3.5f);
    }
    
    public void CheckIdletoAttack()
    {
        if (FindNearistEnemy(AttackRange) != Vector3.zero&& enableToAttackFlag) ChangeState(new StateEnemyAttack());
    }

    public void CheckPatroltoAttack()
    {
        if (FindNearistEnemy(AttackRange) != Vector3.zero && enableToAttackFlag&& timeCouting>2f) ChangeState(new StateEnemyAttack());
    }
    public void CheckIfAttackIsDone()
    {
        if(enemyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime>1|| timeCouting>1.03f)
        {
            ChangeState(new StateEnemyIdle());
        }
    }

    public void CheckIdletoPatrol()
    {
        if (timeCouting > timeLimit)
        {
            ChangeState(new StateEnemyPatrol());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            ChangeState(new StateEnemyPatrol());
        }
    }

    public void InitializeVariables()
    {
        AttackRange = 5f;
        AttackSpeed = 10;
        weaponListCreate(); //Khởi tạo danh sách vũ khí
        weaponSwitching((weaponType)Random.RandomRange((int)weaponType.Arrow, (int)weaponType.Z));   //Đổi vũ khí và Material của vũ khí vào
        AddWeaponPower();
        IsDeath = false;
        Level = 0;
        OnResetAllTrigger();
        ChangeState(new StateEnemyIdle());
    }

    public override void attack()
    {
        transform.LookAt(FindNearistEnemy(AttackRange));
        OnAttack();
        enableToAttackFlag = false;
        attackScript.SetID(gameObject.GetInstanceID(), opponentID);
    }
    public void OnHit()
    {
        PlayDieAudio();
        currentState.OnExit(this);
        IsDeath = true;
        agent.SetDestination(transform.position);
        GameManager.Instance.KilledAmount++;
        OnDeath();
        StartCoroutine(EnemyDeath());
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(2f);
        Pooling.instance._Push(gameObject.tag, gameObject);
    }

    public override void AddLevel()
    {
        characterCanvasAnim.SetTrigger("AddLevel");
        Level++;
        transform.localScale = new Vector3(1f + 0.1f * Level, 1f + 0.1f * Level, 1f + 0.1f * Level);
        agent.speed = (1f + 0.05f * Level) * 5f;
        AttackRange = 1.05f*AttackRange;
    }

    public void weaponSwitching(weaponType _weaponType)
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (i == (int)_weaponType)
            {
                Material[] CurrentWeaponMaterial = weaponArray[i].GetComponent<Renderer>().sharedMaterials;
                Material temp = GetRandomWeaponMaterial(_weaponType);
                for (int j = 0; j < weaponArray[i].GetComponent<Renderer>().sharedMaterials.Length; j++)
                {
                    CurrentWeaponMaterial[j] = temp;
                }
                weaponArray[i].GetComponent<Renderer>().sharedMaterials = CurrentWeaponMaterial;
                weaponArray[i].SetActive(true);
            }
            else
            {
                weaponArray[i].SetActive(false);
            }
        }
    }
    #region Get Random Weapon Material
    public Material GetRandomWeaponMaterial(weaponType _weaponType)
    {
        switch (_weaponType)
        {
            case weaponType.Arrow:
                return _weapon.ArrowDefaultMaterials[Random.Range(0, _weapon.ArrowDefaultMaterials.Length)];
            case weaponType.Axe_0:
                return _weapon.Axe0DefaultMaterials[Random.Range(0, _weapon.Axe0DefaultMaterials.Length)];
            case weaponType.Axe_1:
                return _weapon.Axe1DefaultMaterials[Random.Range(0, _weapon.Axe1DefaultMaterials.Length)];
            case weaponType.boomerang:
                return _weapon.BoomerangDefaultMaterials[Random.Range(0, _weapon.BoomerangDefaultMaterials.Length)];
            case weaponType.candy_0:
                return _weapon.Candy0DefaultMaterials[Random.Range(0, _weapon.Candy0DefaultMaterials.Length)];
            case weaponType.candy_1:
                return _weapon.Candy1DefaultMaterials[Random.Range(0, _weapon.Candy1DefaultMaterials.Length)];
            case weaponType.candy_2:
                return _weapon.Candy2DefaultMaterials[Random.Range(0, _weapon.Candy2DefaultMaterials.Length)];
            case weaponType.candy_4:
                return _weapon.Candy4DefaultMaterials[Random.Range(0, _weapon.Candy4DefaultMaterials.Length)];
            case weaponType.Hammer:
                return _weapon.HammerDefaultMaterials[Random.Range(0, _weapon.HammerDefaultMaterials.Length)];
            case weaponType.knife:
                return _weapon.KnifeDefaultMaterials[Random.Range(0, _weapon.KnifeDefaultMaterials.Length)];
            case weaponType.uzi:
                return _weapon.UziDefaultMaterials[Random.Range(0, _weapon.UziDefaultMaterials.Length)];
            case weaponType.Z:
                return _weapon.ZDefaultMaterials[Random.Range(0, _weapon.ZDefaultMaterials.Length)];
            default:
                return _weapon.ArrowDefaultMaterials[Random.Range(0, _weapon.ArrowDefaultMaterials.Length)];
        }
    }
    #endregion
}
