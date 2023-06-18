using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // 싱글톤 구현
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("게임 매니저가 없습니다.");
            }
            return _instance;
        }
    }
    private GameObject _yeomRock;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<EnemyInfoSO> _enemyInfos;
    [SerializeField] private GameObject _spawnPositionsParent;
    [SerializeField] private List<Transform> _enemySpawnPositions;
    
    public UIMain mainUI;
    public List<MonsterBaseScript> enemyList;
    public int enemyCount { get; set; } = 0;

    void Awake()
    {
        // 실행시 초기화, 중복시 그건 파괴
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _enemySpawnPositions = _spawnPositionsParent.GetComponentsInChildren<Transform>().ToList();
        if (mainUI == null) mainUI = FindObjectOfType<UIMain>();

    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (Transform position in _enemySpawnPositions)
        {
            SpawnEnemyAtPosition(position);
        }
        enemyCount = enemyList.Count;
        UpdateUI();
    }

    public void SpawnEnemyAtPosition(Transform SpawnPoint)
    {
        GameObject SpawnedEnemy = Instantiate(_enemyPrefab, SpawnPoint);
        MonsterBaseScript baseScript = SpawnedEnemy.GetComponent<MonsterBaseScript>();
        
        enemyList.Add(baseScript);

        int index = Random.Range(0, _enemyInfos.Count);
        baseScript.InitializeWithSO(_enemyInfos[index]);
    }
    public void UpdateUI()
    {
        mainUI.enemyCount = enemyCount;
    }
}
