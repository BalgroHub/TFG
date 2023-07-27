using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POvercharger : Piece
{
    public float attackSpeedBuff;
    public float damageBuff;
    public float maxLifeReduction;

    public override void TypeActions()
    {
        AddAttackSpeed();
        AddDamage();

        ReduceMaxLife();
    }

    public void AddAttackSpeed()
    {
        PlayerStats.Instance.AddAttSpeMultiplier(attackSpeedBuff);
    }

    public void AddDamage()
    {
        PlayerStats.Instance.AddDamMultiplier(damageBuff);
    }

    public void ReduceMaxLife()
    {
        PlayerStats.Instance.lifeMax -= maxLifeReduction; 
    }


    /// <summary>
    /// When the time expires, we remove the temporal attack speed buff
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator AttackSpeedCountdown(float duration)
    {
        yield return new WaitForSeconds(duration);

        PlayerStats.Instance.RemoveAttSpeMultiplier(identifier);
    }
}
