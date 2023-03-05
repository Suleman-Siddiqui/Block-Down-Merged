using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class TileScripts : MonoBehaviour
    {
        public int TileNumber;
        [SerializeField] private Text TileNumberText;
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] private float downSpeed;
        [SerializeField] private float rightSpeed;
        
        private GridTile currentGridTile;
        private Vector2 _mouseDownPosition;
        private Vector2 _mouseUpPosition;
        // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
        private const float MAX_SWIPE_TIME = 0.5f; 
        // Factor of the screen width that we consider a swipe
        // 0.17 works well for portrait mode 16:9 phone
        private const float MIN_SWIPE_DISTANCE = .05f;
        private Vector2 _startPos;
        private float _startTime;
        private bool IsSwipeFound;
        private bool IsFoundEndLocatedPoint;
        
        public void Update()
        {
            if(!IsFoundEndLocatedPoint)
                SwipeInput();
            
        }
        
        /*
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
*/

        #region SWIPE MOVE AND AUTO DOWN

        private bool isMousedown = false;
        private Vector2 mouseTouchPosPrevious;
        
        private void SwipeInput()
        {
            if (Input.GetMouseButtonUp(0) && isMousedown)
            {
                isMousedown = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                mouseTouchPosPrevious = Input.mousePosition;
                _startPos = new Vector2(mouseTouchPosPrevious.x/(float)Screen.width, mouseTouchPosPrevious.y/(float)Screen.width);
                _startTime = Time.time;
                isMousedown = true;
            }

            if (!Input.GetMouseButton(0) || !isMousedown) return;
            var mouseTouchUpPos = Input.mousePosition;
            var endPos = new Vector2(mouseTouchUpPos.x/(float)Screen.width, mouseTouchUpPos.y/(float)Screen.width);
            var swipe = new Vector2(endPos.x - _startPos.x, endPos.y - _startPos.y);
            SwipeShift(swipe);

        }

        private int swipeCount = 0;
        private void SwipeShift(Vector2 swipe)
        {
           
            if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                return;
            print(swipe.magnitude);
            if(swipeCount>0) return;;
            if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) 
            {
                // Horizontal swipe
                if (swipe.x > 0) {
                    MoveRight();
                }
                else {
                    MoveLeft();
                }
            }
            else { // Vertical swipe
                if (swipe.y > 0) {
                    print("Swipe Up ");
                }
                else {
                    print("Swipe down ");
                }
            }

            mouseTouchPosPrevious =new Vector2(swipe.x+MIN_SWIPE_DISTANCE,swipe.y+MIN_SWIPE_DISTANCE);
            if (!(Time.time > _startTime + MAX_SWIPE_TIME)) return;
             _startTime = Time.time + MAX_SWIPE_TIME;
            swipeCount = 0;

        }
       
        public void MoveDown()
        {
            var bottom_GT = currentGridTile.GetNextBottom_GT();
            if (bottom_GT == null)
            {
                IsFoundEndLocatedPoint = true;
                GamePlayManager.GM_Instance.InitializerScripts.SpawnPlayerTile();
            }
            if(bottom_GT==null) return;
            SetCurrentGridTile(bottom_GT);
            transform.DOMove(bottom_GT.transform.position, downSpeed).OnComplete(MoveDown).SetDelay(1f);
        }

        private void MoveLeft()
        {
            var leftGt = currentGridTile.GetLeft_GT();
            if(leftGt==null) return;
            DOTween.KillAll();
            swipeCount = 1;
            SetCurrentGridTile(leftGt);
            transform.position = leftGt.transform.position; //DOMove(leftGt.transform.position, rightSpeed);
        }

        private void MoveRight()
        {
            var rightGt = currentGridTile.GetRight_GT();
            if(rightGt==null) return;
            swipeCount = 1;
            SetCurrentGridTile(rightGt);
            DOTween.KillAll();
            transform.position = rightGt.transform.position; // DOMove(rightGt.transform.position, rightSpeed);
        }
        
        public void SetCurrentGridTile(GridTile gridTile)
        {
            currentGridTile = gridTile;
            transform.SetParent(currentGridTile.transform);
        }


        #endregion

        public void SetLocalPos()
        {
            transform.localPosition = Vector3.zero;
        }

        public void SetLocalScale()
        {
            transform.localScale = Vector3.one;
        }

     
        public void SetTileNumberAndColor(int tileNumber,Color color)
        {
            TileNumberText.text = tileNumber.ToString();
            SpriteRenderer.color = color;
            TileNumber = tileNumber;
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
