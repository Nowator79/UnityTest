using UnityEngine;

public class UIBase : MonoBehaviour
{
    public bool NoHidden = false;

    public void Hidden()
    {
        gameObject.SetActive(false);
    }
    public void Show(bool hiddenAll = true)
    {
        if (hiddenAll)
        {
            CanvasControler.StaticCanvasControler.HiddenUIs();
        }
        gameObject.SetActive(true);
    }

    public virtual void Init()
    {
    }
}
