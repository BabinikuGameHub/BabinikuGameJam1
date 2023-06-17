using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YeomRockActions : MonoBehaviour
{
    private Rigidbody2D _rb;
    private YeomRockControl _ctrl;
    [SerializeField]
    private SpriteRenderer _playerRenderer;
    [SerializeField]
    private Animator _playerAnim;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float handleSpeed;
    [SerializeField]
    private Transform _handle;
    private float _xVelocity;
    private float _yVelocity;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _ctrl = GetComponent<YeomRockControl>();
        
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
        _handle.eulerAngles = Mathf.LerpAngle(_handle.eulerAngles.z, mouseAngle, Time.deltaTime * handleSpeed) * Vector3.forward;

    }

    void LightThrow()
    {

    }
}
