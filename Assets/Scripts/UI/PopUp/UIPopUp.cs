using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : UIBase
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    public virtual void ClosePopUI()
    {
        Managers.UI.ClosePopUpUI(this);
    }
}
