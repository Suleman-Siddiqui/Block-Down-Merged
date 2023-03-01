using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileScripts : MonoBehaviour
{

    void OnMouseDown()

    {
        if(AppController.IsClickSeriveOn)
        {
            if (CheckMyBottomFil())
            {
                if (GamePlayManager.GM_Instance.CheckedTileHolder_ChildGreaterThan3())
                {
                    MoveTwoTile_EquallyWithDifferentPos();
                }
                else
                {
                    print("idher ata");
                    GameObject tileObj = GamePlayManager.GM_Instance.GetTopActiveTile();
                    tileObj.transform.SetParent(GetMyLastBottomEptyTile().transform);
                    tileObj.GetComponent<TileScripts>().MoveToNextGridPost();
                    GamePlayManager.GM_Instance.MoveSecondTileToFirstPositon();
                }

            }
            else
            {
                if (transform.childCount == 0)
                {
                    if (GamePlayManager.GM_Instance.CheckedTileHolder_ChildGreaterThan3())
                    {
                        MoveTwoTile_EquallyWithDifferentPos();

                    }
                    else
                    {
                        GameObject tileObj = GamePlayManager.GM_Instance.GetTopActiveTile();
                        tileObj.transform.SetParent(transform);
                        tileObj.GetComponent<TileScripts>().MoveToNextGridPost();
                        GamePlayManager.GM_Instance.MoveSecondTileToFirstPositon();
                    }
                }

            }

        }

    }

    bool CheckMyBottomFil()
    {
        if (bottomNeighbour != null && bottomNeighbour.transform.childCount == 0)
        {
            return true;
        }
        return false;
    }

    GameObject GetMyLastBottomEptyTile()
    {
        GameObject TempObj =null;
        if (bottomNeighbour != null && bottomNeighbour.transform.childCount == 0)
        {
            GameObject BotmNibour = bottomNeighbour.transform.gameObject;
            int counterB = 0;
            while (BotmNibour != null && BotmNibour.transform.childCount == 0 && counterB < 6)
            {
                TempObj = BotmNibour.transform.gameObject;

                counterB++;
                if (BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour != null
                            && BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour.transform.childCount == 0)
                {
                    BotmNibour = BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour.gameObject;
                }
            }
        }
        else
        {
            if(transform.childCount == 0)
            {
                TempObj = this.gameObject;
            }
        }

        return TempObj;
    }

    void MoveTwoTile_EquallyWithDifferentPos()
    {
        GameObject tileHolderChildNumber3 = GamePlayManager.GM_Instance.GetBottomlastActiveTile_FromTileHolder();
        tileHolderChildNumber3.transform.SetParent(GetMyLastBottomEptyTile().transform);
        tileHolderChildNumber3.GetComponent<TileScripts>().MoveToNextGridPost();

        GameObject tileHolderChildNumber0 = GamePlayManager.GM_Instance.newTileSpawnHolder.transform.GetChild(0).gameObject;
        GameObject emptyGridTile = GamePlayManager.GM_Instance.GetGridEmptyTile();

        if(emptyGridTile!=null)
        {
            tileHolderChildNumber0.transform.SetParent(emptyGridTile.transform);
            tileHolderChildNumber0.GetComponent<TileScripts>().MoveToNextGridPost();
        }
        GamePlayManager.GM_Instance.MoveSecondTileToFirstPositon();
    }

    #region ++++++++++++ NEIGHBOUR FINDING MODULE  +++++++++++++++ 
    // important dont delete 
    [Header("==Neighbour Check Values===")]
    public int left = -1;
    public int right = -1;
    public int up = -1;
    public int bottom = -1;

    public int topRight = -1;
    public int topLeft = -1;
    public int botRight = -1;
    public int botLeft = -1;

    // horizental verticals 
    public GridTileScripts leftNeighbour = null;
    public GridTileScripts rightNeighbour = null;

    public GridTileScripts topNeighbour = null;
    public GridTileScripts bottomNeighbour = null;
    // Diagonal 
    public GridTileScripts bottomLeftNeighbour = null;
    public GridTileScripts bottomRightNeighbour = null;

    public GridTileScripts topLeft_Neighbour = null;
    public GridTileScripts topRight_Neighbour = null;


    public void FindNeihbourForMe(int countNumberOfBlock, int numOfRows, int numOfCols)
    {

        if ((countNumberOfBlock + 1) % numOfCols != 0) //numofcols=4
            right = countNumberOfBlock + 1;// right 

        if (countNumberOfBlock % numOfCols != 0)
            left = countNumberOfBlock - 1; //left

        if ((countNumberOfBlock - numOfCols) > -1)
            bottom = countNumberOfBlock - numOfCols;  //bottom

        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)))
        {
            up = countNumberOfBlock + numOfCols;  // top 
        }
        //=======Top Diagonals ==============================================================
        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock + 1) % numOfCols != 0)
            topRight = countNumberOfBlock + numOfCols + 1;   // top Diagonals right 

        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock) % numOfCols != 0)
            topLeft = countNumberOfBlock + numOfCols - 1; //  top Diagonals left


        //=========== Bottom Diagonals ==================================================
        if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock + 1) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
            botRight = (countNumberOfBlock - numOfCols) + 1;   // Bottom  Diagonals right 

        if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
            botLeft = (countNumberOfBlock - numOfCols) - 1;//  top Diagonals left

        Invoke("SetNighbour_ObjectValue", 1);
    }

    void SetNighbour_ObjectValue()
    {
        leftNeighbour = GetNeighbour(left);
        rightNeighbour = GetNeighbour(right);

        topNeighbour = GetNeighbour(up);
        bottomNeighbour = GetNeighbour(bottom);
        // Diagonal 
        bottomLeftNeighbour = GetNeighbour(botLeft);
        bottomRightNeighbour = GetNeighbour(botRight);

        topLeft_Neighbour = GetNeighbour(topLeft);
        topRight_Neighbour = GetNeighbour(topRight);
    }

    GridTileScripts GetNeighbour(int childIndex)
    {
        GameObject parentObject_Grid = this.gameObject.transform.parent.gameObject;
        GridTileScripts NeighbourOf_Block = null;
        if (childIndex >= 0)
        {
            NeighbourOf_Block = parentObject_Grid.gameObject.transform.GetChild(childIndex).gameObject.GetComponent<GridTileScripts>();
        }
        return NeighbourOf_Block;
    }

    #endregion

    #region Check MergedCard MOVE TO FACTHED POS Func 

    public bool CheckBottomNeighbout_ToGetFatchedCard()
    {
        bool IsFound = false;
        if (IsFound_Bottom())
        {
            MoveToFatchedCardFunbottomPos(bottomNeighbour.transform.GetChild(0).gameObject,transform.GetChild(0).gameObject);

            IsFound = true;
        }
        return IsFound;
    }
    
    public bool CheckTopNeigibourTileNull()
    {
        if (topNeighbour != null)
        {
            if (!IsChildFound(topNeighbour.transform.gameObject))
            {
               return true;
            }
        }
        return false;
    }

    bool IsFound_Bottom()
    {
        if (bottomNeighbour != null)
        {
            if (IsChildFound(bottomNeighbour.transform.gameObject))
            {
                if (IsbottomTileNumberSame(bottomNeighbour.transform.gameObject))
                {
                    return true;
                }

            }
        }
        return false;
    }

    bool IsChildFound(GameObject Obj)
    {
        if (Obj.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }

    bool IsbottomTileNumberSame(GameObject NeighbourObj)
    {
        if (transform.childCount > 0)
        {
            int MyChildCardRank = transform.GetChild(0).GetComponent<TileScripts>().TileNumber;
            int MyNeighbour_ChildCardRank = NeighbourObj.transform.GetChild(0).GetComponent<TileScripts>().TileNumber;
            if (MyChildCardRank == MyNeighbour_ChildCardRank)
            {
                return true;
            }
        }
        return false;
    }

    void MoveToFatchedCardFunbottomPos(GameObject pr_CardDestinationObj, GameObject fatchedCard)
    {
       // fatchedCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        fatchedCard.GetComponent<TileScripts>().FatchedCardPosObj = pr_CardDestinationObj;
        fatchedCard.GetComponent<TileScripts>().MoveToFatchedPos();
    }
   
    #endregion

}
