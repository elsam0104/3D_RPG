using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    public void OnUpdate()
    {
        if(Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if(_pressed)//클릭하고 있었다 = 클릭
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                    _pressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
