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

    private void Update()
    {
        UpdateLoS();
    }

    private void LightThrow()
    {

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
