using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shouder : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "booster";

    public override string Description => "small increase in speed";

    public override kind GetKinds()
    {
        return kind.normal;
    }


}
