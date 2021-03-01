using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.PushStack(PanelType.MainMenu);
    }

}
