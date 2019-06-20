using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour {
    [SerializeField] float LevelLoadDelay = 0.3f;
    //[SerializeField] float SlowmoFactor = 0.2f;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject canvas;
    bool Unlocked;

    

    private void Update()
    {
        bool temp = true;
        Treasure[] treasures = FindObjectsOfType<Treasure>() as Treasure[];
        foreach (Treasure treasure in treasures)
        {
            if (!treasure.Opened)
            {
                temp = false;
            }
        }
        if (temp)
        {
            UnlockDoor();
        }
    }
    private void Start()
    {

        GetComponent<SpriteRenderer>().sprite = sprites[0];
        Unlocked = false;
    }
    public void UnlockDoor()
    {
        Unlocked = true;
        GetComponent<SpriteRenderer>().sprite = sprites[1];

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Unlocked)
        {
            if (CloudVariables.ImportantValues[1] <= (SceneManager.GetActiveScene().buildIndex - 1))
            {
                CloudVariables.ImportantValues[1] = SceneManager.GetActiveScene().buildIndex - 1;
                Debug.Log("Change hogaya level");
            }
            PlayGamesScript.Instance.SaveData();
            StartCoroutine(LoadNextLevel());
            
        }
        else
            canvas.SetActive(true);
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas.SetActive(false);
    }
    IEnumerator LoadNextLevel()
    {
        //Time.timeScale = SlowmoFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        //Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 5)
            PlayGamesScript.Instance.UnlockAchievement(GPGSIds.achievement_standard_achievement);
        SceneManager.LoadScene(currentSceneIndex + 1);

    }
}
