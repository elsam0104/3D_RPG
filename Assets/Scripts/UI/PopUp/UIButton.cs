using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        //Get<Text>((int)Texts.ScoreText).text = "score";
    }
}
