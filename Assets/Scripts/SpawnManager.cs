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
    [SerializeField] private Bounds _mapbounds;
    private List<Transform> _enemySpawnPositions;
    private List<GameObject> _enemyList;

    void Awake()
    {
        if (_spawnPositionsParent != null)
        {
            _enemySpawnPositions = _spawnPositionsParent.GetComponentsInChildren<Transform>().ToList();
            _enemyList = new();
        }
    }

    private void Start()
    {
        SpawnYeomRock();

        if (_spawnPositionsParent == null || _enemySpawnPositions.Count == 0)
            return;

        SpawnEnemies();
        //StartCoroutine(SpawnEnemies());
    }

    private void SpawnYeomRock()
    {
        //염록 소환!
        GameObject Yeom = Instantiate(GameManager.Instance.YeomRockPrefab, _playerSpawnPosition.transform);
        _cam.Follow = Yeom.transform;
        UIHPScript.Instance.InstantiateWYeom(Yeom);
    }

    private void SpawnEnemies()
    {
        foreach (Transform position in _enemySpawnPositions)
        {
            SpawnEnemyAtPosition(position);
            GameManager.Instance.EnemyCount = _enemyList.Count;
            GameManager.Instance.UpdateEnemyCountUI();
        }

        GameManager.Instance.EnemyCount = _enemyList.Count;
        GameManager.Instance.UpdateEnemyCountUI();
    }

    public void SpawnEnemyAtPosition(Transform SpawnPoint)
    {
        SpawnPoint.position = new Vector2(SpawnPoint.position.x, SpawnPoint.position.y);

        GameObject SpawnedEnemy = Instantiate(GameManager.Instance.EnemyPrefab, SpawnPoint);
        MonsterBaseScript baseScript = SpawnedEnemy.GetComponent<MonsterBaseScript>();

        if(SpawnedEnemy != null)
        {
            _enemyList.Add(SpawnedEnemy);
        }
        else
        {
            Debug.Log("Spawn error");
        }

        int index = Random.Range(0, GameManager.Instance.EnemyInfos.Count);
        baseScript.InitializeWithSO(GameManager.Instance.EnemyInfos[index]);
    }

}
