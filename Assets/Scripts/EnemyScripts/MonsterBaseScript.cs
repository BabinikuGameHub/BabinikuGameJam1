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

    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private GameObject _projectile;

    private EnemyInfoSO _enemyInfo;
    private float _rateOfFire = 1f;
    private float _rofCount = 0f;
    private float _projectileSpeed;
    private Sprite _aliveEnemySprite;
    private Sprite _deadEnemySprite;
    private EnemyType _enemyType;
    private int _monsterLevel;
    private int _monsterHealth;
    private float _movementSpeed;
    private float _minimumDistance;
    private bool _isKnockedBack;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        if(_enemyInfo != null)
            InitializeWithSO(_enemyInfo);

    }

    void Start()
    {
        _yeomRock = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;

        if(!_isKnockedBack) _rb.velocity = Vector2.zero;

        UpdateLineOfSight();

        if (_enemyType == EnemyType.Close)
        {//근접타입이면 단순 접근 행동
            ApproachPlayer();
        }
        else
        {//원거리타입이면 최소거리 접근 후 원거리 공격
            if (IsWithinMinimalDistance())
            {
                ShootPlayer();
            }
            else
            {
                ApproachPlayer();
            }
        }
    }

    public void InitializeWithSO(EnemyInfoSO SOInfo)
    {
        _enemyInfo = SOInfo;

        _enemyType = _enemyInfo.EnemyType;
        _monsterLevel = _enemyInfo.MonsterLevel;
        _monsterHealth = _monsterLevel;

        _projectileSpeed = _enemyInfo.ProjectileSpeed;

        _movementSpeed = _enemyInfo.MovementSpeed;
        _minimumDistance = _enemyInfo.MinimumDistance;
        _rateOfFire = _enemyInfo.RateOfFire;

        _aliveEnemySprite = _enemyInfo.AliveEnemySprite;
        _deadEnemySprite = _enemyInfo.DeadEnemySprite;

        _enemySpriteRenderer.sprite = _aliveEnemySprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            YeomRockActions actions = collision.gameObject.GetComponent<YeomRockActions>();
            actions.ApplyDamage();

            //근접공격은 염록이도 적도 상호 녹백
            actions.Knockback(_lineOfSight);
            Knockback();
            //TakeDamage();
        }
    }

    private void Knockback()
    {
        Vector2 knockbackDirection = new Vector2(-_lineOfSight.x, -_lineOfSight.y);

        _rb.AddForce(knockbackDirection * 1500f);

        _isKnockedBack = true;
        StartCoroutine(ResetRB());
    }

    private IEnumerator ResetRB()
    {
        yield return new WaitForSeconds(0.15f);
        _rb.velocity = Vector2.zero;

        _isKnockedBack = false;
    }

    void UpdateLineOfSight()
    {
        _lineOfSight = (_yeomRock.transform.position - transform.position).normalized;
        _enemySpriteRenderer.flipX = _lineOfSight.x > 0;
    }

    bool IsWithinMinimalDistance()
    {
        float distance = Vector2.Distance(_yeomRock.transform.position, transform.position);

        if(distance < _minimumDistance)
        {
            return true;
        }

        return false;
    }

    void ApproachPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _yeomRock.transform.position, _movementSpeed * Time.deltaTime);
    }

    void ShootPlayer()
    {
        if(_rofCount <= 0)
        {
            GameObject projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            ProjectileScript pScript = projectile.GetComponent<ProjectileScript>();
            pScript.SetTrajectory(_lineOfSight, _projectileSpeed);

            _rofCount = _rateOfFire;
        }
        else
        {
            _rofCount -= Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        Debug.Log("EnemyHit");
        _monsterHealth--;
        Knockback();
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