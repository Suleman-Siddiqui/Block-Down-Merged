using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : MonoBehaviour
{

    public GameObject newTileSpawnHolder;
    public GameObject initialzerGameObj;
    public GameObject gridTilesParent;
    public GameObject GameOverTextOb;
    public GameObject ReplayBtnObj;
    public Text ScoreShowUiText;

    public InitializerScripts InitializerScripts;
    public static GamePlayManager GM_Instance;
    public GameObject[] LastTilesOFGrid;

    void CheckInstance()
    {
        if (GM_Instance == null)
            GM_Instance = this;
        else
            Destroy(GM_Instance);
    }

    void Awake()
    {
        CheckInstance();
        AppController.IsClickSeriveOn = true;
    }
   

    public GameObject GetTopActiveTile()
    {
        if(newTileSpawnHolder.transform.childCount>0)
        {
            return newTileSpawnHolder.transform.GetChild(0).gameObject;
        }

        return null;
    }

    public GameObject GetBottomlastActiveTile_FromTileHolder()
    {
        if (newTileSpawnHolder.transform.childCount > 3)
        {
            return newTileSpawnHolder.transform.GetChild(newTileSpawnHolder.transform.childCount-1).gameObject;
        }

        return null;
    }

    public void MoveSecondTileToFirstPositon()
    {
        newTileSpawnHolder.transform.GetChild(0).GetComponent<TileScripts>().MoveToFirstPostion(InitializerScripts.playerTileSpawninglocalPosArr[0]);
        newTileSpawnHolder.transform.GetChild(0).GetComponent<TileScripts>().ScaleResetEffect();


        newTileSpawnHolder.transform.GetChild(1).transform.localPosition = InitializerScripts.playerTileSpawninglocalPosArr[1];
        newTileSpawnHolder.transform.GetChild(1).GetComponent<TileScripts>().SetScaleValue(InitializerScripts.playerTileScaleArry[1]);
        // new tile also spawn
        InitializerScripts.SpawnPlayerTile();

    }

    public void MoveClickTileToTopUpPos(GameObject clickTileObj)
    {
        clickTileObj.transform.SetParent(newTileSpawnHolder.transform);
        clickTileObj.GetComponent<TileScripts>().MoveToTopFirstPos(clickTileObj.GetComponent<TileScripts>().moveToUpPos);
        newTileSpawnHolder.transform.GetChild(0).GetComponent<TileScripts>().MoveToTopFirstPoslocal(new Vector3(0, 0.5f, 0));
    }

    public bool CheckedTileHolder_ChildGreaterThan3()
    {
        if(newTileSpawnHolder.transform.childCount>3)
        {
            return true;
        }
        return false;
    }

    public void SetNewTile_NumberAfterMerged(GameObject DestinationCardObj, GameObject MergedTile)
    {
        int mergedTileNumber = MergedTile.GetComponent<TileScripts>().TileNumber;
        Destroy(MergedTile);
        int nextNumber = mergedTileNumber * 2;
        DestinationCardObj.GetComponent<TileScripts>().TileNumber = nextNumber;
        DestinationCardObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nextNumber.ToString();
        DestinationCardObj.transform.GetComponent<SpriteRenderer>().color=InitializerScripts.GetTileColorAtNumberWise(nextNumber);

        ScoreCountFun(mergedTileNumber);
        // again check to merged 
        bool isFound =  DestinationCardObj.transform.parent.GetComponent<GridTileScripts>().CheckBottomNeighbout_ToGetFatchedCard();
        if(!isFound)
        {
            GamePlayManager.GM_Instance.GameOverChecked();
        }

    }

    public bool GameOverChecked()
    {
        if(gridTilesParent.transform.childCount>0)
        {
            int countercheck = 0;
            for (int i = 0; i < gridTilesParent.transform.childCount; i++)
            {
               if(gridTilesParent.transform.GetChild(i).transform.childCount>0)
                {
                    countercheck++;
                }
            }
            if(countercheck== gridTilesParent.transform.childCount)
            {
                 print("GAME OVER ");
                 GameOverTextOb.SetActive(true);
                 ReplayBtnObj.SetActive(true);
                AppController.IsClickSeriveOn = false;
                return true;
            }
        }

        return true;
    }

    void ScoreCountFun(int tileNumber)
    {
        tileNumber += tileNumber;
        ScoreShowUiText.text = tileNumber.ToString();

    }

    public GameObject GetGridEmptyTile()
    {

        for (int i = 0; i < gridTilesParent.transform.childCount; i++)
        {
            if(gridTilesParent.transform.GetChild(i).transform.childCount==0)
            {
                return gridTilesParent.transform.GetChild(i).gameObject;
            }
        }  
        return null;
    }




}