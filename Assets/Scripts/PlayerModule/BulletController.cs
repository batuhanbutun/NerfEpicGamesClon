using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private BossController _bossController;

    [SerializeField] private Transform _enemy;
    [SerializeField] private ParticleSystem _waterSplash;

    private float _bulletSpeed = 5f;

    Quaternion lookRotation;
    Vector3 moveDirection;
    void Start()
    {
        _enemy = GameObject.Find("Enemy").transform;
        if (_enemy)
        {
            _enemyController = _enemy.GetComponent<EnemyController>();
            moveDirection = new Vector3(_enemy.position.x - transform.position.x, 0, _enemy.position.z - transform.position.z);
        }

    }

    
    void Update()
    {
        Movement();
    }

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            Instantiate(_waterSplash, gameObject.transform.position, _waterSplash.transform.rotation);
            Destroy(gameObject);
            _enemyController.getDamage();
        }
        if (collision.collider.gameObject.layer == 9)
        {
            _bossController = _enemy.GetComponent<BossController>();
            Instantiate(_waterSplash, gameObject.transform.position, _waterSplash.transform.rotation);
            Destroy(gameObject);
            _bossController.getDamage();
        }
    }

    private void Die()
    {
        if (_gameStats.isGameOver)
            Destroy(gameObject);
    }

  
    private void Movement()//Su mermisi hareketi
    {
        if (_enemy)
        {
            transform.Translate(moveDirection * Time.deltaTime * _bulletSpeed);
        }
    }

}
