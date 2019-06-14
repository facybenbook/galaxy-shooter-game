using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _nextFire = 0.0f;

    public bool isTripleShotActive = false;

    public bool isSpeedBostActive = false;

    private float _speedBoostConst = 1.5f;

    public bool isShieldActive = false;

    [SerializeField]
    private GameObject _shieldGameObject;

    public int lives = 3;

    private UIManager _UIManager;

    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    private float horizontalSpeed = 2.0F;
    private float verticalSpeed = 2.0F;
    private float horizontalInput = 0;
    private float verticalInput = 0;

    [SerializeField]
    private GameObject[] _engineFailures;

    private int _hitCount = 0;

    // Use this for initialization
    void Start () {

        Debug.Log("Name: " + name + " is called!");
        Debug.Log("Player position: " + transform.position);
        transform.position = new Vector3(0, 0, 0);

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_UIManager != null)
        {
            _UIManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnCoroutines();
        }

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

   //   Debug.Log("Update is called!");

        Movement();
        
        if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
            Shoot();
        }

	}

    private void Movement ()
    {

        if (Input.GetAxis("Mouse X") != 0)
        {
            horizontalInput = verticalSpeed * Input.GetAxis("Mouse X");
        }
        else
        {
            horizontalInput = horizontalSpeed * Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            verticalInput = Input.GetAxis("Mouse Y");
        }
        else
        {
            verticalInput = Input.GetAxis("Vertical");
        }
       
        if (isSpeedBostActive == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * _speedBoostConst * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }
    }

    private void Shoot()
    {

        if ( (Time.time > _nextFire))
        {
            _audioSource.Play();

            _nextFire = Time.time + _fireRate;

            if (isTripleShotActive == true)
            {
                //Triple Shot
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                //Center
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            }
            
        }
        
    }

    private void TripleShot()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.04f, 0), Quaternion.identity);
        Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.04f, 0), Quaternion.identity);
    }

    public void TripleShotPowerupOn()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerupDown());
    }

    public IEnumerator TripleShotPowerupDown()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBostActive = true;
        StartCoroutine(SpeedBoostPowerupDown());
    }

    public IEnumerator SpeedBoostPowerupDown()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBostActive = false;
    }

    public void ShieldPowerupOn()
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(ShieldPowerupDown());
    }

    public IEnumerator ShieldPowerupDown()
    {
        yield return new WaitForSeconds(10.0f);
        isShieldActive = false;
        _shieldGameObject.SetActive(false);
    }

    public void Damage()
    {

        if (isShieldActive == true)
        {
            isShieldActive = false;
            _shieldGameObject.SetActive(false);
        }
        else
        {
            lives -= 1;
            _UIManager.UpdateLives(lives);

            _hitCount++;

            if (_hitCount == 1)
            {
                int randomEngineSelection = Random.Range(0, 2);
                _engineFailures[randomEngineSelection].SetActive(true);
            }
            else if (_hitCount >= 2)
            {
                _engineFailures[0].SetActive(true);
                _engineFailures[1].SetActive(true);
            }

            if (lives <= 0)
            {
                Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _UIManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
        }

    }

}
