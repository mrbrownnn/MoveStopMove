using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private FloatingJoystick _Joystick;
    [SerializeField] private TextMeshProUGUI aliveAmount;
    [SerializeField] private GameObject guide;
    private void Awake()
    {
        GameObject.FindObjectOfType<PlayerController>()._Joystick = _Joystick;
    }

    // Update is called once per frame
    void Update()
    {  
        UpdateAliveNumber();
        if (guide.activeSelf)
        {
            if (Input.GetMouseButtonDown(0)) guide.SetActive(false);
        }
        if (GameManager.Instance.gameState == GameManager.GameState.gameOver)
        {
            StartCoroutine(GameOver());
            _Joystick.gameObject.SetActive(false);
        }
        else if (GameManager.Instance.gameState == GameManager.GameState.gameWin)
        {
            StartCoroutine(GameWin());
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        guide.SetActive(true);
        _Joystick.gameObject.SetActive(true);
    }

    public void OpenSetting()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.Setting);
    }

    public void UpdateAliveNumber()
    {
        aliveAmount.text = "Alive: " + GameManager.Instance.TotalCharAlive;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        UIManager.Instance.OpenUI(UIName.GameOver);
    }

    IEnumerator GameWin()
    {
        yield return new WaitForSeconds(2);
        UIManager.Instance.OpenUI(UIName.Victory);
    }
}
