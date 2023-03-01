using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public Text CoinsDisplayText;
    public Text PreviousTargetDisplayTextt;
    public Text NewTargetDisplayText;
    public Text GO_ScoreDisplayText;
    public Image targetfillAmountImage;

    public bool IsGamePlayScreen;




    void Start()
    {
        if(IsGamePlayScreen)
        {
            SetPrefsValueAndShowOnText();
        }
    }


    [Header("Audio Screming")]

    public AudioClip clickaudio;
    public AudioClip gotidropaudio;
    public AudioClip gotireachaudio;
    public AudioClip avatarSelectionaudio;

    public AudioSource audioSrce;
    
    public void LoadChoiseScene(string Pr_SceneName)
    {
        SceneManager.LoadScene(Pr_SceneName);
    }

    public void DialogeOpenFun(GameObject db)
    {
        db.SetActive(true);
    }

    public void DialogeCloseFun(GameObject db)
    {
        db.SetActive(false);
    }

    public void ReplayGame()
    {
        AppController.IsClickSeriveOn = true;
        SceneManager.LoadScene("GamePlay");
    }
    public void LoadMainScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
    





    //==============================================================================

    public void ClickBtnAudioFun()
    {
        if(AppController.isMusic)
        {
            audioSrce.PlayOneShot(clickaudio);
        }
    }

    public void GotiDropaudioFun()
    {
        if (AppController.isMusic)
        {
            audioSrce.PlayOneShot(gotidropaudio);
        }
    }

    public void GotireachudioFun()
    {
        if (AppController.isMusic)
        {
            audioSrce.PlayOneShot(gotireachaudio);
        }
    }

    public void AvatarselectaudioFun()
    {
        if (AppController.isMusic)
        {
            audioSrce.PlayOneShot(avatarSelectionaudio);
        }
    }

    //public void SpinStartingFun()
    //{
    //    Spin.IsSpinStart = true;
    //}

    
    #region Score Target FillAmount and TrophiesCount

    int currentTargetValue;
    int targetCountViaScore;

    int divider_DifferenceValue;
    int tempValue = 0;

    public void TargetCount_ViaScore(int pr_cardMergedNumber)
    {
        targetCountViaScore += pr_cardMergedNumber;
        PreviousTargetDisplayTextt.text = targetCountViaScore.ToString();
       

        PlayerPrefs.SetInt("Prv_Terget", targetCountViaScore);
        FillAmountSetValue(pr_cardMergedNumber);
        IncreaseTarget();
    }

    void SetPrefsValueAndShowOnText()
    {
        // if(PlayerPrefs.HasKey("Prev_Terget"))
        if (!PlayerPrefs.HasKey("Current_Terget"))
                PlayerPrefs.SetInt("Current_Terget", 10);

        NewTargetDisplayText.text = PlayerPrefs.GetInt("Current_Terget").ToString();
        PreviousTargetDisplayTextt.text= PlayerPrefs.GetInt("Prv_Terget").ToString();
        CoinsDisplayText.text = PlayerPrefs.GetInt("CoinsValue").ToString();

        currentTargetValue = PlayerPrefs.GetInt("Current_Terget");
        targetCountViaScore = PlayerPrefs.GetInt("Prv_Terget");


        divider_DifferenceValue = currentTargetValue - targetCountViaScore;
        FillAmountSetValue(0);
    }

    void IncreaseTarget()
    {
        if(targetCountViaScore >= currentTargetValue)
        {
            CountCoinsFun(currentTargetValue);
            currentTargetValue += currentTargetValue;
            PlayerPrefs.SetInt("Current_Terget", currentTargetValue);

            NewTargetDisplayText.text = PlayerPrefs.GetInt("Current_Terget").ToString();
            targetfillAmountImage.fillAmount = 0;
            divider_DifferenceValue = currentTargetValue - targetCountViaScore;
            tempValue = 0;
        }
    }

    void FillAmountSetValue(int Value)
    {
        tempValue += Value;
        float value = ((float)tempValue / divider_DifferenceValue/* currentTargetValue*/);
        targetfillAmountImage.fillAmount = value;
    }
   
    void CountCoinsFun(int Value)
    {
        // 50% add of current achived target value 
        PlayerPrefs.SetInt("CoinsValue", PlayerPrefs.GetInt("CoinsValue")+ (Value / 2));
        CoinsDisplayText.text = PlayerPrefs.GetInt("CoinsValue").ToString();
        GO_ScoreDisplayText.text = PlayerPrefs.GetInt("CoinsValue").ToString();
    }
   
    #endregion


}
