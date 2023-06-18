using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private GameObject _playerSpawnPosition;
    [SerializeField] private GameObject _spawnPositionsParent;
    private List<Transform> _enemySpawnPositions;
    private List<MonsterBaseScript> _enemyList;

    // Start is called before the first frame update
    void Awake()
    {
        _enemySpawnPositions = _spawnPositionsParent.GetComponentsInChildren<Transform>().ToList();
        _enemyList = new();
    }

    private void Start()
    {
        //염록 소환!
        GameObject Yeom = Instantiate(GameManager.Instance.YeomRockPrefab, _playerSpawnPosition.transform);
        _cam.Follow = Yeom.transform;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (Transform position in _enemySpawnPositions)
        {
            SpawnEnemyAtPosition(position);
        }
        GameManager.Instance.EnemyCount = _enemyList.Count;
        GameManager.Instance.UpdateEnemyCountUI();
    }

    public void SpawnEnemyAtPosition(Transform SpawnPoint)
    {
        GameObject SpawnedEnemy = Instantiate(GameManager.Instance.EnemyPrefab, SpawnPoint);
        MonsterBaseScript baseScript = SpawnedEnemy.GetComponent<MonsterBaseScript>();

        _enemyList.Add(baseScript);

        int index = Random.Range(0, GameManager.Instance.EnemyInfos.Count);
        baseScript.InitializeWithSO(GameManager.Instance.EnemyInfos[index]);
    }

}
