using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float _health = 20f;
 
    public float health { get { return _health; } }
  

}
