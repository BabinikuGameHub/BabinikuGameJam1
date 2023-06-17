using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool isThrowing;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _ctrl = GetComponent<YeomRockControl>();
        _ctrl.lightThrowAction += LightThrow;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        ChangeLookPoint();
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
            //_handLightRig.

            timer += Time.deltaTime;
        }
        _handLight.rotation = _hand.rotation;
        _handLight.parent = _hand;
        isThrowing = false;
    }
}
