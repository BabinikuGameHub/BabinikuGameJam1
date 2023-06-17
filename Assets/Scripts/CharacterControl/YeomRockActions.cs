using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class YeomRockActions : MonoBehaviour
{
    private Rigidbody2D _rb;
    private YeomRockControl _ctrl;
    private float _xVelocity;
    private float _yVelocity;
    [SerializeField]
    private SpriteRenderer _playerRenderer;
    [SerializeField]
    private Animator _playerAnim;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _handleSpeed;
    [SerializeField]
    private Transform _handle;
    [SerializeField]
    private Transform _handLight;
    [SerializeField]
    private Animator _handLightAnim;
    [SerializeField]
    private Transform _hand;

    [SerializeField]
    public int Health;
    [SerializeField]
    private float _bulletTimeDuration;
    [SerializeField]
    private float _hitRadius;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private GameObject _hitMarkerPrefab;

    private int _remainingAmmo;
    private Coroutine _bulletTimeCoroutine;
    private List<Vector2> _firePositions;
    private List<GameObject> _hitMarkerList;
    bool isThrowing;
    bool isBulletTime;
    bool isReloading;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _firePositions = new();
        _hitMarkerList = new();
        _remainingAmmo = 6;
    }

    void Start()
    {
        _ctrl = GetComponent<YeomRockControl>();
        _ctrl.LightThrowAction += LightThrow;
        _ctrl.BulletTime += BulletTime;
        _ctrl.FireBullet += FireAtPosition;


    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        ChangeLookPoint();
    }

    public void ApplyDamage()
    {
        Health--;
        UIHPScript.Instance.UpdateHealth(Health);
        CheckDeath();
    }

    public void Knockback(Vector2 Direction)
    {
        _rb.AddForce(Direction * 2000f);
    }

    void CheckDeath()
    {
        if(Health <= 0)
        {
            //GameOver;
            //DestroyObject
            //Show GameOver Prompt
            //Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }

    void ApplyMovement()
    {
        if (_ctrl.PlayerMovement != Vector2.zero)
            _playerAnim.SetBool("Move", true);
        else
            _playerAnim.SetBool("Move", false);
        _xVelocity = _ctrl.PlayerMovement.x * _movementSpeed;
        _yVelocity = _ctrl.PlayerMovement.y * _movementSpeed;

        _rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
    void ChangeLookPoint()
    {
        var mouseAngle = Mathf.Atan2(_ctrl.PlayerLineOfSight.y, _ctrl.PlayerLineOfSight.x) * Mathf.Rad2Deg;

        //_handle.eulerAngles = Vector3.Lerp(mouseAngle, _handle.eulerAngles, Time.deltaTime);

        _playerRenderer.flipX = _ctrl.PlayerLineOfSight.x > 0;
        _handle.eulerAngles = Mathf.LerpAngle(_handle.eulerAngles.z, mouseAngle, Time.deltaTime * _handleSpeed) * Vector3.forward;

    }
    void LightThrow()
    {
        if (isThrowing) return;
        isThrowing = true;
        StartCoroutine(LighThrowCoroutine());
    }
    IEnumerator LighThrowCoroutine()
    {
        _handLight.parent = transform;
        var StartAngle = _handLight.eulerAngles.z;
        float timer = 0;
        float throwTime = 1;
        int lookPos = _playerRenderer.flipX ? 1 : -1;
        _handLightAnim.SetTrigger("throw");
        while (throwTime >= timer)
        {
            yield return null;
            _handLightAnim.speed = throwTime;
            _handLight.eulerAngles = Vector3.forward * Mathf.Lerp(StartAngle, StartAngle + (360f * lookPos), timer / throwTime);

            timer += Time.deltaTime;
        }
        _handLight.rotation = _hand.rotation;
        _handLight.parent = _hand;
        isThrowing = false;
    }

    void BulletTime()
    {
        //손전등 던지는 중에만 발동 가능
        if (!isThrowing || isReloading) return;

        //이미 불렛타임 중이면 해제
        if (isBulletTime)
        {
            CancelBulletTime();
        }
        else
        {
            _bulletTimeCoroutine = StartCoroutine(BTCoroutine());
        }
    }

    IEnumerator BTCoroutine()
    {
        //불렛타임 진입
        isBulletTime = true;
        //시간 느려짐
        Time.timeScale = 0.1f;
        //커서가 조준커서로 바뀜
        CursorController.instance.SetToAimCursor();


        yield return new WaitForSecondsRealtime(_bulletTimeDuration);
        ResolveFire();
        CursorController.instance.SetToBasicCursor();
        Time.timeScale = 1f;
        isBulletTime = false;

    }

    private void CancelBulletTime()
    {
        if (_bulletTimeCoroutine != null)
        {
            StopCoroutine(_bulletTimeCoroutine);
            _bulletTimeCoroutine = null;
            ResolveFire();
            CursorController.instance.SetToBasicCursor();
            Time.timeScale = 1f;
            isBulletTime = false;
            Debug.Log("Bullet Time Cancelled");
        }
    }

    public void FireAtPosition()
    {
        if (!isBulletTime ||_remainingAmmo == 0)
            return;

        Vector2 inputmousePosition = Mouse.current.position.ReadValue();
        Vector2 firePosition = Camera.main.ScreenToWorldPoint(new Vector3(inputmousePosition.x, inputmousePosition.y, Camera.main.nearClipPlane));
        _firePositions.Add(firePosition);
        GameObject hitMarker = Instantiate(_hitMarkerPrefab, firePosition, Quaternion.identity);
        _hitMarkerList.Add(hitMarker);
        _remainingAmmo--;
        BulletCylinderScript.Instance.UpdateMagazine(_remainingAmmo);
    }

    public void ResolveFire()
    {
        foreach(Vector2 hitPosition in _firePositions)
        {
            //포지션에 총기 이펙트
            Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(hitPosition, _hitRadius);

            if(rangeChecks.Length > 0)
            {
                foreach(Collider2D collider in rangeChecks.ToList())
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        MonsterBaseScript mBaseScript = collider.gameObject.GetComponent<MonsterBaseScript>();
                        mBaseScript.TakeDamage();
                    }
                }
            }

        }

        foreach(GameObject hitMarker in _hitMarkerList)
        {
            Destroy(hitMarker);
        }
        _hitMarkerList = new();

        ReloadSequence();

    }

    public void ReloadSequence()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        BulletCylinderScript.Instance.ChangeRed();
        while (BulletCylinderScript.Instance.ReloadRound() == false)
        {
            yield return new WaitForSecondsRealtime(_reloadTime);
        }


        //yield return new WaitForSecondsRealtime(_reloadTime);
        //BulletCylinderScript.Instance.UpdateMagazine(_remainingAmmo);
        BulletCylinderScript.Instance.ChangeGreen();
        _remainingAmmo = 6;
        isReloading = false;
    }
}
