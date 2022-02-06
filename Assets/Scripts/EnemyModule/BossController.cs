using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] GameStats _gameStats;
    [SerializeField] EnemyStats _enemyStats;

    [SerializeField] ParticleSystem _splashEnemy;
    [SerializeField] Animator _myAnim;
    [SerializeField] Animator _bodyAnim;
    [SerializeField] public bool _catchPlayer = false;
    [SerializeField] Transform _player;
    [SerializeField] private Text _healthText;
    [SerializeField] private Button _playButton;

    [SerializeField] private Transform _weaponEnd;
    [SerializeField] private GameObject _bossBullet;

    [SerializeField] private GameObject _popUpText;

    private bool isAttacked = false;
    public bool playerSeen = false;

    private float lookRotationSpeed = 20f;
    private float _health;
    private float fireRate = 0.1f;
    float nextFire = 0f;
    int count = 5;
    void Start()
    {
        _health = _enemyStats.health;
        _healthText.text = _health.ToString();
    }


    void Update()
    {
        FireBullets();
        _healthText.text = _health.ToString();
        if (_gameStats.isGameOver)
        {
            _myAnim.enabled = false;
        }
    }



    public void getDamage() //Damage yedi
    {
        _health -= 1;
        isAttacked = true;
        _myAnim.SetBool("isAttacked", true);
        _bodyAnim.SetTrigger("getDamage");
        Instantiate(_popUpText, gameObject.transform.position, Quaternion.identity);
        if (_health <= 0)
        {
            Die();
        }
    }

    private void lookTarget()//Karakterin olduðu pozisyona bak
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        if (_gameStats.isGameOver)
        {
            _myAnim.SetTrigger("gameOver");
        }
    }
    private void Die()
    {
        Instantiate(_splashEnemy, gameObject.transform.position, _splashEnemy.transform.rotation);
        Destroy(gameObject, 0.1f);
        _gameStats.isGameSuccess = true;
        _playButton.gameObject.SetActive(true);
    }


    private void FireBullets()
    {
    
        if (Time.time >= nextFire && count < 5)
        {
            Instantiate(_bossBullet, _weaponEnd.position, _bossBullet.transform.rotation);
            nextFire = Time.time + fireRate;
            count++;
        }
    }

    private void ResetFireCount()
    {
        count = 0;
    }
        

}
