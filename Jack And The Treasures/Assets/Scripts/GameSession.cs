using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Monetization;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text Livestext ;
    [SerializeField] public Text scoretext ;
    [SerializeField] GameObject AdCanvas;
    [SerializeField] GameObject AdFailedCanvas;
    [SerializeField] Button button;
    [SerializeField] GameObject ConfirmationScreen;
    
    private void Awake()
    {
        Time.timeScale = 1f;
        AdCanvas.SetActive(false);
        AdFailedCanvas.SetActive(false);
        ConfirmationScreen.SetActive(false);
        button.interactable = true;
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        
        if (numGameSessions > 1) 
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, false);
        }
        
        Livestext.text = playerLives.ToString();
        scoretext.text = score.ToString();
        AdCanvas.SetActive(false);


    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            playerLives--;
            Livestext.text = playerLives.ToString();
            OpenCanvas();
            //ResetGameSession();
        }
    }

    public void OnClose()
    {
        ResetGameSession();
        AdCanvas.SetActive(false);
        AdFailedCanvas.SetActive(false);

    }

    private void OpenCanvas()
    {
        AdCanvas.SetActive(true);
        button.interactable = false;
        FindObjectOfType<Player>().isCanvasOpened = true;
        

    }

    public void OpenConfirmationScreen()
    {
        ConfirmationScreen.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<Player>().isCanvasOpened = true;
        button.interactable = false;

    }

    public void CloseConfirmationScreen()
    {
        ConfirmationScreen.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<Player>().isCanvasOpened = false;
        button.interactable = true;

    }

    public void GiveLife()
    {
        playerLives++;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Livestext.text = playerLives.ToString();
        AdCanvas.SetActive(false);


    }


    public void AddToScore(int PointsToAdd)
    {
        score += PointsToAdd;
        scoretext.text = score.ToString();
        //UIScript.Instance.GetPoint(PointsToAdd);
        if (score > CloudVariables.ImportantValues[0])
        {
            CloudVariables.ImportantValues[0] = score;
            PlayGamesScript.Instance.AddScoreToLeaderboard(GPGSIds.leaderboard_coins_collected, score);
        }

    }
    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Livestext.text = playerLives.ToString();
    }

    public void LoadMenuScreen()
    {
        ResetGameSession();
        
    }
    private void ResetGameSession()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }
    private void Update()
    {
        
    }
    

    public void ProcessAdFailure()
    {
        AdFailedCanvas.SetActive(true);
    }

    public string placementId = "rewardedVideo";
    //private Button adButton;
    //private GameSession gameSession;

#if UNITY_IOS
       private string gameId = "3036930";
#elif UNITY_ANDROID
    private string gameId = "3036931";
#endif

    public void ShowAd()
    {
    // StartCoroutine(ShowAdWhenReady());
        if (Monetization.IsReady(placementId))
        {
            ShowAdCallbacks options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult;
            ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
            ad.Show(options);
        }

        else
        {
            ProcessAdFailure();
        }
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            button.interactable = true;
            FindObjectOfType<Player>().isCanvasOpened = false;
            GiveLife();
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            ProcessAdFailure();
        }
    }

}