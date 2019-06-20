using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }
    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameModeScene()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel2Scene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel3Scene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel4Scene()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadLearnModScene()
    {
        SceneManager.LoadScene(9);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadInstructionScene()
    {
        SceneManager.LoadScene(7);
    }
    public void ExitScreen()
    {
        Application.Quit();
    }

    public void LevelChangerScene()
    {
        SceneManager.LoadScene(10);
    }

   
}
