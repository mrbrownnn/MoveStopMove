using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IInitializeVariables
{
    private enum SpawnArea {BottomLeft, BottomRight , UpLeft , UpRight}
    private SpawnArea AreaToSpawn;
    private int CharacterOnMapAmount;
    private int BottomLeft, BottomRight, UpLeft, UpRight, minAmount;  //Dùng biến này để xác định số lượng Character ở từng vùng trong map
    private int SpawnAmount;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        FindAreaToSpawn();
        GameManager.Instance.SpawnAmount = SpawnAmount;
    }

    public void InitializeVariables()
    {
        CharacterOnMapAmount = 10; //Mặc định trên map chỉ có 10 Character
        SpawnAmount = GameManager.Instance.TotalCharacterAmount - 2;
    }

    #region Find the Area to Spawn Enemy
    void FindAreaToSpawn()
    {
        UpRight = 0;
        UpLeft = 0;
        BottomRight = 0;
        BottomLeft = 0;
        for (int i = 0; i < GameManager.Instance.CharacterList.Count; i++)
        {
            if (GameManager.Instance.CharacterList[i].gameObject.CompareTag("Enemy"))
            {
                if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x >= 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z >= 0) UpRight++;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x < 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z > 0) UpLeft++;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x > 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z < 0) BottomRight++;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x < 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z < 0) BottomLeft++;
            }
            else if (GameManager.Instance.CharacterList[i].gameObject.CompareTag("Player")) //Mục đích không cho Enemy Spawn ở gần Player.
            {
                if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x >= 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z >= 0) UpRight = GameManager.Instance.TotalCharacterAmount;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x < 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z > 0) UpLeft = GameManager.Instance.TotalCharacterAmount;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x > 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z < 0) BottomRight = GameManager.Instance.TotalCharacterAmount;
                else if (GameManager.Instance.CharacterList[i].gameObject.transform.position.x < 0 && GameManager.Instance.CharacterList[i].gameObject.transform.position.z < 0) BottomLeft = GameManager.Instance.TotalCharacterAmount;
            }
        }
        minAmount = UpRight;
        AreaToSpawn = SpawnArea.UpRight;
        if (minAmount> UpLeft)
        {
            minAmount = UpLeft;
            AreaToSpawn = SpawnArea.UpLeft;
        }
        if (minAmount > BottomRight)
        {
            minAmount = BottomRight;
            AreaToSpawn = SpawnArea.BottomRight;
        }
        if (minAmount > BottomLeft)
        {
            minAmount = BottomLeft;
            AreaToSpawn = SpawnArea.BottomLeft;
        }
        if (GameManager.Instance.IsAliveAmount < CharacterOnMapAmount && SpawnAmount > 0)
        {
            int InAttackRangeOfXEnemy=0;
            Vector3 desireSpawnPosition = Vector3.zero;

            if (AreaToSpawn == SpawnArea.UpRight) desireSpawnPosition = new Vector3(Random.Range(5f, 24f), 0, Random.Range(5f, 18.5f));
            else if (AreaToSpawn == SpawnArea.UpLeft) desireSpawnPosition = new Vector3(Random.Range(-24f, -5f), 0, Random.Range(5f, 18.5f));
            else if (AreaToSpawn == SpawnArea.BottomLeft) desireSpawnPosition = new Vector3(Random.Range(-24f, -5f), 0, Random.Range(-18.5f, -5f));
            else desireSpawnPosition = new Vector3(Random.Range(5f, 24f), 0, Random.Range(-18.5f, -5f));
            for (int i = 0; i < GameManager.Instance.CharacterList.Count; i++)  //Chỉ sinh Enemy tại điểm ko nằm trong Attackrange của bất cứ Character nào
            {
                if(Vector3.Distance(GameManager.Instance.CharacterList[i].transform.position, desireSpawnPosition) < GameManager.Instance.CharacterList[i].AttackRange&& !GameManager.Instance.CharacterList[i].IsDeath)
                {
                    InAttackRangeOfXEnemy++;
                }
            }
            if (InAttackRangeOfXEnemy==0&& desireSpawnPosition!=Vector3.zero)
            {
                GameObject gob = Pooling.instance._Pull("Enemy", "Prefabs/Enemy");
                gob.transform.position = desireSpawnPosition;
                EnemyController enemyController = gob.GetComponent<EnemyController>();
                enemyController.IsDeath = false;
                enemyController.InitializeVariables();
                SpawnAmount--;
            }
            
        }
    }
    #endregion
}
