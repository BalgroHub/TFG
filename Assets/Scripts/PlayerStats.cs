using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //The reference that can be acces fron anywhere (Singleton)
    public static PlayerStats Instance { get; private set; }

    //Normal variables
    public float lifeMax;
    public float lifeCurrent;

    //Life change variables
    public GameObject healthBarGO;
    public Slider healthBarSL;
    public GameObject healthTextGO;
    public TextMeshProUGUI healthText;

    

    //We have 2 list of floats, representing the multipliers of damage the player recieve. One is permanent and the other are temporal multipliers(cooldown abilities)
    //The int list are the identifiers of the pieces generating that multiplier (example the piece 3 has a 0.5 multiplier, so the identifiers(2) = 3, and multipliers(2) = 0.5)
    public  List<float> permanentDamRedMultipliers;
    public  List<float> temporalDamRedMultipliers;
    public  List<int>   temporalDamRedIdentifiers;
    [SerializeField] private float permanentDamRedFinal;
    [SerializeField] private float temporalDamRedFinal;


    //We have 2 list of floats, representing the multipliers of attack speed the pieces of the player has. One is permanent and the one are temporal.
    //Each piece that ads a temporal multiplier, also adds their identifier to the list, so we know wich multiplier if from wich piece
    public List<float> permanentAttSpeMultipliers;
    public List<float> temporalAttSpeMultipliers;
    public List<int> temporalAttSpeIdentifiers;
    [SerializeField] private float permanentAttSpeFinal;
    [SerializeField] private float temporalAttSpeFinal;


    //We have 2 list of floats, representing the multipliers of damage the pieces of the player has. One is permanent and the one are temporal.
    //Each piece that ads a damage multiplier, also adds their identifier to the list, so we know wich multiplier if from wich piece
    public List<float> permanentDamMultipliers;
    public List<float> temporalDamMultipliers;
    public List<int> temporalDamIdentifiers;
    [SerializeField] private float permanentDamFinal;
    [SerializeField] private float temporalDamFinal;



    private void Awake()
    {
        Instance = this;

        permanentDamRedFinal = 1f;
        temporalDamRedFinal = 1f;

        permanentAttSpeFinal = 0f;
        temporalAttSpeFinal = 0f;

        permanentDamFinal = 0f;
        temporalDamFinal = 0f;

        //All the things of the health bar
        healthBarSL = healthBarGO.GetComponent<Slider>();
        healthText = healthTextGO.GetComponent<TextMeshProUGUI>();
        
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.J))
        {
            takeDamage(20f);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colid = collision.gameObject;

        //If the coliding object is a enemy (7)
        if (collision.gameObject.layer == 7)
        {
            //We take the damage depending of the type of enemy
            takeDamage((int)colid.GetComponent<Enemy>().contactDamage);

            //Then we destroy that enemy
            Destroy(colid);

        }
        // or a enemy bullet (10)
        if (collision.gameObject.layer == 10)
        {
            //We take the damage of the bullet
            takeDamage(colid.GetComponent<Bullet>().damage);

            //Then we destroy the bullet
            Destroy(colid);
        }
    }


    /// <summary>
    /// We clean all the list
    /// </summary>
    public void clearAllList()
    {
        permanentDamRedMultipliers.Clear();
        temporalDamRedMultipliers.Clear();
        temporalDamRedIdentifiers.Clear();

        permanentAttSpeMultipliers.Clear();
        temporalAttSpeMultipliers.Clear();
        temporalAttSpeIdentifiers.Clear();

        permanentDamMultipliers.Clear();
        temporalDamMultipliers.Clear();
        temporalDamIdentifiers.Clear();
}


    /// <summary>
    /// This method is called when the gameplay starts
    /// </summary>
    public void StartGameplay()
    {
        //The current life is set to the maximum life
        lifeCurrent = lifeMax;
        healthBarSL.maxValue = lifeMax;
        ChangeLife(0);

        //We multiply and save all the permanents multipliers of all the stats
        foreach (float mul in permanentDamRedMultipliers)
        {
            permanentDamRedFinal *= (100 - mul) / 100;
        }
        CalculateTempDamRedFinal();

        foreach (float mul in permanentAttSpeMultipliers)
        {
            permanentAttSpeFinal += mul / 100;
        }
        CalculateTempAttSpeFinal();

        foreach (float mul in permanentDamMultipliers)
        {
            permanentDamFinal += mul / 100;
        }
        CalculateTempDamFinal();


    }


    /// <summary>
    /// This method is call when the gameplay ends
    /// </summary>
    public void EndGameplay()
    {
        //We reset all the permanent multipliers of all the stats
        permanentDamRedFinal = 1f;

        permanentAttSpeFinal = 0f;

        permanentDamFinal = 0f;
    }



    #region DamageReduction

    /// <summary>
    /// Add to the player a permanent multiplier on reciving damage
    /// </summary>
    /// <param name="multiplier">The multiplier being aplicated</param>
    public void AddDamRedMultiplier(float multiplier)
    {
        permanentDamRedMultipliers.Add(multiplier);
    }

    /// <summary>
    /// Add to the player a temporal multiplier on reciving damage
    /// </summary>
    /// <param name="multiplier">The multiplier being aplicated</param>
    /// <param name="identifier">The identifier of the piece in the case of being temporal</param>
    public void AddDamRedMultiplier(float multiplier, int identifier)
    {
        temporalDamRedMultipliers.Add(multiplier);
        temporalDamRedIdentifiers.Add(identifier);

        //Recalculate the temporal dam red multipliers final
        CalculateTempDamRedFinal();
    }

    public void RemoveDamRedMultiplier(int identifier)
    {
        if (GameState.Instance.inGame)
        {
            //We search the temporalDefenseMultiplier and eliminate it from the List
            int pos = 0;
            foreach (int iden in temporalDamRedIdentifiers)
            {
                if (iden == identifier)
                {
                    break;
                }
                pos++;
            }

            //We eliminate this identifier first
            temporalDamRedIdentifiers.RemoveAt(pos);

            //And now we eliminate the multiplier in the list with that position on the list
            temporalDamRedMultipliers.RemoveAt(pos);

            //Recalculate the temporal dam red multipliers final
            CalculateTempDamRedFinal();
        }
            
    }

    public void CalculateTempDamRedFinal()
    {
        temporalDamRedFinal = 1f;
        //We multiply and save all the permanents multipliers of all the stats (we invert them first)
        foreach (float mul in temporalDamRedMultipliers)
        {
            temporalDamRedFinal *= (100 - mul) / 100;
        }
    }

    #endregion


    #region AttackSpeed

    public void AddAttSpeMultiplier(float multiplier)
    {
        permanentAttSpeMultipliers.Add(multiplier);
    }

    public void AddAttSpeMultiplier(float multiplier, int identifier)
    {
        temporalAttSpeMultipliers.Add(multiplier);
        temporalAttSpeIdentifiers.Add(identifier);

        //Recalculate the temporal att spe multipliers final
        CalculateTempAttSpeFinal();
    }

    public void RemoveAttSpeMultiplier(int identifier)
    {
        if(GameState.Instance.inGame)
        {
            //We search the temporalAttSpeMultiplier and eliminate it from the List
            int pos = 0;
            foreach (int iden in temporalAttSpeIdentifiers)
            {
                if (iden == identifier)
                {
                    break;
                }
                pos++;
            }

            //We eliminate this identifier first
            temporalAttSpeIdentifiers.RemoveAt(pos);

            //And now we eliminate the multiplier in the list with that position on the list
            temporalAttSpeMultipliers.RemoveAt(pos);

            //Recalculate the temporal att spe multipliers final
            CalculateTempAttSpeFinal();
        }
        
    }

    public void CalculateTempAttSpeFinal()
    {
        temporalAttSpeFinal = 0f;
        //We multiply and save all the permanents multipliers of all the stats (we invert them first)
        foreach (float mul in temporalAttSpeMultipliers)
        {
            temporalAttSpeFinal += mul / 100;
        }

        ApplyAttSpeToPieces();
    }

    /// <summary>
    /// We apply the permanent and temporal Attack Speed to the modifiers of all shooting Pieces.
    /// </summary>
    public void ApplyAttSpeToPieces()
    {
        ICEquipment.Instance.useAllFunctionsMod(1, permanentAttSpeFinal + temporalAttSpeFinal);
    }

    #endregion


    #region DamageMultiplier
    public void AddDamMultiplier(float multiplier)
    {
        permanentDamMultipliers.Add(multiplier);
    }

    public void AddDamMultiplier(float multiplier, int identifier)
    {
        temporalDamMultipliers.Add(multiplier);
        temporalDamIdentifiers.Add(identifier);

        //Recalculate the temporal Dam multipliers final
        CalculateTempDamFinal();
    }

    public void RemoveDamMultiplier(int identifier)
    {
        if (GameState.Instance.inGame)
        {
            //We search the temporalDamMultiplier and eliminate it from the List
            int pos = 0;
            foreach (int iden in temporalDamIdentifiers)
            {
                if (iden == identifier)
                {
                    break;
                }
                pos++;
            }

            //We eliminate this identifier first
            temporalDamIdentifiers.RemoveAt(pos);

            //And now we eliminate the multiplier in the list with that position on the list
            temporalDamMultipliers.RemoveAt(pos);

            //Recalculate the temporal att spe multipliers final
            CalculateTempDamFinal();
        }
     
    }

    public void CalculateTempDamFinal()
    {
        temporalDamFinal = 0f;
        //We multiply and save all the permanents multipliers of all the stats
        foreach (float mul in temporalDamMultipliers)
        {
            //Debug.Log("" + mul);
            temporalDamFinal += mul / 100f;
        }
        //Debug.Log("" + temporalDamFinal);
        ApplyDamToPieces();
    }

    /// <summary>
    /// We apply the permanent and temporal Damage boost to the modifiers of all shooting Pieces.
    /// </summary>
    public void ApplyDamToPieces()
    {
        ICEquipment.Instance.useAllFunctionsMod(2, permanentDamFinal + temporalDamFinal);
    }

    #endregion



    /// <summary>
    /// We call this method when we are gonna take damage
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(float damage)
    {

        //Right now we only have one type of damage

        //We apply the defense multiplier
        float damageTaken = damage * permanentDamRedFinal * temporalDamRedFinal;

        //We activate the beforeHit triggers

        //We take the damage
        ChangeLife(-damageTaken);
        //Debug.Log("Damage taken: " + damageTaken);

        //We activate the afterHit triggers
        ICEquipment.Instance.useAllFunctions(4);

        //And we recalculate the temporal damage reductions
        CalculateTempDamRedFinal();
    }



    /// <summary>
    /// We add or substrac life according of the number pased. Also we refresh all the life attached widgets like lifebar
    /// </summary>
    /// <param name="lifeChanged"></param>
    public void ChangeLife(float lifeChanged)
    {
        lifeCurrent += lifeChanged;

        //Now if the player health is over the maximum, we reduce it to the maximum
        if(lifeCurrent > lifeMax)
        {
            lifeCurrent = lifeMax;
            //Debug.Log("Player OVERHEALED");

        }

        //If the life is 0 or below, we make the death events
        if(lifeCurrent <= 0)
        {
            lifeCurrent = 0f;

            LevelManager.Instance.RestartLevel();
        }

        //Now we change all the widgeds atached to life
        healthBarSL.value = lifeCurrent;
        healthText.text = "" + (int)lifeCurrent;
    }



    


}
