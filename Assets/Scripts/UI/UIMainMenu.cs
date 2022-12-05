using UnityEngine;

public class UIMainMenu : UIBase
{
    [SerializeField]
    private UINetWorkForm UINetWorkForm;
    public void OpenNetWorkGame()
    {
        UINetWorkForm.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
