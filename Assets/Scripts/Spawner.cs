using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public float spawnInterval;
    public Vector2 spawnDirection;
    public Transform enemiesTransform;

    private void Awake()
    {
        enemiesTransform = transform.parent.parent.Find("Enemies");
        Vector3 directionVector3 = transform.Find("Direction").position;
        spawnDirection = new Vector2( directionVector3.x - transform.position.x, directionVector3.y - transform.position.y );
        
    }

    private void Update()
    {
    }

    /// <summary>
    /// The spawn starts spawning enemies each interval of time
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (enemies.Count > 0)
        {
            //We spawn the next gameobject on the list
            GameObject spawnedEnemy = Instantiate(enemies[0], transform.position, Quaternion.identity, enemiesTransform);

            //Assing the velocity
            spawnedEnemy.GetComponent<Rigidbody2D>().velocity = spawnDirection;

            //And quit it from the list
            enemies.RemoveAt(0);

            yield return new WaitForSeconds(spawnInterval);
        }

        //When we are finish, we tell the Spawn Manager to eliminate from the list this spawner, and then we autodestruct
        SpawnerManager.Instance.spawns.Remove(gameObject);
        Destroy(gameObject);
    }

    
}
