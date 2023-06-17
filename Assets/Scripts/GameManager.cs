using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private GameObject _yeomRock;

    public GameObject EnemyPrefab;
    public List<EnemyInfoSO> EnemyInfos;
    public List<Transform> EnemySpawnPositions;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach(Transform position in EnemySpawnPositions)
        {
            SpawnEnemyAtPosition(position);
        }
    }

    public void SpawnEnemyAtPosition(Transform SpawnPoint)
    {
        GameObject SpawnedEnemy = Instantiate(EnemyPrefab, SpawnPoint);
        MonsterBaseScript baseScript = SpawnedEnemy.GetComponent<MonsterBaseScript>();
        int index = Random.Range(0, EnemyInfos.Count);
        baseScript.InitializeWithSO(EnemyInfos[index]);
    }

}
