using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingarmor : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "healing";

    public override string Description => "healing";

    public override kind GetKinds()
    {
        return kind.normal;
    }
}
