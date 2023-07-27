using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    public List<GameObject> spawns = new List<GameObject>();
    public List<float> spawnTime = new List<float>();

    public bool notEnded;

    private void Awake()
    {
        Instance = this;
        notEnded = true;
    }

    private void Update()
    {

        if(spawns.Count == 0 && notEnded)
        {
            if(GameObject.Find("Enemies").transform.childCount == 0)
            {
                //We win this level and pass to the next
                LevelManager.Instance.NextLevel();
                notEnded = false;
            }
        }
    }

    /// <summary>
    /// We start the corrutine of each spawn, with its individual spawnTime
    /// </summary>
    public void StartAllCountdowns()
    {
        int loop = 0;
        foreach (GameObject spawner in spawns)
        {
            StartCoroutine(AllCountdowns( spawner, spawnTime[loop] ));
            loop++;
        }
    }


    /// <summary>
    /// Starts a countdown, and wen it finishes the spawner is activated and starts spawning
    /// </summary>
    /// <param name="spawn"></param>
    /// <param name="countdown"></param>
    /// <returns></returns>
    IEnumerator AllCountdowns(GameObject spawn, float countdown)
    {
        yield return new WaitForSecondsRealtime(countdown);
        spawn.SetActive(true);
        spawn.GetComponent<Spawner>().StartSpawning();

    }


    /// <summary>
    /// We search how many enemies of wich type are in each spawner
    /// </summary>
    /// <returns>1.Fast, 2.Big</returns>
    public Vector2 CountEnemiesType()
    {
        Vector2 enemiesVector = Vector2.zero;

        foreach (GameObject spawner in spawns)
        {
            foreach (GameObject enemy in spawner.GetComponent<Spawner>().enemies)
            {

                if (enemy.name.Equals("EnemyBig"))
                {
                    enemiesVector += new Vector2(0, 1);
                }
                else
                {
                    enemiesVector += new Vector2(1, 0);
                }

            }
        }


        return enemiesVector;
    }

}
