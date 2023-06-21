using Assets.Scripts.Dudes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float timeBetweenSpawns;
        [SerializeField] private float bossLevelTimeDelay;
        [SerializeField] private DudeData dude;
        [SerializeField] private int bossLevelIncrement = 6;
        private int nextBossLevel = 6;
        private float nextSpawnTime = 0f;

        private void Start()
        {
            nextSpawnTime = timeBetweenSpawns;
        }

        // Update is called once per frame
        void Update()
        {
            if (dude.IsAlive()) // first we check we have a player
            {
                if (dude.GetLevel() == nextBossLevel)
                {
                    nextBossLevel += bossLevelIncrement;

                    foreach (Transform t in spawnPoints)
                    {
                        Instantiate(enemy, t.position, Quaternion.identity);
                    }
                    nextSpawnTime = Time.time + bossLevelTimeDelay;
                }
                else if (Time.time > nextSpawnTime)
                {
                    Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                    nextSpawnTime = Time.time + timeBetweenSpawns;
                }


            }


        }
    }
}