using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMultiResolution : MonoBehaviour {
    public CanvasScaler canvas1;
    public Camera gmPlayCamera;

    public bool IsMainScreen;
    public bool IsGamePlayScreen;

    public GameObject PrivacyBtn;
    public GameObject SwiptBtn;
	// Use this for initialization
	void Start ()
    {
        float currentRatio = (float)Screen.width / (float)Screen.height;
        //currentRatio = currentRatio * 100;
        currentRatio = Mathf.Round(currentRatio * 100f) / 100f; 
        Debug.Log(currentRatio);

        if (IsMainScreen)
             MainSetting(currentRatio);
        if (IsGamePlayScreen)
            GamePlaySetting(currentRatio);
    } 


    void MainSetting(float currentRatio)
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        //|| currentRatio == 0.75f || currentRatio == 0.67f || currentRatio == 0.62f || currentRatio == 0.59f
        if (currentRatio == 0.63f  || currentRatio == 0.6f || currentRatio == 0.75f || currentRatio == 0.62f)
        {
            if (canvas1 != null)
                canvas1.matchWidthOrHeight = 1;
        }
    }

    void GamePlaySetting(float currentRatio)
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //|| currentRatio == 0.75f || currentRatio == 0.67f || currentRatio == 0.62f || currentRatio == 0.59f
        if (currentRatio == 0.47f || currentRatio == 0.46f)
        {
            gmPlayCamera.orthographicSize = 7.6f;
            canvas1.matchWidthOrHeight = 0;
        }
        if ( currentRatio == 0.46f)
        {
            gmPlayCamera.orthographicSize = 8.1f;
            canvas1.matchWidthOrHeight = 0;
        }
        if (currentRatio == 0.5f)
        {
            gmPlayCamera.orthographicSize = 7.4f;
            canvas1.matchWidthOrHeight = 0;
        }

        if (currentRatio == 0.62f  )
        {
            if (canvas1 != null)
                canvas1.matchWidthOrHeight = 1;
        }
        if ( currentRatio == 0.63f || currentRatio == 0.6f)
        {
            if (canvas1 != null)
                canvas1.matchWidthOrHeight = 1;
        }

        if (currentRatio == 0.75f)
        {
            if (canvas1 != null)
                canvas1.matchWidthOrHeight = 1;
          //  SwiptBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(832,233);
        }
    }

}
