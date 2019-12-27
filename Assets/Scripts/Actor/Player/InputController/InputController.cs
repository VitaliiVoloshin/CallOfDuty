using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Input", menuName = "InputConfig", order = 52)]
public class InputController:ScriptableObject
{
    [SerializeField] private KeyCode m_grenadeThrowButton;
    [SerializeField] private KeyCode m_shootButton;
    [SerializeField] private string m_verticalAxis;
    [SerializeField] private string m_horizontalAxis;

    public KeyCode grenadeThrow => m_grenadeThrowButton;
    public KeyCode shootButton => m_shootButton;
    public string verticalAxis => m_verticalAxis;
    public string horizontalAxis => m_horizontalAxis;
}
