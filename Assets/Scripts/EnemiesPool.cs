using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyYellow, enemyPink, enemyGreen;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> enemies;

    private static EnemiesPool instance;

    public static EnemiesPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddEnemiesToPool(poolSize);
        StartCoroutine(EnableEnemy());
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomEnemy = Random.Range(0, 3);
            GameObject enemy = null;
            switch (randomEnemy)
            {
                case 0:
                    enemy = Instantiate(enemyYellow);
                    break;
                case 1:
                    enemy = Instantiate(enemyPink);
                    break;
                case 2:
                    enemy = Instantiate(enemyGreen);
                    break;
            }
            enemy.SetActive(false);
            enemies.Add(enemy);
            enemy.transform.SetParent(transform);
        }
    }

    public GameObject RequestEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Count);
        for (int i = randomEnemy; i < enemies.Count; i++)
        {
            if (!enemies[i].activeSelf)
            {
                enemies[i].SetActive(true);
                return enemies[i];
            }
        }
        AddEnemiesToPool(1);
        enemies[enemies.Count - 1].SetActive(true);
        return enemies[enemies.Count - 1];
    }

    IEnumerator EnableEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            RequestEnemy();
        }
    }
}
