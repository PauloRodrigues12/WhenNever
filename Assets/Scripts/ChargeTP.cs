using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargeTP : MonoBehaviour
{
    //Tempo
    public float timeToCharge;
    public int timeBetweenWaves;
    private float timeActive;

    //Peças
    public bool hasPiece1;
    public bool hasPiece2;
    public bool hasPiece3;

    //Carregamento do Tp
    private bool hasCharged;
    private bool hasStartedCharge;
    
    //Inimigos
    public Transform[] spawnpoints;
    public GameObject[] enemies;

    //Sprites do TP
    public SpriteRenderer brokenTP;
    public SpriteRenderer completedTP;

    //Audio
    private AudioSource audioSource;
    public AudioClip tpSound;
    private bool hasPlayedSound;

    //Scripts
    private PlayerController player;
    private SelectWeapon selectWeapon;

    private LevelLoader levelLoader;

    public int NextScene;

    private void Start()
    {
        completedTP.enabled = false;

        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        selectWeapon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SelectWeapon>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    private void Update()
    {
        //Começar a contagem
        if (hasStartedCharge == true)
        {
            timeActive += Time.deltaTime;
        }        

        //Acabou a contagem
        if (timeActive >= timeToCharge)
            hasCharged = true;

        if (hasCharged == true)
        {
            //Audio
            if(hasPlayedSound == false)
                PlayTpSound();

            //Destruir todos os inimigos
            GameObject[] enemies_ = GameObject.FindGameObjectsWithTag("EnemyPrefab");
            for (int i = 0; i < enemies_.Length; i++)
            {
                Destroy(enemies_[i]);
            }

            //Passar para a proxima cena
            selectWeapon.currentWeapon = player.currentWeapon;
            levelLoader.NextScene = NextScene;
            levelLoader.wasFromTP = true;
            levelLoader.LoadNextScene();

            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (hasStartedCharge == false)
            {
                //Se tiver todas as peças
                if (hasPiece1 == true && hasPiece2 == true && hasPiece3 == true)
                {
                    //Começar sequência
                    StartCoroutine(startSequence());
                    hasStartedCharge = true;
                    brokenTP.enabled = false;
                    completedTP.enabled = true;
                }
            }
        }
    }

    private IEnumerator startSequence()
    {
        while (hasCharged == false)
        {
            //Inimigos
            for (int i = 0; i < spawnpoints.Length; i++)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnpoints[i].position, spawnpoints[i].rotation);
            }

            audioSource.Play();

            yield return new WaitForSeconds(timeBetweenWaves);
        }     
    }

    private void PlayTpSound()
    {
        audioSource.clip = tpSound;
        audioSource.Play();
        hasPlayedSound = true;
    }
}
