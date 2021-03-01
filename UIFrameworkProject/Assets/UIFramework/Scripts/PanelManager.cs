using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager //: MonoBehaviour
{
    #region 单例模式
    private static PanelManager _instance;
    public static PanelManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PanelManager();
            }
            return _instance;
        }
    }

    private PanelManager()
    {
        ParsePanelTypeJson();
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    #endregion
    //private void Awake()
    //{
    //}
    //private void Start()
    //{
     
    //}

    private Dictionary<PanelType,string> panelPathDict;//保存从Json中解析的面板信息
    private Dictionary<PanelType, BasePanel> panelDict;//保存UI面板和面板类型
    private Stack<BasePanel> panelStack;//面板栈

    /// <summary>
    /// 面板入栈
    /// </summary>
    public void PushStack(PanelType panelType)
    {
        if(panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if(panelStack.Count > 0)
        {
            //如果栈里边有面板,那么就将栈顶面板禁用,然后继续产生下一个面板
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetUIPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    /// <summary>
    /// 面板出栈
    /// </summary>
    public void PopStack()
    {
        if(panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0) return;
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    /// <summary>
    /// 解析Json
    /// </summary>
    public void ParsePanelTypeJson()
    {
        panelPathDict = new Dictionary<PanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("PanelJson");
        PanelList jsonObject = JsonUtility.FromJson<PanelList>(ta.text);
        foreach (PanelProp info in jsonObject.panelList)
        {
            //panelDict.Add(info.panelType, info.panelPath);
            panelPathDict[info.panelType] = info.panelPath;
        }
    }

    BasePanel GetUIPanel(PanelType panelType)
    {
        if(panelDict == null)
        {
            panelDict = new Dictionary<PanelType, BasePanel>();
        }
        string path = panelPathDict.TryGet(panelType);
        //panelPathDict.TryGetValue(panelType, out path);
        GameObject instPanel = GameObject.Instantiate(Resources.Load(path), CanvasTransform, false) as GameObject;
        panelDict[panelType] = instPanel.GetComponent<BasePanel>();
        return instPanel.GetComponent<BasePanel>();
    }
}


[System.Serializable]
class PanelList
{
    public List<PanelProp> panelList;
}
