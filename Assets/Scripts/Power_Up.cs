using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up : MonoBehaviour
{
    [SerializeField]
    private float _PowerUpSpeed = 3f;
    private Player _Player;
    [SerializeField]
    private int powerUpId;
    [SerializeField]
    private AudioClip _powerUpSound;

    // Update is called once per frame
    void Update()
    {
        //PowerUp geht nach unten und sobald es den Schirm berlästt wird es zerstört
        transform.Translate(Vector3.down * _PowerUpSpeed * Time.deltaTime);

        if (transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PowerUp wird aktiviert sobald es eingesammelt wird
        if (collision.tag == "Player")
        {

            _Player = GameObject.Find("Player").GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);

            if (_Player != null)
            {
                switch (powerUpId)
                {
                    case 0:
                        _Player.TrippleShotPowerUp();
                        break;
                    case 1:
                        _Player.SpeedPowerUp();
                        break;
                    case 2:
                        _Player.ShieldPowerUp();
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
