using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
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
        public TileScripts playerTileObject;
   
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
                    var currentTile = Instantiate(TilePlacer);
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

        public  void SpawnPlayerTile()
        {
            var gridTileMyParent = GetNewTileStartingParen();
            if(gridTileMyParent.transform.childCount>0)return;
            
            var TileObj = Instantiate(playerTileObject) ;
            TileObj.transform.SetParent(gridTileMyParent.transform);
            var r = Random.Range(0, 2);
            var tileScript = TileObj.GetComponent<TileScripts>();
           
            tileScript.SetLocalPos();
            tileScript.SetLocalScale();
            tileScript.SetTileNumberAndColor(GirdTileNumber[r],TileColorNumberWise[r]);   
            tileScript.SetCurrentGridTile(gridTileMyParent);
            tileScript.MoveDown();
        }

        private GridTile GetNewTileStartingParen()
        {
            return  gridMainParent.transform.GetChild(Colom*Row- ((int)Colom/ 2)-1).gameObject.GetComponent<GridTile>() ;
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
}
