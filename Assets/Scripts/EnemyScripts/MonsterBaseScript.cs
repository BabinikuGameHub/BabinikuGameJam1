using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseScript : MonoBehaviour
{
    private Collider2D _collider;
    private Rigidbody2D _rb;
    private GameObject _yeomRock;

    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _movementSpeed;

    // Start is called before the first frame update

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _yeomRock = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyType == EnemyType.Close)
        {//근접타입이면 단순 접근 행동
            ApproachPlayer();
        }
        else
        {//원거리타입이면 원거리 공격

        }


    }

    void ApproachPlayer()
    {
        float step = _movementSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, _yeomRock.transform.position, step);
    }
}

public enum EnemyType
{
    Close,
    Ranged,
}
