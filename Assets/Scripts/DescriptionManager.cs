using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager Instance { get; private set; }

    public List<GameObject> listGO = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }

    private void Start()
    {

        int children = transform.childCount;
        for(int i = 0; i < children; i++)
        {
            listGO.Add(transform.GetChild(i).gameObject);
        }
    }

    public void ShowText(int identifier)
    {
        int newIdentifier = identifier - 1;
        int loop = 0;
        //We deactivate all the text from others pieces
        foreach(GameObject go in listGO)
        {
            if(loop != newIdentifier)
            {
                go.SetActive(false);
            }
            loop++;
        }

        listGO[newIdentifier].SetActive(true);

    }
}
