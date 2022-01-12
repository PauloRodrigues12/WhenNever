using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyWall : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;

    private void Start()
    {
        enemyBehavior = GameObject.Find("Enemy Controller Trump").GetComponent<EnemyBehavior>();

        StartCoroutine(StartDestroy());
    }

    //Destruir parede
    private IEnumerator StartDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBehavior.specialAttackCooldown);

            Destroy(this.gameObject);
            StopAllCoroutines();
        }
    }
}
