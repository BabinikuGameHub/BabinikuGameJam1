using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeomRockActions : MonoBehaviour
{
    private Rigidbody2D _rb;
    private YeomRockControl _ctrl;

    [SerializeField]
    private float _movementSpeed;

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
    }

    void ApplyMovement()
    {
        _xVelocity = _ctrl.PlayerMovement.x * _movementSpeed;
        _yVelocity = _ctrl.PlayerMovement.y * _movementSpeed;

        _rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

}
