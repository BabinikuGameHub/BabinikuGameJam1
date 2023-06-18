using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YeomRockControl : MonoBehaviour
{
    private YeomRockInput _yControls;
    private InputAction _moveAction;
    public Vector2 PlayerMovement;
    public Vector2 PlayerLineOfSight;
    public Action LightThrowAction;
    public Action BulletTime;
    public Action FireBullet;

    // Start is called before the first frame update
    private void Awake()
    {
        _yControls = new YeomRockInput();
        _yControls.Enable();

        //WASD 움직임
        _yControls.Player.Move.performed += context => PlayerMovement = context.ReadValue<Vector2>();
        _yControls.Player.Move.canceled += context => PlayerMovement = Vector2.zero;

        ////라이트 던지기
        //_yControls.Player.ThrowLight.started += context => LightThrowAction.Invoke();

        //불렛타임. 단, 라이트 던지는 중에만 가능.
        _yControls.Player.BulletTime.started += context => BulletTime.Invoke();

        //불렛타임. 단, 라이트 던지는 중에만 가능.
        _yControls.Player.BulletTime.started += context => LightThrowAction.Invoke();

        //총 발사. 불렛타임 중에만 가능
        _yControls.Player.Fire.started += context => FireBullet.Invoke();

    }

    private void Update()
    {
        UpdateLoS();
    }

    void UpdateLoS()
    {
        Vector2 inputmousePosition = Mouse.current.position.ReadValue();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(inputmousePosition.x, inputmousePosition.y, Camera.main.nearClipPlane));
        Vector3 shoulderPosition = transform.position;

        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        Vector2 shoulderPosition2D = new Vector2(shoulderPosition.x, shoulderPosition.y);

        PlayerLineOfSight = (mousePosition2D - shoulderPosition2D).normalized;
    }
}
