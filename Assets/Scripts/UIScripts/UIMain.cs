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

    public int enemyCount { set { enemyCountText.text = value.ToString(); } }
    public int stageCount { set { stageCountText.text = "STAGE " + value.ToString(); } }

    private void Awake()
    {
        GameManager.Instance.mainUI = this;
    }

}
