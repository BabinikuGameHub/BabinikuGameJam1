using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCylinderScript : MonoBehaviour
{
    public static BulletCylinderScript Instance;

    private YeomRockActions _yrActions;
    private int _magazine;
    [SerializeField] private List<GameObject> _cylinderObjects;

    private Color _originalColor;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        _originalColor = _cylinderObjects[0].GetComponent<Image>().color;
    }

    void Start()
    {
        _yrActions = GameObject.FindGameObjectWithTag("Player").GetComponent<YeomRockActions>();
        //_magazine = _yrActions.Magazine();
        UpdateMagazine(6);
    }

    public void UpdateMagazine(int roundNum)
    {
        _magazine = roundNum;

        if (_magazine >= 6)
        {//Reload
            foreach (var obj in _cylinderObjects)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            _cylinderObjects[_magazine + 1].SetActive(false);
        }
    }

    public bool ReloadRound()
    {
        if(_magazine == 6)
        {
            //꽉 차면 return true
            return true;
        }
        else
        {
            _magazine++;
            _cylinderObjects[_magazine].SetActive(true);
            return false;
        }
    }

    public void ChangeRed()
    {
        foreach(GameObject cylinder in  _cylinderObjects)
        {
            cylinder.GetComponent<Image>().color = Color.red;
        }
    }

    public void ChangeGreen()
    {
        foreach (GameObject cylinder in _cylinderObjects)
        {
            cylinder.GetComponent<Image>().color = _originalColor;
        }
    }

}
