using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class ScenePersist : MonoBehaviour
    {

    [SerializeField] int liveForScene;
    [SerializeField] Text treasureTotal;
    [SerializeField] Text treasureOpened;
    Treasure[] treasures;


    private void Awake()
        {
            ScenePersist[] SPs = FindObjectsOfType<ScenePersist>();
            liveForScene = SceneManager.GetActiveScene().buildIndex;
            if (SPs.Length > 1)
            {
                if (SPs[0].liveForScene == SPs[1].liveForScene)
                {
                    print("Destroying duplicate ScenePersist");
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
            DontDestroyOnLoad(gameObject);
            
    }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            
    }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex != liveForScene)
            {
                print("Destroying absolete ScenePersist");
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    private void TreasureCheck()
    {
        treasures = FindObjectsOfType<Treasure>() as Treasure[];
        treasureTotal.text = treasures.Length.ToString();

        int temp = 0;
        foreach (Treasure treasure in treasures)
        {
            if (treasure.Opened)
            {
                temp++;
            }
        }
        treasureOpened.text = temp.ToString();

    }

    private void Update()
    {
        TreasureCheck();
    }
}