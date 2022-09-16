using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill
{ 
    /// <summary>
    /// 자식을 찾아오는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="recursive">false면 직계자손만을 찾음</param>
    /// <returns></returns>
    public static T FindChild<T>(GameObject go, string name = null,bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;
        if(recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                Transform transform = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name)||transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
