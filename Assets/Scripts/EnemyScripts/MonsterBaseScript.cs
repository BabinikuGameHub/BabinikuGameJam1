using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseScript : MonoBehaviour
{
    private Collider2D _collider;
    private Rigidbody2D _rb;
    private GameObject _yeomRock;
    private bool _isDead;
    private Vector2 _lineOfSight;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private Sprite _deadEnemySprite;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] [Range (1,3)] private int _monsterLevel;
    private int _monsterHealth;
    [SerializeField] private float _movementSpeed;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _monsterHealth = _monsterLevel;
    }

    void Start()
    {
        _yeomRock = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;

        UpdateLineOfSight();

        if (_enemyType == EnemyType.Close)
        {//근접타입이면 단순 접근 행동
            ApproachPlayer();
        }
        else
        {//원거리타입이면 원거리 공격
            ShootPlayer();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }

    void UpdateLineOfSight()
    {
        _lineOfSight = (_yeomRock.transform.position - transform.position).normalized;
        _enemySpriteRenderer.flipX = _lineOfSight.x > 0;

    }

    void ApproachPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _yeomRock.transform.position, _movementSpeed * Time.deltaTime);
    }

    void ShootPlayer()
    {
        GameObject projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        ProjectileScript pScript = projectile.GetComponent<ProjectileScript>();
        pScript.SetTrajectory(_lineOfSight, _projectileSpeed);

    }

    public void TakeDamage()
    {
        _monsterHealth--;

        CheckDeath();
    }

    private void CheckDeath()
    {
        //적이 사망하면 시체는 어두워도 보이게 처리

        if(_monsterHealth <= 0)
        {
            //삭제가 아닌 시체 남긴체로 비활성화
            _enemySpriteRenderer.sprite = _deadEnemySprite;
            _collider.enabled = false;
            _rb.velocity = Vector2.zero;
            _isDead = true;
        }
    }
}

public enum EnemyType
{
    Close,
    Ranged,
}
