using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;
    [SerializeField] private GameObject limitLeft, limitRight, limitTop, limitBottom;
    public GameObject[] powerUpPrefabs; // Array con los prefabs de los power-ups
    public float[] probabilidades; // Array con las probabilidades de cada power-up


    private void OnEnable()
    {
        // Set the limits of the enemy's movement
        limitLeft.transform.position = new Vector2(limitLeft.transform.position.x, Random.Range(-5.5f, 5.5f)); // Set the limitLeft position
        limitRight.transform.position = new Vector2(limitRight.transform.position.x, Random.Range(-5.5f, 5.5f)); // Set the limitRight position
        limitTop.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), limitTop.transform.position.y); // Set the limitTop position
        limitBottom.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), limitBottom.transform.position.y); // Set the limitBottom position
        ResetPosition();
    }

    public void ResetPosition()
    {
        // Selects a random limit from the list to start form
        List<Transform> limits = new List<Transform> { limitLeft.transform, limitRight.transform, limitTop.transform, limitBottom.transform };
        int randomLimit = Random.Range(0, limits.Count);
        transform.position = limits[randomLimit].position;
    }

    void Update()
    {
        player = FindObjectOfType<Player>().transform;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser" || collision.gameObject.tag == "Player")
        {
            //GameManager.instance.sumarPuntos();
            gameObject.SetActive(false);

            soltarPowerUp();

        }
    }

    public void soltarPowerUp()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilidades.Length; i++)
        {
            cumulativeProbability += probabilidades[i];
            if (randomValue <= cumulativeProbability)
            {
                // Instancia el PowerUp en la posición del enemigo
                Instantiate(powerUpPrefabs[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
