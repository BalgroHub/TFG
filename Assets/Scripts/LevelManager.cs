using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<string> levelScenes = new List<string>();
    public int actualLevelIndex;
    public string actualLevelName;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            NextLevel();
        }
    }

    private void Awake()
    {
        Instance = this;
        actualLevelIndex = 0;
        actualLevelName = SceneManager.GetActiveScene().name;
    }

    public void NextLevel()
    {
        //We get the actual level index
        int loop = 0;
        foreach(string level in levelScenes)
        {
            if(level.Equals(SceneManager.GetActiveScene().name))
            {
                actualLevelIndex = loop;
                break;
            }
            loop++;
        }

        SceneManager.LoadScene(levelScenes[actualLevelIndex + 1]);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(levelScenes[0]);
    }
}
