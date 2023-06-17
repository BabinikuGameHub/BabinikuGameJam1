using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    [SerializeField] private Sprite _basicCursor;
    [SerializeField] private Sprite _gunAimCursor;
    private Image _cursorImage;


    // Update is called once per frame
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        _cursorImage = GetComponent<Image>();
        SetToBasicCursor();
    }

    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void SetToBasicCursor()
    {
        _cursorImage.sprite = _basicCursor;
    }

    public void SetToAimCursor()
    {
        _cursorImage.sprite = _gunAimCursor;
    }
}
