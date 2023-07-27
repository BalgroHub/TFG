using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceMovement : MonoBehaviour
{
    public static InterfaceMovement Instance { get; private set; }

    public List<GameObject> listGameplay = new List<GameObject>();
    public List<GameObject> listEquipment = new List<GameObject>();

    public bool gameplayHidden;
    public bool equipmentHidden;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        gameplayHidden = true;
        equipmentHidden = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            ChangeVisibility();
        }
    }

    public void ChangeVisibility()
    {
        
        foreach (GameObject go in listGameplay)
        {
            if (go != null)
            {
                
                if (gameplayHidden)
                {
                    go.SetActive(true);
                    
                }
                else
                {
                    go.SetActive(false);
                    
                }
            }
        }
        gameplayHidden = !gameplayHidden;

        foreach (GameObject go in listEquipment)
        {
            if(equipmentHidden)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        equipmentHidden = !equipmentHidden;

    }
}
