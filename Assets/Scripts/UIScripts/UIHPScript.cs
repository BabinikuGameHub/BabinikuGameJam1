using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPScript : MonoBehaviour
{
    public static UIHPScript Instance;

    private YeomRockActions _yeomRock;
    private int _yeomRockHealth;
    [SerializeField] private GameObject _hpIconPrefab;

    private List<GameObject> _hpIcons = new();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        _yeomRock = GameObject.FindWithTag("Player").GetComponent<YeomRockActions>();
        _yeomRockHealth = _yeomRock.Health;

        for (int i = 0; i < _yeomRockHealth; i++)
        {
            GameObject hpIcon = Instantiate(_hpIconPrefab, transform);
            _hpIcons.Add(hpIcon);
        }

    }
    public void TestDamage()
    {
        _yeomRockHealth--;
        UpdateHealth(_yeomRockHealth);
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
                hpIcon.enabled = true;
            }
            else
            {
                hpIcon.enabled = false;
            }
        }

    }
}
