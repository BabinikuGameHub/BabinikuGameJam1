using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private GameObject _yeomRock;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<EnemyInfoSO> _enemyInfos;
    [SerializeField] private GameObject _spawnPositionsParent;
    [SerializeField] private List<Transform> _enemySpawnPositions;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _enemySpawnPositions = _spawnPositionsParent.GetComponentsInChildren<Transform>().ToList();

    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach(Transform position in _enemySpawnPositions)
        {
            SpawnEnemyAtPosition(position);
        }
    }

    public void SpawnEnemyAtPosition(Transform SpawnPoint)
    {
        GameObject SpawnedEnemy = Instantiate(_enemyPrefab, SpawnPoint);
        MonsterBaseScript baseScript = SpawnedEnemy.GetComponent<MonsterBaseScript>();
        int index = Random.Range(0, _enemyInfos.Count);
        baseScript.InitializeWithSO(_enemyInfos[index]);
    }

}
