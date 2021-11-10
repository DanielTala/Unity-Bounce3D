using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3[] points;

    private int point_number = 0;
    private Vector3 current_target;

    private float tolerance;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float delay_time;

    private float delay_start;

    public bool automatic;
    private bool playerTrigger;

    void Start()
    {
        if (points.Length > 0)
        {
            current_target = points[0];         //starts the current target at point 0
        }
        tolerance = speed * Time.deltaTime;
    }


    void Update()
    {
        if (transform.position != current_target)
        {
            MovePlatform();                             
        }
        else
        {
            UpdateTarget();                             
        }
    }

    void MovePlatform()
    {
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)                                                  //moves platform to the current_target
        {
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delay_start > delay_time)
            {
                NextPlatform();                                 //delay before identifying next target
            }
        }
        else
        {
            if(playerTrigger)
            {
                playerTrigger = false;                      //if not automatic, player must step on the platform for the platform to move to the next target
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        point_number++;
        if(point_number >= points.Length)
        {
            point_number = 0;                                        //updates the current target, if it's going back to the first target or the next target
        }

        current_target = points[point_number];
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;                     //sticks the player to the platform
        playerTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;                  //unsticks the player to the platform
    }
}
