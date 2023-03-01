using UnityEngine;
using System.Collections;

public class AppController : MonoBehaviour
{

    public static bool IsClickSeriveOn = true;

    public static bool IsBoombsSericeOn = false;
    public static bool IsHamberServiceOn = false;

    public static bool IsFatchingStop = false;
    public static bool IsGameOverFound = false;

    public static bool IsVideoWatchedCompleted = false;

    public static GameObject HemeberKilTile;
    public static GameObject BommberKilltile;



    public static float Musicvalue = 1;
    public static float Soundvalue1 = 1;
    public enum PlayMode
    {
        MULTI_PLAYER,AI
    }
    public enum PlayerTurn
    {
        PLAYER_1, PLAYER_2
    };

    public static PlayMode GamePlayMode=PlayMode.AI;
    public static PlayerTurn Player_Turn = PlayerTurn.PLAYER_1;
    public static int Grid_Size_RowAndcolumn=0;
    public static int optBackgroundNumber=0;
    public static int optGotiNumber=0;


    public static string P1InputNameSt="";
    public static string P2InputNameSt="";




    public static float AdTimer = 0.0f;

    public static int gamePlayCount = 0;


    public static void ButtonsOff(GameObject[] offButton , bool trueOrFalse)
	{
		for (int a=0; a<offButton.Length; a++) {
			offButton[a].SetActive(trueOrFalse);
			
		}
	}
	
	
	public static void CollidersOff(GameObject[] offCollider , bool trueOrFalse)
	{
		for (int a=0; a<offCollider.Length; a++) {
			offCollider[a].GetComponent<Collider2D>().enabled = trueOrFalse;
		}
	}


	//game variables
	public static ArrayList movingOption;
	//public static Coin coinClicked;
    public static string yellowTag = "Player Yellow";
    public static string blueTag = "Player Blue";
    public static string TagToCompare;

    public static string CurrenTPlayerTurnBaseSaveTag ;

   // public static Point pointClicked;
	public static bool isTurn;
	public static int cntrySelected;

	public static string player1Name;
	public static string player2Name;
	//ai variables

	
	public static bool isPlayingWithAi;

    //safi new variables

    public static ArrayList pointsWithBlueNeighbour;
    public static ArrayList aiKillingStartingPoints, aiKillingEndingPoints;
    public static ArrayList aiyellowPointsWithEmptyNeighbours,emptyNeighbours;

    //ends here
	public static int yellowKilledCounter;
	public static int blueKilledCounter;

	public static bool isGameOver;
	public static bool isGamePaused;
	public static bool isExitDialoge;
    public static bool isMusic = true;
    public static bool isSound = false;
   
    public static bool chartBoostShow = false;
    public static bool chartBoostMainMenu = false;
    public static bool chartBoost = false;
	public static bool isScroller  = false;
	
	public static bool _isDialogue  = false;
	public static bool isCamPic=false;
	public static bool isDialogue  = false;
	public static bool showPurchaseDialogue  = false;
	
	public static int isPurchased  = 0;
	
	public static int gamePlayed  = 1;
	public static int toPlay  = 1;
	
	public static bool isEating = true;
	public static int inAppCoinsAdded;
    
    public static int alreadyRated = 0;

    public static float getMWidth(float pWidth )  {
		float w_  = ((pWidth * 100f) / 720f);
		return ((w_ /100.00f) *Screen.width);
	}
	
	public static float getMHeight(float pHeight )  {
		float h_  = ((pHeight * 100f) /1280f);
		return ((h_ /100.0f) * Screen.height);
	}
	
	public static float getRWidth(float pWidth ) {
		float w_  = ((pWidth * 100f) / Screen.width);
		return ((w_ /100.0f) * 720f);
		
	}
	
	public static float getRHeight(float pHeight ) {
		float h_  = ((pHeight * 100f) / Screen.height);
		return ((h_ /100.0f) * 1280f);
	}
}
