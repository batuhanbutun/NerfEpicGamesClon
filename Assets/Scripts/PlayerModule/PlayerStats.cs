using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _maxSeenTime = 1f;
    public float fireRate { get { return _fireRate; } }
    public float maxSeenTime { get { return _maxSeenTime; } set { _maxSeenTime = value; } }
}
