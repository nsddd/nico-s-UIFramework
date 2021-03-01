using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : BasePanel
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null) canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    public void OnClosePanel()
    {
        PanelManager.Instance.PopStack();
    }
}
