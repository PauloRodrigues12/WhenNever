using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    private BulletBehavior bulletBehavior;

    public string enemyName;

    void Start()
    {
        enemyBehavior = GameObject.Find(enemyName).GetComponent<EnemyBehavior>();
    }

    //Comportamento para os inimigos levarem dano das balas do player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enemyBehavior.isMelee == true)
            if(other.gameObject.tag == "Bullet")
            {
                bulletBehavior = other.GetComponent<BulletBehavior>();

                if(bulletBehavior.isEnemy == false)
                    enemyBehavior.enemyHp -= bulletBehavior.bulletDamage;

                Destroy(other.gameObject);
            }
    }
}
