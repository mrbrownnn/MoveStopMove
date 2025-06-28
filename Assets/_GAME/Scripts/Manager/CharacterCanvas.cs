using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private Transform Character;
    [SerializeField] private EnemyRandomSkin _enemySkin;
    [SerializeField] private Image CharacterLevelBG;
    [SerializeField] private TextMeshProUGUI CharacterLevelText;
    [SerializeField] private TextMeshProUGUI CharacterName;
    private Character character;
    private Canvas canvas;
    private PlayerController characterController;
    private EnemyController enemyController;
    private TextMeshProUGUI characterNameTextMeshPro;
    private TextMeshProUGUI characterLevelTextMeshPro;
    // Start is called before the first frame update
    void Awake()
    {
        characterLevelTextMeshPro = CharacterLevelText.gameObject.GetComponent<TextMeshProUGUI>();
        character = Character.gameObject.GetComponent<Character>();
        characterController = Character.GetComponent<PlayerController>();
        enemyController = Character.GetComponent<EnemyController>();
        canvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        characterNameTextMeshPro = CharacterName.gameObject.GetComponent<TextMeshProUGUI>();
        if (Character.gameObject.CompareTag("Player"))
        {
            characterNameTextMeshPro.text = "You";
            characterNameTextMeshPro.color = Color.black;
            characterLevelTextMeshPro.text = "" + characterController.Level;
            CharacterLevelBG.color = Color.black;
        }
        else
        {
            characterNameTextMeshPro.text = "" + enemyController.enemyName;
            characterNameTextMeshPro.color = _enemySkin.EnemyColor[enemyController.EnemySkinID].color;
            characterLevelTextMeshPro.text = "" + enemyController.Level;
            CharacterLevelBG.color = _enemySkin.EnemyColor[enemyController.EnemySkinID].color;
        }
    }
    private void Update()
    {
        if (character.IsDeath == true) canvas.enabled = false;
        else canvas.enabled = true;
        if (Character.gameObject.CompareTag("Player")) characterLevelTextMeshPro.text = "" + characterController.Level;
        else characterLevelTextMeshPro.text = "" + enemyController.Level;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(new Vector3(GameManager.Instance.mainCamera.transform.position.x, GameManager.Instance.mainCamera.transform.position.y, Character.transform.position.z));
        transform.localScale =new Vector3(1/Character.localScale.x, 1 / Character.localScale.y, 1 / Character.localScale.z);
    }
}
