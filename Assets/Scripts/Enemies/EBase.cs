using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBase : Enemy
{
    protected override void Awake()
    {
        life = 10;
        contactDamage = 20;
    }
}
