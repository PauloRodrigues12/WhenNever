using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    #region Variaveis

    //Movimento
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 1.5f;
    Vector2 movementDirection;
    Vector2 movementPerSecond;
    private NavMeshAgent agent;
    
    //Sprite do inimigo
    public GameObject spriteGameObject;
    private SpriteRenderer enemySpriteR;

    //Inimigo
    private Rigidbody2D rbEnemy;
    public GameObject parent;

    //Player
    private Transform target;
    private PlayerController playerController;

    //Bala do inimigo
    public GameObject bulletPrefab;
    public GameObject SpecialPrefab;
    public Transform[] firePoints;

    //Tipo de inimigo
    public bool isMelee;
    public bool isRanged;
    public bool isNinja;
    public bool isFlame;
    public bool isBoss;
    public bool isLancer;
    public bool isRex;
    public bool isTrump;
    public bool isHitler;

    //Ataque
    public int meleeDamage;
    public float attackCooldown;
    public float specialAttackCooldown;
    private bool canAttack = false;

    //Vida do inimigo
    public int enemyHp;
    public Slider enemyHpBar;

    //Spawn de inimgos
    public Transform[] summonLocation;
    public GameObject enemyPrefab;

    //Spawn de parede
    public Transform wallLocation;
    public GameObject wallPrefab;

    //Colisões
    private bool hasCollided;
    Vector2 hitDirection;

    //Animação
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    public AudioClip specialAudio;
    public AudioClip bossShoot;

    #endregion

    #region Start e Update

    private void Start()
    {
        latestDirectionChangeTime = 0f;
        
        //Player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = target.GetComponent<PlayerController>();

        //Inimigo
        rbEnemy = GetComponent<Rigidbody2D>();
        enemySpriteR = spriteGameObject.GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();

        //Animação
        animator = spriteGameObject.GetComponent<Animator>();

        //Audio
        audioSource = GetComponent<AudioSource>();

        if (isMelee == true)
            agent.Warp(new Vector3(this.transform.position.x, this.transform.position.y, 0));      

        //Atacar ranged
        if (isRanged == true)
        {
            calcuateNewMovementVector();
            StartCoroutine(Shoot());         
        }

        //Atacar melee
        if(isMelee == true)
            StartCoroutine(Attack());

        //Atacar Boss
        if(isBoss == true)
            StartCoroutine(SpecialAttack());

        //Atualizar a vida maxima do inimigo
        enemyHpBar.maxValue = enemyHp;
    }

    private void Update()
    {
        //Movimento Melee
        if (isMelee == true && isRanged == false)
        {
            agent.SetDestination(target.position);
        }

        //Movimento Ranged
        if (isRanged == true && isMelee == false)
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }

            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }

        //Movimento se for Melee e Ranged
        if (isMelee == true && isRanged == true)
        {
            agent.SetDestination(target.position);
        }

        //Flip do sprite
        if (this.transform.eulerAngles.z >= 0 && this.transform.eulerAngles.z <= 180)
            enemySpriteR.flipX = Physics2D.gravity.y < 0;
        else
            enemySpriteR.flipX = Physics2D.gravity.y > 0;

        //Rotação
        Vector2 lookDir = -(Vector2)transform.position - -(Vector2)target.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rbEnemy.rotation = angle;

        //Atualizar vida
        enemyHpBar.value = enemyHp;

        //Morrer
        if (enemyHp <= 0)
            Dead();
    }

    #endregion

    #region Colisoes e Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Atacar melee
        if (isMelee == true)
            if (other.gameObject.tag == "Player")
            {
                agent.isStopped = true;
                canAttack = true;

                animator.SetBool("Attack", true);
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Parar de atacar melee
        if(isMelee == true)
            if (other.gameObject.tag == "Player")
            {
                agent.isStopped = false;
                canAttack = false;

                animator.SetBool("Attack", false);
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isRanged == true)
        {
            if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            {
                hitDirection = (transform.position - other.transform.position).normalized;

                hasCollided = true;
                calcuateNewMovementVector();
            }
        }
    }

    #endregion

    #region IEnumerators

    private IEnumerator Attack()
    {
        while (true)
        {
            //Atacar player
            if (canAttack == true)
            {
                playerController.hp -= meleeDamage;

                //Audio
                if(isBoss == true)
                    audioSource.clip = bossShoot;

                audioSource.Play();
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            //Audio
            if (isBoss == true && isTrump == false)
                audioSource.clip = bossShoot;

            audioSource.Play();

            //Instaciar as balas do Ninja
            if (isNinja == true && isBoss == false)
            {
                animator.SetBool("Attack", true);

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                }

                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

                animator.SetBool("Attack", false);
            }

            //Ninja
            if(isFlame == true)
                for (int i = 0; i < 3; i++)
                { 
                    Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                }

            //Instanciar bala
            if (isNinja == false)
            {
                Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
            }

            if (isHitler == true)
            {
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

                animator.SetBool("Attack", false);
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private IEnumerator SpecialAttack()
    {
        while (true)
        {
            //Audio
            audioSource.clip = specialAudio;
            audioSource.Play();

            //Instanciar bala do boss Lancer
            if (isLancer == true)
            {
                animator.SetBool("Special", true);

                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

                Instantiate(SpecialPrefab, firePoints[0].position, firePoints[0].rotation);

                animator.SetBool("Special", false);
            }

            //Aumentar a velocidade e dano do boss Rex
            if (isRex == true)
            {
                agent.speed *= 3;
                meleeDamage *= 2;

                yield return new WaitForSeconds(specialAttackCooldown / 2);

                agent.speed /= 3;
                meleeDamage /= 2;
            }

            //Dar spawn a uma parede
            if (isTrump == true)
            {
                Instantiate(wallPrefab, wallLocation.position, spriteGameObject.transform.rotation);
            }

            //Dar spawn de inimigos
            if (isTrump == true || isHitler == true)
            {
                animator.SetBool("Special", true);

                for (int i = 0; i < summonLocation.Length; i++)
                {
                    Instantiate(enemyPrefab, summonLocation[i].position, spriteGameObject.transform.rotation);

                    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

                    animator.SetBool("Special", false);
                }
            }

            yield return new WaitForSeconds(specialAttackCooldown);
        }
    }

    #endregion

    #region Funcoes

    private void calcuateNewMovementVector()
    {
        if(hasCollided == true)
        {
            movementDirection = -hitDirection;
            movementPerSecond = movementDirection * agent.speed;

            hasCollided = false;
        }
        else
        {
            movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            movementPerSecond = movementDirection * agent.speed;
        }
    }

    private void Dead()
    {
        StopAllCoroutines();
        Destroy(parent.gameObject);
    }

    #endregion
}
