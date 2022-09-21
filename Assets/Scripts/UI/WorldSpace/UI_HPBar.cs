using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_HPBar : UIBase
{
    enum Sliders
    {
        HPBar,
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
    }
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        SetHpRatio(1);
    }
    public void SetHpRatio(float ratio)
    {
        GetSlider((int)Sliders.HPBar).value = ratio;
    }
}
