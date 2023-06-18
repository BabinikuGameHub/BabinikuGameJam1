using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMain : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI enemyCountText;
    [SerializeField]
    private TextMeshProUGUI stageCountText;
    [SerializeField]
    private TextMeshProUGUI stageClearText;

    public int enemyCount { set { enemyCountText.text = value.ToString(); } }
    public int stageCount { set { stageCountText.text = "STAGE " + value.ToString(); } }

    private void Awake()
    {
        GameManager.Instance.mainUI = this;
        stageClearText.enabled = false;
    }
    public IEnumerator StageClearCoroutine()
    {
        stageClearText.enabled = true;

        yield return new WaitForSecondsRealtime(3f);
        stageClearText.enabled = false;
    }

}
