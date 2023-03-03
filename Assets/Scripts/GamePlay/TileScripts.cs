using System;
using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class TileScripts : MonoBehaviour
    {
        public int TileNumber;
        [SerializeField] private float downSpeed;
        private GridTile currentGridTile;
        
        public void MoveDown()
        {
            var bottom_GT = currentGridTile.GetNextBottom_GT();
            if(bottom_GT==null) return;
            print("move down");
            transform.DOMove(bottom_GT.transform.position, downSpeed).OnComplete(()=>
            {
                SetCurrentGridTile(bottom_GT);
                MoveDown();
            });
        }

        public void SetCurrentGridTile(GridTile gridTile)
        {
            currentGridTile = gridTile;
        }
        
        
        
        private void OnMouseDown()
        {
            if(transform.parent.GetComponent<GridTile>() && AppController.IsClickSeriveOn)
            {
                GridTile gdTile = transform.parent.GetComponent<GridTile>();
                if (gdTile.CheckTopNeigibourTileNull())
                {
                    GamePlayManager.GM_Instance.MoveClickTileToTopUpPos(this.gameObject);
                }
            }
        }
        
        void MovtToParentTilePos()
        {
            iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1f, "speed", 20f));
            iTween.MoveTo(gameObject, iTween.Hash("x", transform.parent.position.x, "y",transform.parent.position.y,
                "speed", 20f, "easeType", "easeInOutQuad","oncomplete", "CheckMyParentToSelf_FatchedFound"));
        }

        void CheckMyParentToSelf_FatchedFound()
        {
            bool IsfoundMerged = transform.parent.GetComponent<GridTile>().CheckBottomNeighbout_ToGetFatchedCard();
            if (!IsfoundMerged)
            {
                GamePlayManager.GM_Instance.GameOverChecked();
            }
        }

        public GameObject FatchedCardPosObj;

        public void MoveToFatchedPos()
        {
        
            iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1f,"speed", 15));
            iTween.MoveTo(gameObject, iTween.Hash("x", FatchedCardPosObj.transform.position.x, "y", FatchedCardPosObj.transform.position.y,"delay",0.1f,
                "speed", 15, "easeType", "easeInOutQuad", "oncomplete", "DestorySelfAndInitNext"));
        }

        void DestorySelfAndInitNext()
        {
            GamePlayManager.GM_Instance.SetNewTile_NumberAfterMerged(FatchedCardPosObj, this.gameObject);
        }
        
        
        

    }
}
