using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class InitializerScripts : MonoBehaviour
{
   

    public int[] GirdTileNumber;
    public Color[] TileColorNumberWise;
    [Header("Grid ITems")]
    public GameObject gridMainParent;
    public GameObject gridTilePrefab;
    public float gridRowXOffset;
    public float gridColoumYOffset;
    public int Row,Colom;
    public float gridTParentTileScale;
    public GameObject playerTileObject;
   
    private void Start()
    {
        GenerateGrid(gridTilePrefab, Row, Colom, gridRowXOffset, gridColoumYOffset);
      //  SpawnPlayerTile();
        Invoke("SpawnPlayerTile",1f);
    }

     //Grid Init
    private void GenerateGrid(GameObject TilePlacer, int Pr_numOfRow, int Pr_numOfCol,float xIncrementalValue, float yIncrementalValue)
    {
        int nameconuter = 0;
        float xOffset = 0;
        float YOffset = 0;
        for (int row = 0; row < Pr_numOfRow; row++)
        {
            xOffset = 0;
            for (int col = 0; col < Pr_numOfCol; col++)
            {
                GameObject currentTile = Instantiate(TilePlacer);
                currentTile.transform.SetParent(gridMainParent.transform);
                currentTile.transform.localPosition = new Vector3(currentTile.transform.position.x + xOffset, currentTile.transform.position.y + YOffset, 0);
                currentTile.transform.name = nameconuter.ToString();
                currentTile.GetComponent<SpriteRenderer>().enabled = true;
                xOffset += xIncrementalValue;
                currentTile.GetComponent<NeigbourFoundGridTile>().CheckNeighbour(nameconuter, Pr_numOfRow, Pr_numOfCol);
                nameconuter++;

            }
            YOffset += yIncrementalValue;
        }

        gridMainParent.transform.localScale =
            new Vector3(gridTParentTileScale, gridTParentTileScale, gridTParentTileScale);
    }

    private  void SpawnPlayerTile()
    {
        var TileObj = Instantiate(playerTileObject) ;
      
        var gridTileMyParent = GetNewTileStartingParen();
        TileObj.transform.SetParent(gridTileMyParent.transform);
        TileObj.transform.localPosition = Vector3.zero;
        TileObj.transform.localScale = Vector3.one;
        
        // number property
        var r = Random.Range(0, 2);
        TileObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = GirdTileNumber[r].ToString();
        TileObj.GetComponent<SpriteRenderer>().color = TileColorNumberWise[r];
        var tileScript = TileObj.GetComponent<TileScripts>();
        tileScript.TileNumber=GirdTileNumber[r];
        tileScript.SetCurrentGridTile(gridTileMyParent);
        tileScript.MoveDown();
    }

    private GridTile GetNewTileStartingParen()
    {
        return gridMainParent.transform.GetChild(Colom*Row- ((int)Colom/ 2)-1).GetComponent<GridTile>();
    }

    public Color GetTileColorAtNumberWise(int tileNumber)
    {
        return tileNumber switch
        {
            2 => TileColorNumberWise[0],
            4 => TileColorNumberWise[1],
            8 => TileColorNumberWise[2],
            16 => TileColorNumberWise[3],
            32 => TileColorNumberWise[4],
            64 => TileColorNumberWise[5],
            128 => TileColorNumberWise[6],
            256 => TileColorNumberWise[7],
            512 => TileColorNumberWise[8],
            1024 => TileColorNumberWise[9],
            2048 => TileColorNumberWise[10],
            _ => new Color()
        };
    }
}
