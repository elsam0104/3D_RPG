using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Dictionary<int, Stat> dict = Managers.Data.StartDict;

        List<GameObject> list = new List<GameObject>();
        //list.Add(Managers.Resource.Instantiate("Knight"));
        //Managers.Resource.Destroy(list[i]);
        //list.RemoveAt(i);


    }
    public override void Clear()
    {

    }
}
