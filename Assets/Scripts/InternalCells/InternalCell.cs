using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InternalCell : MonoBehaviour
{
    

    public GameObject player;

    public Piece[,] cellMatrix;
    public int maxX = 9;
    public int maxY = 5;
    public int minX = 0;
    public int minY = 0;
    public int sizeX = -1;
    public int sizeY = -1;
    private Vector2Int posStartGrid;
    public GameObject[] allPieces;

    //Gameobjects transform for creating the grid dimensions
    public Transform downLeft;
    public Transform upRight;

    //List of Pieces to spawn manually (Piece Spawner)
    public List<GameObject> pieceSpawnerListGameObjects;
    public List<Vector2Int> pieceSpawnerListPositions;


    private Grid thisGrid;
    public GameObject singleCellPrefab;
    public Transform gridCompleteTransform;

    public GameObject gridDetectorGO;
    private BoxCollider gridDetectorBC;





    protected virtual void Awake()
    {
        thisGrid = GetComponent<Grid>();
    }

    void Start()
    {
        gridDetectorBC = gridDetectorGO.GetComponent<BoxCollider>();

        DefineGridLimits();

        //Create the piece matrix
        cellMatrix = new Piece[sizeX, sizeY];

        //Spawn the pieces in their positions
        pieceSpawner();

        CreateVisualGrid();

        
    }

    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.L))
        {
            //printMatrix();
            useAllFunctions(1);
        }
        */
    }

    /// <summary>
    /// Take the transform, get the in grid position and calculating the tall and lenght, and the position of the grid
    /// </summary>
    private void DefineGridLimits()
    {
        //Debug.Log( "downLeft gets the world cell: " + thisGrid.WorldToCell(downLeft.position));
        //Debug.Log(" upRight gets the world cell: " + thisGrid.WorldToCell(upRight.position));

        maxX = thisGrid.WorldToCell(upRight.position).x;
        maxY = thisGrid.WorldToCell(upRight.position).y;

        minX = thisGrid.WorldToCell(downLeft.position).x;
        minY = thisGrid.WorldToCell(downLeft.position).y;

        posStartGrid = new Vector2Int(minX, minY);
        sizeX = (maxX - minX) + 1;
        sizeY = (maxY - minY) + 1;

        //Now we change the gridDetector to match the grid size
        gridDetectorGO.transform.localPosition = new Vector3(sizeX/2f, sizeY/2f, 0);
        gridDetectorBC.size   = new Vector3(sizeX, sizeY, 2);
    }

    /// <summary>
    /// With the prefab singleCell, we create the cell visually one by one
    /// </summary>
    private void CreateVisualGrid()
    {
        for(int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                GameObject temporalSingleCell = Instantiate(singleCellPrefab,gridCompleteTransform.position, Quaternion.identity, gridCompleteTransform);

                //The position depend of the scale of the grid too
                Vector3 gridScale = this.gameObject.transform.localScale;
                temporalSingleCell.transform.Translate((i + 0.5f)*gridScale.x, (j + 0.5f)*gridScale.y, 0f);
            }
        }
    }

    //When droping a piece in a cell, we fill the internal cell with that piece
    //Check if that piece can fit in that cell
    public bool dropPieceIn(int x, int y, Piece piec)
    {
        bool fit = true;

        //Go over each miniblock
        //If a miniblock is over another piece or out the grid, the piece can not be placed
        foreach (Vector2Int vec in piec.blocks)
        {
            //If its out
            if (vec.x + x < minX || vec.y + y < minY || vec.x + x > maxX || vec.y + y > maxY)
            {
                fit = false;
                Debug.Log("Se sale del grid");
                break;
            }

            //If is touching another piece
            if (cellMatrix[vec.x + x, vec.y + y] != null)
            {
                fit = false;
                Debug.Log("Se solapa");
                break;
            }

            
        }

        //If all miniblock can be placed, we go over every miniblock again and place it
        if (fit == true)
        foreach (Vector2Int vec in piec.blocks)
        {
            cellMatrix[vec.x + x, vec.y + y] = piec;
        }
        else
        {
            Debug.Log("No se ha podido poner la pieza");
            return false;
        }
        return true;

    }


    public bool deletePieceIn(int x, int y)
    {
        //We save a reference to the temporal piece in the parameter positons
        Piece temporal = cellMatrix[x, y];

        //We see the miniblocks of the temporal piece
        foreach (Vector2Int vec in temporal.blocks)
        {
            //We look to the position of each miniblock + actual position, and if its the same as the piece we put it in null
            if(cellMatrix[vec.x + x, vec.y + y] == temporal)
            {
                cellMatrix[vec.x + x, vec.y + y] = null;
            }
        }


        return true;
    }

    /// <summary>
    /// Spawn the initial pieces we want in the grid
    /// </summary>
    public void pieceSpawner()
    {
        Grid mygrid = GetComponent<Grid>();
        //We manually place the 4 pieces in the grid
        //We dont need to create the last pos because the grid is the parent, its start runs before

        int loop = 0;
        //We go though each gameobject in pieceSpawnerListGameObjects, creating them in the internal and external grid in the positions in pieceSpawnerListPositions
        foreach (GameObject go in pieceSpawnerListGameObjects)
        {
            Piece pie = go.GetComponent<Piece>();
            MyCellTest cuce = go.GetComponent<MyCellTest>();

            foreach(Vector2Int mb in pie.blocks)
            {
                cellMatrix[pieceSpawnerListPositions[loop].x + mb.x, pieceSpawnerListPositions[loop].y + mb.y] = pie;
            }

            go.transform.position = mygrid.GetCellCenterWorld(new Vector3Int(pieceSpawnerListPositions[loop].x, pieceSpawnerListPositions[loop].y, 0));

            //We set their currentCell to this one
            cuce.SetParentGrid(this.gameObject);

            loop++;
        }

    }

    /// <summary>
    /// Go through all the piecen in the grid, aplying the selected function to all of them
    /// 1.Activate
    /// 2.Deactivate
    /// 3.TypeActions
    /// 4.AfterHitTrigger
    /// </summary>
    public void useAllFunctions(int mode)
    {
        List<int> used = new List<int>();

        //First we deactivate all the pieces
        /*
        if(mode == 1)
        for (int r = 0; r < allPieces.Length; r++)
        {
            allPieces[r].GetComponent<Piece>().setActive(false); 
        }
        */

        //Now we activate everyone that is on the grid
        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if(cellMatrix[i, j] != null && !used.Contains(cellMatrix[i, j].identifier) )
                {
                    //cellMatrix[i, j]
                    used.Add(cellMatrix[i, j].identifier);
                    //Debug.Log(cellMatrix[i, j].identifier);
                    if(mode == 1)
                    {
                        cellMatrix[i, j].setActive(true);
                    }
                    if(mode == 2)
                    {
                        cellMatrix[i, j].setActive(false);
                    }
                    if(mode == 3)
                    {
                        //Now we call the function, and the piece will do whatever they are capable to do
                        cellMatrix[i, j].TypeActions();
                    }
                    if(mode == 4)
                    {
                        //We call the AfterHitTriger of all pieces
                        cellMatrix[i, j].AfterHitTrigger();
                    }
                    

                }
                
            }
        }
    }


    /// <summary>
    /// Types of modes:
    /// 1.ModifyAttSpe
    /// 2.ModifyDamage
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="multiplier"></param>
    public void useAllFunctionsMod(int mode, float multiplier)
    {
        List<int> used = new List<int>();

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if (cellMatrix[i, j] != null && !used.Contains(cellMatrix[i, j].identifier))
                {
                    used.Add(cellMatrix[i, j].identifier);
                    
                    if (mode == 1)
                    {
                        cellMatrix[i, j].ModifyAttSpe(multiplier);
                    }
                    if (mode == 2)
                    {
                        cellMatrix[i, j].ModifyDamage(multiplier);
                    }

                }

            }
        }
    }

    public void ClearAllStats()
    {
        PlayerStats.Instance.clearAllList();
    }


    /// <summary>
    /// We search in all the Equipment pieces if there is a weapon
    /// </summary>
    /// <returns></returns>
    public bool SeeIfWeapon()
    {
        bool ret = false;

        List<int> used = new List<int>();

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if (cellMatrix[i, j] != null && !used.Contains(cellMatrix[i, j].identifier))
                {
                    if (cellMatrix[i, j].name.Equals("Pistol") || cellMatrix[i, j].name.Equals("Machinegun"))
                    {
                        ret = true;
                    }
                }

            }
        }

        return ret;
    }


}
