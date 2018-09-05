using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : SelectableObject {

    public string shopName = "Test";
    [TextArea]
    public string description = "Just a shop";


    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
    }
}
