using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_behavor : MonoBehaviour
{

    [SerializeField]
    private float _lspeed = 8f;

    private bool _isEnemyLaser = false;
    

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _lspeed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject); // So werden die TripleShot kopie gelöcht wenns auserhalb des randes sind
            }

            Destroy(this.gameObject);
            //this.GameObject geht auch ist sogar einfacher
        }
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * _lspeed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject); // So werden die TripleShot kopie gelöcht wenns auserhalb des randes sind
            }

            Destroy(this.gameObject);
            //this.GameObject geht auch ist sogar einfacher
        }
    }
    public void AssihnEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

        }
    }

}
