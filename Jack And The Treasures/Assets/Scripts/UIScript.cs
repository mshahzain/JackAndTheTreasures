using UnityEngine;
using UnityEngine.UI;



public class UIScript : MonoBehaviour
{
    public Text[] ValueTextArray;

    public void UpdateAll()
    {
        for (int i = 0; i < CloudVariables.ImportantValues.Length; i++)
            ValueTextArray[i].text = CloudVariables.ImportantValues[i].ToString();
    }

    public void Save()
    {
        PlayGamesScript.Instance.SaveData();
    }

    public void Increment(int index)
    {
        CloudVariables.ImportantValues[index]++;
        ValueTextArray[index].text = CloudVariables.ImportantValues[index].ToString();
    }

    public void ShowAchievements()
    {
        PlayGamesScript.Instance.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        PlayGamesScript.Instance.ShowLeaderboardsUI();
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://divertidoentertain.wixsite.com/tempsite/privacy-policy");
    }
    /*

    
    public void Increment()
    {
        PlayGamesScript.Instance.IncrementAchievement(GPGSIds.achievement_incremental_achievement, 5);
    }

    public void Unlock()
    {
        PlayGamesScript.Instance.UnlockAchievement(GPGSIds.achievement_standard_achievement);
    }

    public void ShowAchievements()
    {
        PlayGamesScript.Instance.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        PlayGamesScript.ShowLeaderboardsUI();
    }

    
    */
}