using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private bool hasActivated;
    private bool hasClearedRoom;

    public Transform[] spawnpoints;
    public GameObject[] enemies;
    public Transform bossSpawnpoint;
    public GameObject boss;

    public GameObject[] forcefields;

    private void Start()
    {
        for (int i = 0; i < forcefields.Length; i++)
        {
            forcefields[i].SetActive(false);
        }
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (hasActivated == true && enemies.Length == 0)
            hasClearedRoom = true;

        if(hasActivated == true && hasClearedRoom == true)
        {
            //Forcefields
            for (int i = 0; i < forcefields.Length; i++)
            {
                forcefields[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (hasActivated == false)
            {
                //Forcefields
                for (int i = 0; i < forcefields.Length; i++)
                {
                    forcefields[i].SetActive(true);
                }

                hasActivated = true;

                //Inimigos
                for (int i = 0; i < spawnpoints.Length; i++)
                {
                    Instantiate(enemies[Random.Range(0, enemies.Length)], spawnpoints[i].position, spawnpoints[i].rotation);
                }

                if (boss != null)
                    Instantiate(boss, bossSpawnpoint.position, bossSpawnpoint.rotation);
            }
        }
    }
}
