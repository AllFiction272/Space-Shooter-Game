using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Behavor : MonoBehaviour
{
    [SerializeField]
    private float _asteroidSpeed = 3f;

    private Animator _asteroidDestroyed;

    private SpawnManager _spawnManager;

    private AudioSource _exlopdeAsteroidSound;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _asteroidDestroyed = gameObject.GetComponent<Animator>();
        _exlopdeAsteroidSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _asteroidSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D boom)
    {
        if(boom.tag == "Laser")
        {
            Destroy(boom.gameObject);

            _asteroidDestroyed.SetTrigger("On_Enemy_Death");

            _spawnManager.StartSpawning();

            Destroy(transform.GetComponent<BoxCollider2D>());

            _exlopdeAsteroidSound.Play();

            Destroy(this.gameObject, 1.0f);
        }
    }
}
