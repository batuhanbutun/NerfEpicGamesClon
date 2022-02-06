using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameStats _gameStats;
    [SerializeField] EnemyStats _enemyStats;

    [SerializeField] ParticleSystem _splashEnemy;
    [SerializeField] PlayerController _playerController;
    [SerializeField] GameObject _visualDetector;
    [SerializeField] Animator _myAnim;
    [SerializeField] Animator _bodyAnim;
    [SerializeField] public bool _catchPlayer = false;
    [SerializeField] Transform _player;
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _popUpText;

    private bool isAttacked = false;
    public bool playerSeen = false;

    private float lookRotationSpeed = 20f;
    private float _health;
    void Start()
    {
         _health = _enemyStats.health;
         _healthText.text = _health.ToString();
         _visualDetector.gameObject.SetActive(false);
    }

   
    void Update()
    {
        SeePlayer();
    }



    public void getDamage() //Damage yedi
    {
             _health -= 1;
             isAttacked = true;
            _visualDetector.gameObject.SetActive(true);
            _myAnim.SetBool("isAttacked", true);
        _bodyAnim.SetTrigger("getDamage");
        Instantiate(_popUpText, transform.position, Quaternion.identity);
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
        _playerController.GetSeen();
        if(_gameStats.isGameOver)
        {
            _myAnim.SetTrigger("gameOver");
        }
    }

    private void SeePlayer()//Karakteri görünceki animasyon
    {
        if (playerSeen)
        {
            _myAnim.SetBool("seePlayer", true);
            _healthText.text = "!!!!";
            lookTarget();
        }
        else
        {
            _myAnim.SetBool("seePlayer", false);
            _healthText.text = _health.ToString();
        }
        if (_gameStats.isGameOver)
        {
            _myAnim.SetTrigger("gameOver");
            _visualDetector.gameObject.SetActive(false);
        }

    }

    private void Die()
    {
            Instantiate(_splashEnemy, gameObject.transform.position, _splashEnemy.transform.rotation);
            Destroy(gameObject,0.1f);
            _gameStats.isGameSuccess = true;
   
    }

}
