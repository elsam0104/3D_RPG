using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;
    Stack<UIPopUp> _popupStack = new Stack<UIPopUp>();
    UIScene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }
    
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utill.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Utill.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.parent = Root.transform;
        return sceneUI;
    }

    public T ShowPopUpUI<T>(string name = null) where T : UIPopUp
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/PopUp/{name}");

        T popupUI = Utill.GetOrAddComponent<T>(go);
        _popupStack.Push(popupUI);

        go.transform.parent = Root.transform;
        return popupUI;
    }
    public void ClosePopUpUI(UIPopUp popupUI)
    {
        if (_popupStack.Count == 0) return;

        if(_popupStack.Peek()!=popupUI)
        {
            Debug.LogWarning("Close PopUp Failed!");
            return;
        }
        ClosePopUpUI();
    }
    public void ClosePopUpUI()
    {
        if (_popupStack.Count == 0) return;

        UIPopUp popupUI = _popupStack.Pop();
        Managers.Resource.Destroy(popupUI.gameObject);
        popupUI = null;
        _order--;
    }
    public void CloseAllPopUpUI()
    {
        while (_popupStack.Count > 0)
            ClosePopUpUI();
    }
}
