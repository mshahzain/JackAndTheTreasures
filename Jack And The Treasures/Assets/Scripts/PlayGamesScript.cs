﻿using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour
{
    public GameObject tutorialCanvas;

    public static PlayGamesScript Instance { get; private set; }
   
    const string SAVE_NAME = "Tutorial";
    bool isSaving;
    bool isCloudDataLoaded = false;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        //setting default value, if the game is played for the first time
        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, string.Empty);
        //tells us if it's the first time that this game has been launched after install - 0 = no, 1 = yes 
        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
            tutorialCanvas.SetActive(true);

        }



        LoadLocal(); //we want to load local data first because loading from cloud can take quite a while, if user progresses while using local data, it will all
                     //sync in our comparating loop in StringToGameData(string, string)

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {
        //when authentication process is done (successfuly or not), we load cloud data
        Social.localUser.Authenticate(success => { LoadData(); });
    }

    #region Saved Games
    //making a string out of game data (highscores...)
    string GameDataToString()
    {
        return JsonUtil.CollectionToJsonString(CloudVariables.ImportantValues, "myKey");
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string cloudData, string localData)
    {
        if (cloudData == string.Empty)
        {
            StringToGameData(localData);
            isCloudDataLoaded = true;
            return;
        }
        int[] cloudArray = JsonUtil.JsonStringToArray(cloudData, "myKey", str => int.Parse(str));

        if (localData == string.Empty)
        {
            CloudVariables.ImportantValues = cloudArray;
            PlayerPrefs.SetString(SAVE_NAME, cloudData);
            isCloudDataLoaded = true;
            return;
        }
        int[] localArray = JsonUtil.JsonStringToArray(localData, "myKey", str => int.Parse(str));

        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            for (int i = 0; i < cloudArray.Length; i++)
                if (cloudArray[i] > localArray[i]) //cloud save is more up to date
                {
                    //set local save to be equal to the cloud save
                    PlayerPrefs.SetString(SAVE_NAME, cloudData);
                }
        }
        //if it's not the first time, start comparing
        else
        {
            for (int i = 0; i < cloudArray.Length; i++)
                //comparing integers, if one int has higher score in it than the other, we update it
                if (localArray[i] > cloudArray[i])
                {
                    //update the cloud save, first set CloudVariables to be equal to localSave
                    CloudVariables.ImportantValues = localArray;
                    isCloudDataLoaded = true;
                    //saving the updated CloudVariables to the cloud
                    SaveData();
                    return;
                }
        }
        //if the code above doesn't trigger return and the code below executes,
        //cloud save and local save are identical, so we can load either one
        CloudVariables.ImportantValues = cloudArray;
        isCloudDataLoaded = true;
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        if (localData != string.Empty)
            CloudVariables.ImportantValues = JsonUtil.JsonStringToArray(localData, "myKey",
                                                                        str => int.Parse(str));
    }

    //used for loading data from the cloud or locally
    public void LoadData()
    {
        //basically if we're connected to the internet, do everything on the cloud
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
            Debug.Log("Google play working");
        }
        //this will basically only run in Unity Editor, as on device,
        //localUser will be authenticated even if he's not connected to the internet (if the player is using GPG)
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {
        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }
        //same as in LoadData
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            Debug.Log("It's saving on cloud");
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {
            //decoding byte data into string
            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            //parsing
            int[] originalArray = JsonUtil.JsonStringToArray(originalStr, "myKey", str => int.Parse(str));
            int[] unmergedArray = JsonUtil.JsonStringToArray(unmergedStr, "myKey", str => int.Parse(str));

            for (int i = 0; i < originalArray.Length; i++)
            {
                //if original score is greater than unmerged
                if (originalArray[i] > unmergedArray[i])
                {
                    resolver.ChooseMetadata(original);
                    return;
                }
                //else (unmerged score is greater than original)
                else if (unmergedArray[i] > originalArray[i])
                {
                    resolver.ChooseMetadata(unmerged);
                    return;
                }
            }
            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!isSaving)
                LoadGame(game);
            //if we're SAVING game data
            else
                SaveGame(game);
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //the same code that is in else statements in LoadData() and SaveData()
        else
        {
            if (!isSaving)
                LoadLocal();
            else
                SaveLocal();
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
        Debug.Log("PlayGames wala load chal raha hai");
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)
        PlayerPrefs.SetString(SAVE_NAME, stringToSave);

        //encoding to byte array
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        //uploading data to the cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
        Debug.Log("PlayGames wala save chal raha hai");
    }

    //callback for ReadBinaryData
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
                //in such case, we want to assign default value to our string
                cloudDataString = string.Empty;
            //otherwise take the byte[] of data and encode it to string
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            //getting local data (if we've never played before on this device, localData is already
            //string.Empty, so there's no need for checking as with cloudDataString)
            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            //this method will compare cloud and local data
            StringToGameData(cloudDataString, localDataString);
        }
    }

    //callback for CommitUpdate
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }
    #endregion /Saved Games

#region Achievements

public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public void ShowAchievementsUI()
    {
        //Debug.Log("Dikhaao");
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements

    #region Leaderboards
    public void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

}