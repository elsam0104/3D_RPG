using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : UIPopUp
{
    enum Buttons
    {
        PointButton,
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum Images
    {
        ItemImage,
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Text>((int)Texts.ScoreText).text = "점수";

        GameObject goBT = GetButton((int)Buttons.PointButton).gameObject;
        AddUIEvent(goBT, OnPointButtonClicked, Define.UIEvent.Click);

        GameObject goIM = GetImage((int)Images.ItemImage).gameObject;
        AddUIEvent(goIM, ((PointerEventData data) => { goIM.transform.position = data.position; }), Define.UIEvent.Drag);
    }
    int _score = 0;
    public void OnPointButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수:{_score}";
    }
}
