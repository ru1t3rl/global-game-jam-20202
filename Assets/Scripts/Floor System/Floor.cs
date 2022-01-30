using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Floors
{
    [RequireComponent(typeof(BoxCollider))]
    public class Floor : MonoBehaviour
    {
        [SerializeField] bool usePooling = false;
        [SerializeField] List<EnemySettings> possibleEnemies;
        [SerializeField] Door[] doors;

        public UnityEvent onFinishFloor;
        public UnityEvent onEnterRoom;

        Dictionary<EnemySettings, Stack<Entity>> pool = new Dictionary<EnemySettings, Stack<Entity>>();

        int activeEntities = 0;

        int toSpawn = 0;

        Object _lock = new Object();

        BoxCollider spawnCollider;
        public BoxCollider SpawnCollider
        {
            get
            {
                if (!spawnCollider)
                    spawnCollider = GetComponent<BoxCollider>();
                return spawnCollider;
            }
        }

        void Awake()
        {
            if (usePooling)
            {
                for (int i = 0, j; i < possibleEnemies.Count; i++)
                {
                    pool.Add(possibleEnemies[i], new Stack<Entity>());
                    for (j = 0; j < possibleEnemies[i].maxToSpawn; j++)
                    {
                        pool[possibleEnemies[i]].Push(Instantiate(possibleEnemies[i].prefab).GetComponent<Entity>());
                        pool[possibleEnemies[i]].Peek().gameObject.SetActive(false);
                    }
                }
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].Open();
            }
            SpawnEnemies();
        }


        public Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x + boxCollider.center.x, extents.x - boxCollider.center.x),
                2.5f,
                Random.Range(-extents.z + boxCollider.center.z, extents.z - boxCollider.center.z)
            );

            return transform.position + point;
        }

        public void SpawnEnemies()
        {
            for (int i = 0; i < possibleEnemies.Count; i++)
            {
                StartCoroutine(SpawnEnemy(possibleEnemies[i]));
            }
        }

        public void OnEntityDeath(Entity entity, DamageData data)
        {
            activeEntities--;

            if (activeEntities <= 0 && toSpawn <= 0)
            {
                FinishFloor();
            }
        }

        void FinishFloor()
        {
            onFinishFloor?.Invoke();
        }

        IEnumerator SpawnEnemy(EnemySettings enemy)
        {
            int toSpawn = Random.Range(enemy.minToSpawn, enemy.maxToSpawn);
            this.toSpawn += toSpawn;
            int spawned = 0;

            while (spawned < toSpawn)
            {
                if (usePooling)
                {
                    try
                    {
                        pool[enemy].Peek().OnDeath.AddListener(OnEntityDeath);
                        pool[enemy].Peek().transform.position = GetRandomPointInsideCollider(SpawnCollider);
                        pool[enemy].Pop().gameObject.SetActive(true);
                    }
                    catch (KeyNotFoundException)
                    {
                        Instantiate(enemy.prefab, GetRandomPointInsideCollider(SpawnCollider), Quaternion.identity).GetComponent<Entity>().OnDeath.AddListener(OnEntityDeath);
                    }
                }
                else
                {
                    Instantiate(enemy.prefab, GetRandomPointInsideCollider(SpawnCollider), Quaternion.identity).GetComponent<Entity>().OnDeath.AddListener(OnEntityDeath);
                }


                lock (_lock)
                {
                    spawned++;
                    activeEntities++;
                    this.toSpawn--;
                }

                Debug.Log(activeEntities);

                yield return new WaitForSeconds(enemy.useRandomSpawnRate ? Random.Range(enemy.spawnRate, enemy.maxSpawnRate) : enemy.spawnRate);
            }
        }

        public void CloseDoors()
        {
            for (int iDoor = 0; iDoor < doors.Length; iDoor++)
            {
                doors[iDoor].Close();
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Player>())
            {
                onEnterRoom?.Invoke();
                for (int i = 0; i < doors.Length; i++) { doors[i].Close(); }
            }
        }
    }

    [System.Serializable]
    public class EnemySettings
    {
        [Header("Prefab Settings")]
        public string name;
        public GameObject prefab;

        private Entity entity;
        public Entity Entity
        {
            get
            {
                if (!entity)
                    entity = prefab.GetComponent<Entity>();
                return entity;
            }
        }


        [Header("Spawn Settings")]
        public int minToSpawn;
        public int maxToSpawn;

        public bool useRandomSpawnRate = false;

        [Tooltip("When 'Use Random Spawn Rate' is checked spawnRate will be used as minimum")]
        [Range(0f, 10f)] public float spawnRate;
        [Range(0f, 10f)] public float maxSpawnRate;
    }
}