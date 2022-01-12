using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //Propriedades da bala
    public int bulletDamage;
    public int bulletForce;
    public float bulletLifeSpan;
    public bool isExplosive;
    private bool isDestroying;
    public bool isLancer;

    //Inimigo
    public bool isEnemy;
    private EnemyBehavior enemy;

    //Animação de explosão
    private Animator explosionAnim;

    //Range de explosão
    private CircleCollider2D explosionRadius;

    //Audio
    private AudioSource audioSource;

    //PLayer
    private PlayerController playerController;

    void Start()
    {
        //Buscar animator
        explosionAnim = GetComponent<Animator>();

        //Buscar player
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Audio
        if (playerController.isRocket == true)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        if(isLancer == true)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        //Buscar range de explosão
        if (isExplosive == true)
        {
            explosionRadius = GetComponent<CircleCollider2D>();
            explosionRadius.enabled = false;
        }

        //Ignorar balas
        //Dos inimigos
        GameObject[] enemiesBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < enemiesBullets.Length; i++)
        {
            Physics2D.IgnoreCollision(enemiesBullets[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        //Do player
        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < playerBullets.Length; i++)
        {
            Physics2D.IgnoreCollision(playerBullets[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        //Se for inimigo as balas ignoram outros inimigos
        if (isEnemy == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Physics2D.IgnoreCollision(enemies[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

        //Se for player as balas do player ignoram o player
        if (isEnemy == false)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        //Começar a destruir a bala
        StartCoroutine(DestroyByTime());
    }

    private void FixedUpdate()
    {
        //Movimento da bala
        if (isDestroying == false)
            transform.Translate(bulletForce * Time.deltaTime * Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Retirar vida ao inimigo
        if (isEnemy == false)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemy = other.gameObject.GetComponent<EnemyBehavior>();
                enemy.enemyHp -= bulletDamage;
            }
        }

        //Retirar vida ao player
        if (isEnemy == true)
        {
            if (other.gameObject.tag == "Player")
                playerController.hp -= bulletDamage;
        }

        //Destruir bala
        Destroy();
    }

    //Comportamento do rocket
    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(bulletLifeSpan);

        if (isExplosive == true)
        {
            isDestroying = true;
            explosionRadius.enabled = true;

            //Fazer animação de explosão
            explosionAnim.Play("Explosion");

            Destroy(gameObject, explosionAnim.GetCurrentAnimatorStateInfo(0).length);
        }
        if (isExplosive == false)
            Destroy(gameObject);
    }

    private void Destroy()
    {
        if (isExplosive == true)
        {
            isDestroying = true;
            StartCoroutine(StartExplosionRadius());

            Destroy(gameObject, explosionAnim.GetCurrentAnimatorStateInfo(0).length);
            StopAllCoroutines();
        }
        if (isExplosive == false)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isEnemy == false)
            if (other.gameObject.tag == "Enemy")
            {
                //Buscar script do inimigo
                enemy = other.GetComponent<EnemyBehavior>();

                enemy.enemyHp -= bulletDamage / 2; 
            }
    }

    //Range da explosão
    private IEnumerator StartExplosionRadius()
    {
        if (isExplosive)
        {
            if (isDestroying == true)
            {
                yield return new WaitForSeconds(bulletLifeSpan);

                explosionRadius.enabled = true;
            }
        }
    }
}
