using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ICEquipment : InternalCell
{
    public static InternalCell Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }



}
