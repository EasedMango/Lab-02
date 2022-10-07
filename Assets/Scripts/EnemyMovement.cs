using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> points = new();

    private Queue<GameObject> spawnQueue;
    public bool canSpawn = true;

    public float spawnTime = 1f;



    public float trackDist;

    public GameObject enemyPrefab;
    public GameObject enemyTypeOne;


    void QueueEnemies(GameObject enemy, int amt)
    {
        for (int i = 0; i < amt; i++)
            spawnQueue.Enqueue(enemy);

    }


    // Start is called before the first frame update
    void Start()
    {
        spawnQueue = new Queue<GameObject>();
        for (int i = 1; i < points.Count; i++)
        {
            trackDist += Vector3.Distance(points[i].transform.position, points[i - 1].transform.position);
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            QueueEnemies(enemyPrefab, 6);

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            QueueEnemies(enemyTypeOne, 4);
        }

        if (RhythmManager.instance.OnBeat(2))
        {
            if (canSpawn)
            {
                Spawn();
                canSpawn = false;
            }

        }
        else
        {
            canSpawn = true;
        }


    }



    private void Spawn()
    {
       // print(spawnQueue.Count);
        if (spawnQueue.Count > 0)
        {
            GameObject clone = Instantiate(spawnQueue.Dequeue(), transform);
            clone.GetComponent<Enemy>().points = points;
        }
        
    }





}
