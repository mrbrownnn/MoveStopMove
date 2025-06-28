using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>,IInitializeVariables
{
    public enum GameState { gameUI, gameStarted, gameOver, gameWin }
    public GameState gameState;
    public List<Character> CharacterList = new List<Character>();
    public Transform[] Obstacle;
    public Camera mainCamera;
    public Camera shopCamera;
    public int TotalCharacterAmount, IsAliveAmount, KilledAmount, TotalCharAlive, SpawnAmount;
    private int LevelID;
    public bool OpenSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip WeaponImpact;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip loseAudio;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeVariables();
        LevelID = 1;
        LevelID = PlayerPrefs.GetInt("LevelID");
        OpenSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsAliveAmount = IsAliveCounting();
        TotalCharAlive = SpawnAmount + IsAliveAmount;
    }
    public void InitializeVariables()
    {
        gameState = GameState.gameUI;
        TotalCharacterAmount = 50;
        KilledAmount = 0;
    }

    public void PlayClickSound()
    {
        if (OpenSound) audioSource.PlayOneShot(ClickSound);
    }
    public void PlayWeaponImpackSound()
    {
        if (OpenSound) audioSource.PlayOneShot(WeaponImpact);
    }
    public void PlayAttackAudio()
    {
        if (GameManager.Instance.OpenSound) audioSource.PlayOneShot(attackAudio);
    }
    public void PlayLoseAudio()
    {
        if (GameManager.Instance.OpenSound) audioSource.PlayOneShot(loseAudio);
    }
    public int IsAliveCounting() //Số character đang có trên map
    {
        int IsAliveAmount=0;
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (CharacterList[i].gameObject.activeSelf)
            {
                if (CharacterList[i].IsDeath == false) IsAliveAmount++;
            }
        }
        return IsAliveAmount;
    }

    public void LoadNewLevel()
    {
        LevelID++;
        if (LevelID > 2) LevelID = 1;
        PlayerPrefs.SetInt("LevelID", LevelID);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level" + LevelID);
    }
}
