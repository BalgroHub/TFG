using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public List<Vector2Int> blocks = new List<Vector2Int>();
    public int identifier = -1;
    public bool active = false;

    public List<PieceTypes> types;


    /// <summary>
    /// Function that is called when the gameplay starts. Every child class will override it
    /// </summary>
    public virtual void TypeActions() { }


    /// <summary>
    /// We set our active to the parameter passed
    /// </summary>
    /// <param name="boo"></param>
    public virtual void setActive(bool boo)
    {
        active = boo;
    }

    public virtual void AfterHitTrigger()
    {

    }

    //Specific Methods


    public virtual void ModifyAttSpe(float multiplier)
    {

    }

    public virtual void ModifyDamage(float multiplier)
    {

    }
}
