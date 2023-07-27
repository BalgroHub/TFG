using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaster : MonoBehaviour
{

    public static GridMaster Instance { get; private set; }

    public GameObject gridDetectorEquimpentGO;
    public GameObject gridDetectorReserveGO;
    private GridDetector gridDetectorEquimpentSC;
    private GridDetector gridDetectorReserveSC;

    public GameObject currentCursorGrid;

    public Transform currentParent;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        gridDetectorEquimpentSC = gridDetectorEquimpentGO.GetComponent<GridDetector>();
        gridDetectorReserveSC = gridDetectorReserveGO.GetComponent<GridDetector>();
    }

    public void SetCurrentCursorGrid(GameObject go)
    {
        currentCursorGrid = go;
    }


    public void SetGridDetectorsActive(bool bo)
    {
        gridDetectorEquimpentSC.SetActive(bo);
        gridDetectorReserveSC.SetActive(bo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
