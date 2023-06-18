using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
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
                    Debug.Log("°ÔÀÓ ¸Å´ÏÀú°¡ ¾ø½À´Ï´Ù.");
            }
            return _instance;
        }
    }

    private GameObject _yeomRock;

    [SerializeField] public GameObject YeomRockPrefab;
    [SerializeField] public GameObject EnemyPrefab;
    [SerializeField] public List<EnemyInfoSO> EnemyInfos;

    public UIMain mainUI;
    public Light2D LightControl;
    public List<MonsterBaseScript> enemyList;
    public int EnemyCount { get; set; } = 0;
    public int StageCount { get; set; } = 1;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //if (mainUI == null) mainUI = FindObjectOfType<UIMain>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void UpdateEnemyCountUI()
    {
        mainUI.enemyCount = EnemyCount;

        if(EnemyCount <= 0)
        {
            StartCoroutine(StageClearSequence());
        }
    }

    public void UpdateStageCountUI()
    {
        if (mainUI == null)
            return;

        Time.timeScale = 1f;
        mainUI.stageCount = StageCount;
    }

    public void SetStage(int stagenum = -1)
    {
        StageCount = stagenum == -1 ? StageCount++ : stagenum;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "GameOverScene" || scene.name != "ClearScene")
        {
            LightControl = FindObjectOfType<Light2D>();
            LightControl.intensity = 0.03f;
        }

        UpdateStageCountUI();
    }

    IEnumerator StageClearSequence()
    {
        //Stage Clear UI
        //오우예아 음성?
        LightControl.intensity = 1f;
        StartCoroutine(mainUI.StageClearCoroutine());

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void CheckNextScene()
    {

        Scene scene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void LoadClearScene()
    {
        SceneManager.LoadScene("ClearScene");
    }
}
