using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialDialogue;
    [SerializeField] private TextMeshProUGUI _dialogueName;
    [SerializeField] private TextMeshProUGUI _helpText;

    private YeomRockInput _uiInput;

    private Action _keyPressed;
    private bool _isFrozen;

    // Start is called before the first frame update
    private void Awake()
    {
        _uiInput = new();
        _uiInput.UI.Enable();

        _uiInput.UI.Submit.started += context => KeyPressed();


        _helpText.text = string.Empty;
        _tutorialDialogue.text = string.Empty;
        _dialogueName.text = string.Empty;
    }

    void Start()
    {
        Time.timeScale = 0.0f;
        _isFrozen = true;

        _dialogueName.text = "염록";
        _tutorialDialogue.text = "내 이름은 염록, 햄버거를 하루 100개는 먹어야 하는 성장기 꼬맹이다. 하지만  햄버거는 너무 비싸. \r\n그래서 문을 닫은 폭데리아에 숨어 들어왔다.\r\n크르르르… 창고에 있는 햄버거는 모두 내 거야!";

    }

    void KeyPressed()
    {
        if( _isFrozen )
        {
            _isFrozen = false;
            Time.timeScale = 1.0f;
        }
    }
}
