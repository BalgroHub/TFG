using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFast : Enemy
{
    protected override void Awake()
    {
        life = 2;
        contactDamage = 10;
    }
}
