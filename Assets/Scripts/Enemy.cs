using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _enemyDeath;
    [SerializeField]
    private float _enemySpeed = 4f;
    private Player _Player;
    private AudioSource _explodeSound;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;


    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();

        _enemyDeath = gameObject.GetComponent<Animator>();

        _explodeSound = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser_behavor[] lasers = enemyLaser.GetComponentsInChildren<Laser_behavor>(); 

            for (int i = 0; i < lasers.Length; i++) // laser wird immer wieder abgeschossen aka kopiert
            {
                lasers[i].AssihnEnemyLaser();
            }

        }

    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8.2f, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage(); // Spieler nimmt schaden
            }

            _enemyDeath.SetTrigger("On_Enemy_Death");

            Destroy(transform.GetComponent<BoxCollider2D>());

            _explodeSound.Play();

            Destroy(this.gameObject, 1.0f);
        }

        if (other.tag == "Laser")
        {
           
            Destroy(other.gameObject); // Gegner wird zerstört wenn er vom laser getrofen wurden ist

            if (_Player != null)
            {
                _Player.AddScore(10); // Punkte wenn ein gegner zerstört worden ist
            }

            _enemyDeath.SetTrigger("On_Enemy_Death");

            Destroy(transform.GetComponent<BoxCollider2D>());

            _explodeSound.Play();

            Destroy(this.gameObject, 1.0f);

        }
    }
 
}
