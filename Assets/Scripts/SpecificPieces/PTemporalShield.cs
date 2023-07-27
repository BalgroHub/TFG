using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTemporalShield : Piece
{
    public float cooldownShield;
    public bool shieldActive;

    public float attSpeDuration;
    public float attSpePer;


    IEnumerator CooldownCorrutine()
    {
        //Debug.Log("Starting cd!");

        shieldActive = false;
        yield return new WaitForSeconds(cooldownShield);

        //We set it as ready and add the buff to the temporalDefenseMultipliers
        shieldActive = true;
        PlayerStats.Instance.AddDamRedMultiplier(100f, identifier);

        //Visuals
        Color white = new Color(1, 1, 1, 1);
        GameObject.Find("ShieldSprite").GetComponent<SpriteRenderer>().color = white;
    }

    IEnumerator AttSpeDurationCorrutine()
    {
        yield return new WaitForSeconds(attSpeDuration);
        PlayerStats.Instance.RemoveAttSpeMultiplier(identifier);
    }




    /// <summary>
    /// This function is called after the player takes damage
    /// </summary>
    public override void AfterHitTrigger()
    {
        if(shieldActive)
        {
            //Shield
            PlayerStats.Instance.RemoveDamRedMultiplier(identifier);
            StartCoroutine(CooldownCorrutine());

            //AttSpe bonus
            PlayerStats.Instance.AddAttSpeMultiplier(attSpePer, identifier);
            StartCoroutine(AttSpeDurationCorrutine());

            //Visuals
            Color white = new Color(1, 1, 1, 0);
            GameObject.Find("ShieldSprite").GetComponent<SpriteRenderer>().color = white;


        }  
    }

    public override void TypeActions()
    {
        //We start the cooldown
        StartCoroutine(CooldownCorrutine());
    }

}
