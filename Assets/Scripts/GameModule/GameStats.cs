using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameStats")]
public class GameStats : ScriptableObject
{
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _isGameSuccess = false;
    public bool isGameOver { get { return _isGameOver; } set { _isGameOver = value; } }
    public bool isGameSuccess { get { return _isGameSuccess; } set { _isGameSuccess = value; } }
}
