using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPropellers : Piece
{
    public float AttackSpeedBuff;
    public float temporalAttackSpeedBuff;

    public override void TypeActions()
    {
        AddAttackSpeed();
    }

    public void AddAttackSpeed()
    {
        PlayerStats.Instance.AddAttSpeMultiplier(AttackSpeedBuff);
        PlayerStats.Instance.AddAttSpeMultiplier(temporalAttackSpeedBuff, identifier);

        StartCoroutine(AttackSpeedCountdown(7));
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
