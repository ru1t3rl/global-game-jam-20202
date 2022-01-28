using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Floors
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] List<EnemySettings> possibleEnemies;
        [SerializeField] bool usePooling = false;

        Dictionary<EnemySettings, Stack<GameObject>> pool = new Dictionary<EnemySettings, Stack<GameObject>>();

        void Awake()
        {
            if (usePooling)
            {
                for (int i = 0, j; i < possibleEnemies.Count; i++)
                {
                    pool.Add(possibleEnemies[i], new Stack<GameObject>());
                    for (j = 0; j < possibleEnemies[i].maxToSpawn; j++)
                    {
                        pool[possibleEnemies[i]].Push(Instantiate(possibleEnemies[i].prefab));
                    }
                }
            }
        }

        public void SpawnEnemies()
        {
            for (int i = 0; i < possibleEnemies.Count; i++)
            {
                StartCoroutine(SpawnEnemy(possibleEnemies[i]));
            }
        }

        IEnumerator SpawnEnemy(EnemySettings enemy)
        {
            float toSpawn = Random.Range(enemy.minToSpawn, enemy.maxToSpawn);
            int spawned = 0;

            while (spawned < toSpawn)
            {
                if (usePooling)
                {
                    pool[enemy].Pop().gameObject.SetActive(true);
                }
                else
                {
                    Instantiate(enemy.prefab);
                }

                spawned++;
                yield return new WaitForSeconds(enemy.useRandomSpawnRate ? Random.Range(enemy.spawnRate, enemy.maxSpawnRate) : enemy.spawnRate);
            }
        }
    }

    [System.Serializable]
    public class EnemySettings
    {
        [Header("Prefab Settings")]
        public string name;
        public GameObject prefab;


        [Header("Spawn Settings")]
        public int minToSpawn;
        public int maxToSpawn;

        public bool useRandomSpawnRate = false;

        [Tooltip("When 'Use Random Spawn Rate' is checked spawnRate will be used as minimum")]
        [Range(0f, 10f)] public float spawnRate;
        [Range(0f, 10f)] public float maxSpawnRate;
    }
}