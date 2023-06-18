using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
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

    [SerializeField] public GameObject YeomRockPrefab;
    [SerializeField] public GameObject EnemyPrefab;
    [SerializeField] public List<EnemyInfoSO> EnemyInfos;

    public UIMain mainUI;
    public List<MonsterBaseScript> enemyList;
    public int EnemyCount { get; set; } = 0;
    public int StageCount { get; set; } = 0;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if (mainUI == null) mainUI = FindObjectOfType<UIMain>();

    }

    public void UpdateEnemyCountUI()
    {
        mainUI.enemyCount = EnemyCount;
    }

    public void UpdateStageCountUI()
    {
        mainUI.stageCount = StageCount;
    }

    public void SetStage(int stagenum = -1)
    {
        StageCount = stagenum == -1 ? StageCount++ : stagenum;
    }

    public void NextStageLoad()
    {

    }
}
