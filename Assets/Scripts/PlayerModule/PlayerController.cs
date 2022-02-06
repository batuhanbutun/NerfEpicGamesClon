using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] public Transform _enemy;
    [SerializeField] private Transform _cam;

    [SerializeField] bool _isFireOn = false;

    [SerializeField] private Transform _weaponEnd;
    [SerializeField] private GameObject _waterBullet;
    [SerializeField] private Animator _myAnim;

    public Vector3 idlePos;
    public bool _isEnemyLeft;
    private float nextFire = 0f;

    void Start()
    {
        _playerStats.maxSeenTime = 1f;
        idlePos = transform.position; //Karakter yer de�i�tirecekse her yer de�i�tinde bunu �a��rabilirim. De�i�ti�i yerin konumunu al�r�m.
        _enemy = GameObject.Find("Enemy").transform; // 2 tane enemy oldu�u zaman bunu fonksiyon i�ine alabilirim. Bir tanesi �ld���nde �al��t�r�r�m.
        EnemyLocation();
    }

  
    void Update()
    {
        CharacterControl();
    }

    private void CharacterControl()//Karakter �nput kontrolleri
    {
        if(!_gameStats.isGameOver && !_gameStats.isGameSuccess)
        {
            if (Input.touchCount > 0) //Mobile �nput
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _isFireOn = true;
                    _myAnim.SetBool("isFireOn", true);
                    lookTarget();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _isFireOn = false;
                    _myAnim.SetBool("isFireOn", false);
                    nextFire = Time.time + _playerStats.fireRate;
                }
            }
           
            if (_playerStats.maxSeenTime <= 0)
            {
                Debug.Log("�LD�N");
            }
         /*   if (Input.GetMouseButton(0) ) PC INPUT BA�LANGI�
            {
                _isFireOn = true;
                    _myAnim.SetBool("isFireOn", true);
                    lookTarget();
            }
            if (Input.GetMouseButtonUp(0) )
            {
                _isFireOn = false;
                    _myAnim.SetBool("isFireOn", false);
                    nextFire = Time.time + _playerStats.fireRate;
            }PC INPUT SON */
            if (!_isFireOn)
            {
                idleRotation();
            }
        }
        else if (_gameStats.isGameOver)
        {
            _myAnim.SetBool("isGameOver", true);
        }
        else if (_gameStats.isGameSuccess)
        {
            _myAnim.SetBool("isGameOver", true);
        }
    }

    private void Fire()
    {
        if (Time.time >= nextFire)
        {
            Instantiate(_waterBullet, _weaponEnd.position, _waterBullet.transform.rotation);
            nextFire = Time.time + _playerStats.fireRate;
        }
        
    }//FireRate

    public void lookTarget()//D��man nerede olursa olsun oraya d�n otomatize
    {
        if (_enemy)
        {
            Vector3 direction = (_enemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            gameObject.transform.rotation = lookRotation;//Daha yumu�ak d�n�� i�in Slerp
        }
        else
        {
            _enemy = GameObject.Find("Enemy").transform;
            Vector3 direction = (_enemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            gameObject.transform.rotation = lookRotation;//Daha yumu�ak d�n�� i�in Slerp
        }
        
    }

    private void idleRotation()//Ate� etmedi�im zaman cameraya d�ns�n karakter
    {
        transform.position = idlePos;
        Vector3 direction = (_cam.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        gameObject.transform.rotation = lookRotation;//Daha yumu�ak d�n�� i�in Slerp
    }

   public void EnemyLocation()//D��man solumuzda m� sa��m�zda m�
    {
        if (_enemy.position.x < transform.position.x)
        {
            _isEnemyLeft = true;
        }
        else
        {
            _isEnemyLeft = false;
        }
    }

    private void PlayerFirePos()//D��man solumuzda ise sola ��k sa� ise sa�a
    {
        if (_isEnemyLeft)
        {
            transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
        }  
    }

    public void GetSeen()//D��mana ka� saniye g�z�kme �ans�m�z
    {
        _playerStats.maxSeenTime -= Time.deltaTime;
        if (_playerStats.maxSeenTime <= 0)
        {
            _gameStats.isGameOver = true;
        }
    }

}
