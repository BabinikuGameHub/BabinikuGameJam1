using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPScript : MonoBehaviour
{
    private YeomRockActions _yeomRock;
    private int _yeomRockHealth;
    [SerializeField] private GameObject _hpIconPrefab;

    private List<GameObject> _hpIcons = new();

    void Start()
    {
        _yeomRock = GameObject.FindWithTag("Player").GetComponent<YeomRockActions>();
        //_yeomRockHealth = _yeomRock.Health;
        _yeomRockHealth = 5;

        for (int i = 0; i < _yeomRockHealth; i++)
        {
            GameObject hpIcon = Instantiate(_hpIconPrefab, transform);
            _hpIcons.Add(hpIcon);
        }

    }

    public void UpdateHealth(int health)
    {
        _yeomRockHealth = health;

        //일단 이미지 끄기
        for(int i = 0; i < _hpIcons.Count; i++)
        {
            Image hpIcon = _hpIcons[i].GetComponent<Image>();
            if (i < _yeomRockHealth)
            {
                hpIcon.enabled = false;
            }
            else
            {
                hpIcon.enabled = true;
            }
        }

    }
}
