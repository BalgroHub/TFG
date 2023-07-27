using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRepairSystem : Piece
{

    public float damageReductionPer;
    public float lifePerSecond;



    /// <summary>
    /// We add the defenes multiplier to the permanentDefense in the PlayerStats scrip
    /// </summary>
    public void AddDefense()
    {
        PlayerStats.Instance.AddDamRedMultiplier(damageReductionPer);
    }


    //We
    public override void TypeActions()
    {
        AddDefense();
        StartCoroutine(HealCorrutine());
    }

    IEnumerator HealCorrutine()
    {
        while(active)
        {
            yield return new WaitForSeconds(1);

            //We heal the player
            PlayerStats.Instance.ChangeLife(lifePerSecond);

        }

    }
}
