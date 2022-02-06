using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private Transform _player;
    [SerializeField] private ParticleSystem _waterSplash;
    [SerializeField] private ParticleSystem _splashPlayer;

    private float _bulletSpeed = 3f;

    Quaternion lookRotation;
    Vector3 moveDirection;

    void Start()
    {
        BulletDirection();
    }


    void Update()
    {
        Movement();
    }

    private void Movement()//Su mermisi hareketi
    {
        if (_player)
        {
            transform.Translate(moveDirection * Time.deltaTime * _bulletSpeed);
            Die();
        }
    }

    private void Die()
    {
        if (_gameStats.isGameOver)
            Destroy(gameObject);
    }

    private void BulletDirection()
    {
        _player = GameObject.Find("Player").transform;
        if (_player)
        {
            _playerController = _player.GetComponent<PlayerController>();
            if (_playerController._isEnemyLeft)
            {
                moveDirection = new Vector3((_playerController.idlePos.x- transform.position.x) - 0.25f, 0, _player.position.z - transform.position.z);
            }
            else
            {
                moveDirection = new Vector3((_playerController.idlePos.x - transform.position.x) + 0.25f, 0, _player.position.z - transform.position.z);
            }

        }
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            Instantiate(_splashPlayer, gameObject.transform.position, _splashPlayer.transform.rotation);
            Destroy(collision.collider.gameObject,0.1f);
            _gameStats.isGameOver = true;
        }
    }
}
