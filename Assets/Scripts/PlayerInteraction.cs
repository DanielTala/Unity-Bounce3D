using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float jumpPadHeight;

    [SerializeField]
    private float speedBoost;

    [SerializeField]
    private Text RingsNumber;

    private float playerSpeedHandler;
    PlayerMovement playerMovement;
    private int coins;
    private Transform checkPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("JumpPad"))
        {
            rb.AddForce(Vector3.up * jumpPadHeight);        // if the player collided with the Jump Pad the player will be launched upwards
        }

        if (collision.collider.tag.Equals("SpeedBoost"))
        {
            playerSpeedHandler = playerMovement.speed;
            playerMovement.speed *= 2;                      // if the player collided with the Speed Boost the player will have its speed doubled
            StartCoroutine("resetSpeed");
        }

        if (collision.collider.tag.Equals("FallReset") || collision.collider.tag.Equals("Bullet"))
        {
            transform.position = checkPoint.transform.position;
            rb.velocity = Vector3.zero;                             // if the player falls off the map or get hit by bullet,  the player will be respawned on the last checkpoint and reset the players velocity
            rb.angularVelocity = Vector3.zero;
        }
    }

    IEnumerator resetSpeed()
    {
        yield return new WaitForSeconds(3f);
        playerMovement.speed = playerSpeedHandler;                  //for the players movement speed to reset to its original value
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("CheckPoint"))
        {
            checkPoint = other.transform;                       // registers the latest checkpoint
        }

        if (other.tag.Equals("Coin"))
        {
            Destroy(other.gameObject);
            coins++;                                            //adds coins that was collected
            RingsNumber.text = coins.ToString();
        }

        if (other.tag.Equals("NextScene"))
        {
            SceneManager.LoadScene(1);                          //loads next scene
        }
    }

}
