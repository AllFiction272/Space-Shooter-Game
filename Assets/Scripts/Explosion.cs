using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator _explosionAnimation;
    // Start is called before the first frame update
    void Start()
    {
        _explosionAnimation = gameObject.GetComponent<Animator>();

        Destroy(this.gameObject, 1.0f);
    }

}
