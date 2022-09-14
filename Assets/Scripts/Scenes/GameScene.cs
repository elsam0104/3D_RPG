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
    }
    public override void Clear()
    {

    }
}
