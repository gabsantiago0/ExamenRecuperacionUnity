using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 0f;
    [SerializeField] private float maxSpeed, aceleration, curbing, deceleration; // Max speed of the player is set in the inspector
    [SerializeField] private float rotationGrades; // Rotation speed of the player is set in the inspector
    public float puntosExtraCadaXSeg = 10f;

    private Animator animator, fireAnimator;
    public bool isAlive = true;

    [SerializeField] private Laser laserPrefab; // Laser prefab is set in the inspector
    [SerializeField] private float laserOffset; // Laser offset is set in the inspector
    private Vector3 shootPosition;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        animator = GetComponent<Animator>(); // Get the animator
        fireAnimator = transform.GetChild(0).GetComponent<Animator>(); // Get the fire animator
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
        }
    }

    private void Move() // Player movement and rotation
    {
        float rotationMovement = Input.GetAxis("Horizontal");
        float movement = Input.GetAxis("Vertical");
        if (rotationMovement < 0) // If the player is rotating to the left
        {
            transform.Rotate(0.0f, 0.0f, rotationGrades * Time.deltaTime, Space.Self); // Rotate the player
            animator.SetTrigger("left");
        }
        else if (rotationMovement > 0) // If the player is rotating to the right
        {
            transform.Rotate(0.0f, 0.0f, -rotationGrades * Time.deltaTime, Space.Self); // Rotate the player
            animator.SetTrigger("right");
        } else
        {
            animator.SetTrigger("idle"); // If the player is not rotating, set the idle animation
        }

        if (movement > 0 && speed < maxSpeed) // If the player is moving forward and the speed is less than the max speed
        { 
            speed += aceleration * Time.deltaTime;
            fireAnimator.SetBool("going", true);
        } 
        else if(movement < 0 && speed > 0) // If the player is moving backwards and the speed is greater than 0
        {
            speed -= curbing * Time.deltaTime;
            fireAnimator.SetBool("going", false); 
        }
        else if (movement == 0 && speed > 0) // If the player is not moving and the speed is greater than 0
        {
            speed -= deceleration * Time.deltaTime;
            fireAnimator.SetBool("going", false);
        }
        else if (movement <= 0 && speed <= 0) // If the player is moving backwards or is not moving and the speed is less than or equal to 0
        {
            speed = 0f;
            fireAnimator.SetBool("going", false);
        }
        else if (movement > 0 && speed > maxSpeed) // If the player is moving forward and the speed is greater than the max speed
        {
            speed = maxSpeed;
            fireAnimator.SetBool("going", true);
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime); // Move the player forward or backwards depending on the speed
        if(Input.GetKeyDown(KeyCode.Space)) // If the player presses the space key to shoot
        {
            shootPosition = transform.GetChild(1).transform.position; // Get the position of the shoot point
            /* We use the laser pool to request a laser instead of instantiating a new one
            Laser laser = Instantiate(laserPrefab, shootPosition, transform.rotation);
            laser.Shoot(transform.up); // Shoot the laser
            */
            GameObject laser = LaserPool.Instance.RequestLaser(); // Request a laser from the pool
            laser.transform.position = shootPosition; // Set the position of the laser
            laser.transform.rotation = transform.rotation; // Set the rotation of the laser
            laser.GetComponent<Laser>().Shoot(transform.up); // Shoot the laser
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "limit") // If the player hits the limit, it will be teleported to the other side
        {
            if (collision.transform.position.x != 0) // If the limit is vertical
            {
                if(collision.transform.position.x > 0) // If the limit is on the right
                {
                    transform.position = new Vector3(transform.position.x * -1 + 0.1f, transform.position.y, 0); // Teleport to the left, adding 0.1f to avoid collision
                }
                else
                {
                    transform.position = new Vector3(transform.position.x * -1 - 0.1f, transform.position.y, 0); // Teleport to the right, substracting 0.1f to avoid collision
                }
            }
            else
            {
                if (collision.transform.position.y > 0) // If the limit is on the top
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y * -1 + 0.1f, 0); // Teleport to the bottom, adding 0.1f to avoid collision
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y * -1 - 0.1f, 0); // Teleport to the top, substracting 0.1f to avoid collision
                }
            }
        }

        if (collision.gameObject.CompareTag("EnemyGA") || collision.gameObject.CompareTag("EnemyR")) // If the player collides with an enemy
        {
            GameManager.instance.PerderVida();
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            GameManager.instance.PerderVida();
        }

    }

}
