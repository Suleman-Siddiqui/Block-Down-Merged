using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class GridTile : MonoBehaviour
    {
        [SerializeField] private NeigbourFoundGridTile neighbourFound_GT;
     
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
           return neighbourFound_GT.bottomNeighbour != null && neighbourFound_GT.bottomNeighbour.transform.childCount == 0;
        }

        public GridTile GetNextBottom_GT()
        {
            return CheckMyBottomFil() ? neighbourFound_GT.bottomNeighbour : null;
        }

        public GridTile GetRight_GT()
        {
            return neighbourFound_GT.rightNeighbour != null && neighbourFound_GT.rightNeighbour.transform.childCount == 0 ? neighbourFound_GT.rightNeighbour : null;
        }
       
        public GridTile GetLeft_GT()
        {
            return neighbourFound_GT.leftNeighbour != null && neighbourFound_GT.leftNeighbour.transform.childCount == 0 ? neighbourFound_GT.leftNeighbour : null;
        }
        
        private bool IsFoundBottomTileNumberSame()
        {
            return CheckMyBottomFil() && IsbottomTileNumberSame(neighbourFound_GT.bottomNeighbour.transform.gameObject);
        }
        
        // Check MergedCard ::  MOVE TO FACTHED POS Func 
        public bool CheckBottomNeighbout_ToGetFatchedCard()
        {
            bool IsFound = false;
            if (!IsFoundBottomTileNumberSame()) return IsFound;
            MoveToFatchedCardFunbottomPos( neighbourFound_GT.bottomNeighbour.transform.GetChild(0).gameObject,transform.GetChild(0).gameObject);

            IsFound = true;
            return IsFound;
        }
    
        public bool CheckTopNeigibourTileNull()
        {
            if (neighbourFound_GT.topNeighbour != null)
            {
                if (!IsChildFound(neighbourFound_GT.topNeighbour.transform.gameObject))
                {
                    return true;
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
   

    }
}
