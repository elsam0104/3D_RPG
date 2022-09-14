using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{
    public Dictionary<int, Stat> StartDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StartDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader,key,Value>(string path) where Loader : ILoader<key,Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
