using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI(UIName.GamePlay);
    }
    public void HomeButton()
    {
        GameManager.Instance.PlayClickSound();
        Application.LoadLevel(Application.loadedLevel);
    }
}
