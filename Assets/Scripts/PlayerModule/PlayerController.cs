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
        idlePos = transform.position; //Karakter yer deðiþtirecekse her yer deðiþtinde bunu çaðýrabilirim. Deðiþtiði yerin konumunu alýrým.
        _enemy = GameObject.Find("Enemy").transform; // 2 tane enemy olduðu zaman bunu fonksiyon içine alabilirim. Bir tanesi öldüðünde çalýþtýrýrým.
        EnemyLocation();
    }

  
    void Update()
    {
        CharacterControl();
    }

    private void CharacterControl()//Karakter Ýnput kontrolleri
    {
        if(!_gameStats.isGameOver && !_gameStats.isGameSuccess)
        {
            if (Input.touchCount > 0) //Mobile Ýnput
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
                Debug.Log("ÖLDÜN");
            }
         /*   if (Input.GetMouseButton(0) ) PC INPUT BAÞLANGIÇ
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

    public void lookTarget()//Düþman nerede olursa olsun oraya dön otomatize
    {
        if (_enemy)
        {
            Vector3 direction = (_enemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            gameObject.transform.rotation = lookRotation;//Daha yumuþak dönüþ için Slerp
        }
        else
        {
            _enemy = GameObject.Find("Enemy").transform;
            Vector3 direction = (_enemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            gameObject.transform.rotation = lookRotation;//Daha yumuþak dönüþ için Slerp
        }
        
    }

    private void idleRotation()//Ateþ etmediðim zaman cameraya dönsün karakter
    {
        transform.position = idlePos;
        Vector3 direction = (_cam.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        gameObject.transform.rotation = lookRotation;//Daha yumuþak dönüþ için Slerp
    }

   public void EnemyLocation()//Düþman solumuzda mý saðýmýzda mý
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

    private void PlayerFirePos()//Düþman solumuzda ise sola çýk sað ise saða
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

    public void GetSeen()//Düþmana kaç saniye gözükme þansýmýz
    {
        _playerStats.maxSeenTime -= Time.deltaTime;
        if (_playerStats.maxSeenTime <= 0)
        {
            _gameStats.isGameOver = true;
        }
    }

}
