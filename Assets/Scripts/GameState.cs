using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceTypes
{
    Shooting,
    DefenseBuff,
    DamageBuff,
    Resource
}


public class GameState : MonoBehaviour
{
    //public GameObject internalCellGo;
    //private InternalCell internalCellCp;

    public static GameState Instance { get; private set; }

    public Transform posGameplay;
    public Transform posEquipment;
    public float lerpDuration;
    private bool isCameraMoving = false;

    public float snapEnd;

    private Vector3 currentPos;

    public bool inGame = false;

    

    /// <summary>
    /// Is called when the class is created (before the start method)
    /// </summary>
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Instance = this;
    }

    void Start()
    {
        // internalCellCp = internalCellGo.GetComponent<InternalCell>();
        
    }


    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && !isCameraMoving)
        {
            changeGamestate();
        }
    }

    /// <summary>
    /// Change the gamestate depending on wich mode we are now
    /// </summary>
    public void changeGamestate()
    {
        if (inGame)
        {
            goToEquipment();
            ICEquipment.Instance.useAllFunctions(2);
            ICEquipment.Instance.ClearAllStats();
            PlayerStats.Instance.EndGameplay();

            InterfaceMovement.Instance.ChangeVisibility();
        }
        else
        {
            goToGameplay();
            ICEquipment.Instance.useAllFunctions(1);
            ICEquipment.Instance.useAllFunctions(3);
            PlayerStats.Instance.StartGameplay();

            InterfaceMovement.Instance.ChangeVisibility();
            SpawnerManager.Instance.StartAllCountdowns();
        }
    }

    //Moves the camera to the gameplay and activates the gameplay
    public void goToGameplay()
    {
        inGame = true;
        StartCoroutine(MyLerp(posGameplay, lerpDuration));
    }

    //Moves the cameta to the equipment and deactivates the gameplay
    public void goToEquipment()
    {
        inGame = false;
        StartCoroutine(MyLerp(posEquipment, lerpDuration));
    }


    /// <summary>
    /// Moves this gameobject smoothly to the end Transform in the duration passed
    /// </summary>
    /// <param name="end"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator MyLerp(Transform end, float duration)
    {
        isCameraMoving = true;

        float timeElapsed = 0f;
        currentPos = new Vector3(0, 0, -10);


        while(timeElapsed <= duration)
        {
            if (timeElapsed / duration >= snapEnd) timeElapsed = duration;

            //transform.localPosition = Vector3.Lerp(transform.localPosition, end.localPosition, timeElapsed  / duration );
            transform.localPosition = Vector3.Lerp(transform.localPosition, end.localPosition, Mathf.Pow(timeElapsed, 2f) / Mathf.Pow(duration, 2f));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        isCameraMoving = false;
    }
}
