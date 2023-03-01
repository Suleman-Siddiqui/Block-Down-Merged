using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScripts : MonoBehaviour
{

    public int TileNumber;
    public Vector3 moveToUpPos;

    void OnMouseDown()
    {
        Debug.Log("name  Tile :: " + name);

        if(transform.parent.GetComponent<GridTileScripts>() && AppController.IsClickSeriveOn)
        {
            GridTileScripts gdTileScript = transform.parent.GetComponent<GridTileScripts>();
            if (gdTileScript.CheckTopNeigibourTileNull())
            {
                GamePlayManager.GM_Instance.MoveClickTileToTopUpPos(this.gameObject);
            }
        }
    }

    public void MoveToFirstPostion(Vector3 firstPos)
    {
       
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1f, "speed", 5f));
        iTween.MoveTo(gameObject, iTween.Hash("x", firstPos.x, "y",firstPos.y, "islocal",true,"speed", 5f, "easeType", "easeInOutQuad"));
    }

    public void MoveToTopFirstPos(Vector3 TopUpPos)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x",0,"y", TopUpPos.y, "time", 0.3f,"easeType", "easeInOutQuad"));
    }

    public void MoveToTopFirstPoslocal(Vector3 TopUpPos)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "y", TopUpPos.y,"islocal",true, "time", 0.3f, "easeType", "easeInOutQuad"));
    }
    //===================================================================

    public void MoveToNextGridPost()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1.3F, "y", 1.3f, "speed", 10F));
        iTween.MoveTo(gameObject, iTween.Hash("x",transform.parent.position.x ,"speed", 10,"easeType", "easeInOutQuad","oncomplete", "MovtToParentTilePos"));

    }

    void MovtToParentTilePos()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1f, "speed", 20f));
        iTween.MoveTo(gameObject, iTween.Hash("x", transform.parent.position.x, "y",transform.parent.position.y,
                                            "speed", 20f, "easeType", "easeInOutQuad","oncomplete", "CheckMyParentToSelf_FatchedFound"));
    }

    void CheckMyParentToSelf_FatchedFound()
    {
        bool IsfoundMerged = transform.parent.GetComponent<GridTileScripts>().CheckBottomNeighbout_ToGetFatchedCard();
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


    public void ScaleResetEffect()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1f, "time", 0.2f));
    }

    public void SetScaleValue(Vector3 ScaleValue)
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", ScaleValue.x, "y", ScaleValue.y, "time", 0.5f));
    }


}
