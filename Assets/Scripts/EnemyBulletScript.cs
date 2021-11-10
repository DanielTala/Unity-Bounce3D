using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public float bulletSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }


    void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;          //launch the bullet forwards
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);                                //destroys the bullet if it collided with anything
    }  
}
