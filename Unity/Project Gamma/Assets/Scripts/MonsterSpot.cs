using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpot : MonoBehaviour
{
    public Transform[] zombieSpot;
    public Transform[] orcSpot;
    public Transform[] slimeSpot;
    public Transform[] kingSlimeSpot;
    public Transform[] minotaurSpot;
    public Transform[] dragonSpot;

    public GameObject zombiePrefab;
    public GameObject orcPrefab;
    public GameObject slimePrefab;
    public GameObject kingSlimePrefab;
    public GameObject minotaurPrefab;
    public GameObject dragonPrefab;

    public float zombieSpawnRadius;
    public float orcSpawnRadius;
    public float slimeSpawnRadius;
    public int zombieSwarmCount;
    public int orcSwarmCount;
    public int slimeSwarmCount;

    private void Awake()
    {
        SpawnBoss(kingSlimePrefab, kingSlimeSpot);
        SpawnBoss(minotaurPrefab, minotaurSpot);
        SpawnBoss(dragonPrefab, dragonSpot);

        SpawnMonster(zombiePrefab, zombieSpot, zombieSwarmCount, zombieSpawnRadius);
        SpawnMonster(orcPrefab, orcSpot, orcSwarmCount, orcSpawnRadius);
        SpawnMonster(slimePrefab, slimeSpot, slimeSwarmCount, slimeSpawnRadius);
    }
    private void SpawnBoss(GameObject prefab, Transform[] spot)
    {
        if (prefab == null || spot.Length == 0)
            return;

        for (int i = 0; i < spot.Length; i++)
        {
            Instantiate(prefab, spot[i].position, spot[i].rotation);
        }
    }

    private void SpawnMonster(GameObject prefab, Transform[] spot, int count, float spawnRadius)
    {
        if (prefab == null || spot.Length == 0 || count == 0)
            return;

        for (int i = 0; i < spot.Length; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
                randomOffset.y = 0;
                Vector3 randomSpawnPosition = spot[i].position + randomOffset;
                Instantiate(prefab, randomSpawnPosition, spot[i].rotation);
            }
        }
    }
}