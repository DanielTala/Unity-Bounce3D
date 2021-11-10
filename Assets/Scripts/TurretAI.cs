using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float range = 15f;
    
    [SerializeField]
    private Transform turretHead;

    [SerializeField]
    private float turnSpeed = 10f;

    [SerializeField]
    private float attackSpeed = 1f;
    private float attackCountdown;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool attack;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        if ((Vector3.Distance(transform.position, player.transform.position)) <= range)
        {
            target = player.transform;                                                          //finds the player if it is in range
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;          //looks at the player if it is in range
        turretHead.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);


        if ((Vector3.Distance(transform.position, target.transform.position)) <= range)
        {
            attack = true;                                                                          //attacks the player if it is in range
        }
        else
        {
            attack = false;
        }

        if (attack)
        {
            if (attackCountdown <= 0f)
            {
                Shoot();                                                            //controlling of instantiation depending on the attackspeed
                attackCountdown = 1f / attackSpeed;
            }
            attackCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, turretHead.rotation);         //instantiates the bullet
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);                   //shows the range of the turret in the Gizmos
    }
}
