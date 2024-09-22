using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]//damit kann ich es in der Engine sehen
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager; // damit die scrips mit einander kommunizieren
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private int _score;

    private AudioSource _laserSound, _explodeSound;

    private UI_Manager _uiManager; // zugang zum UI_Manager script

    private bool isTripleShotActive = false;
    private bool isSpeedPowerActive = false;
    private bool isShieldPowerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _laserSound = GetComponent<AudioSource>(); // laser sound wird gespielt wenn lertaste gedrücktz wird
        _explodeSound = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is NULL!");
        }

        if(_uiManager != null)
        {
            Debug.Log("The UI Manager is NULL");
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovemant();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void CalculateMovemant()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizantalInput = Input.GetAxis("Horizontal");

        //beides ist richtig der ganz unten ist besser optimiert

        //transform.Translate(Vector3.right * horizantalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizantalInput, verticalInput, 0);


        if (isSpeedPowerActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime); // geschwindigkeit und bewegung
        }
        else if (isSpeedPowerActive == true)
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime); // wenn SpeedPowerUp eingesammelt wurde verdopellt sich die geschwiendigkeit
        }

        //borders

        if (transform.position.y >= 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //der direkt untere code kann als ersatz für den überen Code benutzt werden den Mathf.Clamp nimmt eine positive zahl als auch negative 
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //telepotiert auf der anderen seite

        if (transform.position.x >= 12.5f)
        {
            transform.position = new Vector3(-12.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -12.5f)
        {
            transform.position = new Vector3(12.5f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (isTripleShotActive == true)
            {
               
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity); // tripleshot woper up wurde eingesammelt
              
            }
            else
            {
                
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

            

            _laserSound.Play();
        }
    }
    public void Damage()
    {
        if (isShieldPowerActive == true)
        {
            isShieldPowerActive = false;
            _shieldPrefab.SetActive(false); // ShildPowerUp wurde eingesammelt
            return;
        }

        _lives--; // Leben des Spielers

        if(_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEngine.SetActive(true);
        }


        _uiManager.UpdateLives(_lives); // die lebens sprite

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            _explodeSound.Play();

            Destroy(this.gameObject);
            
        }

    }
    public void TrippleShotPowerUp()
    {
        isTripleShotActive = true; // PowerUp wird aktiviert
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f); // Wenn das PowerUp eingesamelt wurden ist hält es für 5 Sekunden.
        isTripleShotActive = false;
    }
    public void SpeedPowerUp()
    {
        isSpeedPowerActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f); // speed power up wird akteviert für 5 sekunden
        isSpeedPowerActive = false;
        _speed = 5f;
    }
    public void ShieldPowerUp()
    {
        isShieldPowerActive = true; // shild wird aktiviert
        _shieldPrefab.SetActive(true);

    }
    public void AddScore(int points)
    {

        _score += points; // punkte werden hinzugefügt
        _uiManager.UpdateScore(_score);
    }
  

}
