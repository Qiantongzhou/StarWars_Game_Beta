using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsed_plasma_thrust1 : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "pulsed_plasma_thrust";

    public override string Description => "small increase in speed";

    public override kind GetKinds()
    {
        return kind.normal;
    }
}
