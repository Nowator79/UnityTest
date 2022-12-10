using UnityEngine;

public class UIMainMenu : UIBase
{
    public static UIMainMenu StaticUIMainMenu { get; private set; }
    private UIMainMenu()
    {
        StaticUIMainMenu = this;
    }
    [SerializeField]
    private UINetWorkForm UINetWorkForm;
    public void OpenNetWorkGame()
    {
        UINetWorkForm.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
