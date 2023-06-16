using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YeomRockControl : MonoBehaviour
{
    private YeomRockInput _yControls;
    private InputAction _moveAction;
    public Vector2 PlayerMovement;


    // Start is called before the first frame update
    private void Awake()
    {
        _yControls = new YeomRockInput();
        _yControls.Enable();

        //WASD 움직임
        _yControls.Player.Move.performed += context => PlayerMovement = context.ReadValue<Vector2>();
        _yControls.Player.Move.canceled += context => PlayerMovement = Vector2.zero;

        //라이트 던지기
        _yControls.Player.ThrowLight.performed += context => LightThrow();


    }

    private void LightThrow()
    {

    }
}
