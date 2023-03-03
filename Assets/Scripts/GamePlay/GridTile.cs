using System;
using UnityEngine;

namespace GamePlay
{
    public class GridTile : MonoBehaviour
    {
        [SerializeField] private NeightFound_GridTile NeightFoundGridTile;
        /*
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
                       
                        }
                    }

                }

            }

        }
*/

      
        

        private bool CheckMyBottomFil()
       {
           return NeightFoundGridTile.bottomNeighbour != null && NeightFoundGridTile.bottomNeighbour.transform.childCount == 0;
       }
       
       // Check MergedCard ::  MOVE TO FACTHED POS Func 
        public bool CheckBottomNeighbout_ToGetFatchedCard()
        {
            bool IsFound = false;
            if (!IsFound_Bottom()) return IsFound;
            MoveToFatchedCardFunbottomPos( NeightFoundGridTile.bottomNeighbour.transform.GetChild(0).gameObject,transform.GetChild(0).gameObject);

            IsFound = true;
            return IsFound;
        }
    
        public bool CheckTopNeigibourTileNull()
        {
            if (NeightFoundGridTile.topNeighbour != null)
            {
                if (!IsChildFound(NeightFoundGridTile.topNeighbour.transform.gameObject))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFound_Bottom()
        {
            return CheckMyBottomFil() && IsbottomTileNumberSame(NeightFoundGridTile.bottomNeighbour.transform.gameObject);
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
   

    }
}
