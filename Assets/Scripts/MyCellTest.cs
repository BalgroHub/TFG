using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCellTest : MonoBehaviour
{
    //Grid and InternalCellScript of the parent
    [SerializeField]
    private Grid myGrid;
    private InternalCell internalCellCp;

    //Variables of the selected piece
    private bool selected = false;
    private Piece piec;
    private Vector3 lastPos;
    private Vector3 lastPosLocal;
    private string lastSortingLayer;


    void Start()
    {
        TryGetComponent(out piec);

        lastPos = transform.position;
        lastPosLocal = transform.localPosition;
        lastSortingLayer = GetComponent<SpriteRenderer>().sortingLayerName;

        SetParentGrid(transform.parent.parent.gameObject);
    }

    public void SetParentGrid(GameObject go)
    {
        myGrid = go.GetComponent<Grid>();
        internalCellCp = go.GetComponent<InternalCell>();
    }

    void Update()
    {
        dragBoxes();
    }
    

    public Vector2Int gridToMatrixCoord(int x, int y)
    {
        return new Vector2Int(x, y);
    }

    public void setLastPos(float x, float y)
    {
        lastPos = new Vector3(x, y, 0);
    }


    /// <summary>
    /// Holding the left click, the boxes are drag across the screen
    /// </summary>
    private void dragBoxes()
    {
        //Look the cell of the cursor (PosCusorPixelCamera->PosCursorUnits->PosCursorCell)
        Vector3 vposCursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //While being held, the object follows the cursor
        if(Input.GetMouseButton(0) && selected)
        {
            transform.position = new Vector3(vposCursor.x, vposCursor.y, 0);

        }
  
    }

    private void OnMouseDown()
    {
        bool onEquipment = false;
        if(transform.parent.parent.gameObject.name.Equals("Grid"))
        {
            onEquipment = true;
        }

        //We only select pieces from the active grid (the normal grid if its collapsed, or the reserve grid if its expanded)
        if(GridReserveHUD.Instance.collapsed == onEquipment )
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected = true;

                //This pos is its lastPos
                lastPos = transform.position;
                lastPosLocal = transform.localPosition;

                //Its position is deleted from the internal grid (lastPos)
                Vector3Int anteriorposCuadraoEnGrid = myGrid.WorldToCell(lastPos);
                internalCellCp.deletePieceIn(anteriorposCuadraoEnGrid.x, anteriorposCuadraoEnGrid.y);


                //We tell the gridDetectors to start listening
                GridMaster.Instance.SetGridDetectorsActive(true);

                //If its from the reserve grid, we collapse it
                if (!GridReserveHUD.Instance.collapsed)
                {
                    GridReserveHUD.Instance.CollapseGrid();
                }

                //We put that piece in a hight sorting layer
                GetComponent<SpriteRenderer>().sortingLayerName = "PieceGrabbed";

                DescriptionManager.Instance.ShowText( GetComponent<Piece>().identifier );
                

            }
        }
        

        

    }

    private void OnMouseUp()
    {
        //We only get pieces of the active grid
        //if (GridReserveHUD.Instance.collapsed == onEquipment)
        if(selected)
        {
            GameObject futuregridGO = GridMaster.Instance.currentCursorGrid;
            Grid futuregrid = futuregridGO.GetComponent<Grid>();
            InternalCell futuregridIC = futuregridGO.GetComponent<InternalCell>();

            //On drop, we calculate the cell on wich it is
            Vector3 vposCursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vposCelda = futuregrid.WorldToCell(vposCursor);
            Vector3Int posCuadraoEnGrid = futuregrid.WorldToCell(transform.position);

            //We look if it can be placed in this position
            bool returned = futuregridIC.dropPieceIn(posCuadraoEnGrid.x, posCuadraoEnGrid.y, piec);

            //If it can be placed, its snaps to the position and its laspos its updated
            if (returned)
            {
                transform.position = futuregrid.GetCellCenterWorld(posCuadraoEnGrid);
                lastPos = transform.position;
                lastPosLocal = transform.localPosition;


                //Change its parents
                transform.parent = GridMaster.Instance.currentParent;
                SetParentGrid(GridMaster.Instance.currentCursorGrid);

                //And change its short layer
                if (transform.parent.parent.gameObject.name.Equals("Grid"))
                {
                    GetComponent<SpriteRenderer>().sortingLayerName = "PiecesEquipment";
                    lastSortingLayer = "PiecesEquipment";
                    this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "PiecesEquipment";
                }
                else
                {
                    GetComponent<SpriteRenderer>().sortingLayerName = "PiecesReserve";
                    lastSortingLayer = "PiecesReserve";
                    this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "PiecesReserve";
                }

            }
            //If it can NOT be placed, the piece goes to their last pos internaly and visually
            else
            {
                //transform.position = lastPos;
                transform.localPosition = lastPosLocal;
                Vector3Int posCuadraoEnGrid2 = myGrid.WorldToCell(transform.position);
                internalCellCp.dropPieceIn(posCuadraoEnGrid2.x, posCuadraoEnGrid2.y, piec);

                GetComponent<SpriteRenderer>().sortingLayerName = lastSortingLayer;

            }

            //The piece is deselected
            selected = false;


            //We tell the gridDetectors to stop listening
            GridMaster.Instance.SetGridDetectorsActive(false);

            //We see if there is a weapon in the grid
            if(ICEquipment.Instance.SeeIfWeapon())
            {

                //We activate the button and set thte text innactive
                //GameObject.Find("TextNoWeapon").SetActive(false);
                ButtonGameplay.Instance.SetReady(true);
            }
            else
            {

                //We deactivate the button
                //GameObject.Find("TextNoWeapon").SetActive(true);
                ButtonGameplay.Instance.SetReady(false);
            }
            
            
        }

        
    }
}
