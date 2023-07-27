using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTankChasis : Piece
{

    public float damageReductionPer;


    /// <summary>
    /// We add the defenes multiplier to the permanentDefense in the PlayerStats scrip
    /// </summary>
    public void AddDefense()
    {
        PlayerStats.Instance.AddDamRedMultiplier(damageReductionPer);
    }

    public override void TypeActions()
    {
        AddDefense();
    }

}
