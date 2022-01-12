using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Velocidade do player
    public float speed;

    //Vida
    public int hp;

    //Texto de vida
    public TextMeshProUGUI hpNumber;

    //Joystick
    public MovementJoystick movementJoystick;

    //Player
    private Rigidbody2D rb;
    private GameObject spriteGO;

    //Animação
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    public AudioClip[] voiceLines;
    public float timeBetweenVoiceLines;

    //Armas
    private PlayerShooting playerShooting;
    public GameObject[] guns;
    private SelectWeapon selectWeapon;
    public int currentWeapon;

    public bool isRifle;
    public bool isMinigun;
    public bool isSniper;
    public bool isShotgun;
    public bool isRevolver;
    public bool isRocket;

    //Scene
    private LevelLoader levelLoader;

    #region Start e Update

    private void Awake()
    {
        //Ativar arma
        selectWeapon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SelectWeapon>();

        if (selectWeapon.currentWeapon == 0)
        {
            isRifle = true;
            currentWeapon = 0;
        }
        else if (selectWeapon.currentWeapon == 1)
        {
            isMinigun = true;
            currentWeapon = 1;
        }
        else if (selectWeapon.currentWeapon == 2)
        {
            isSniper = true;
            currentWeapon = 2;
        }
        else if (selectWeapon.currentWeapon == 3)
        {
            isShotgun = true;
            currentWeapon = 3;
        }
        else if (selectWeapon.currentWeapon == 4)
        {
            isRevolver = true;
            currentWeapon = 4;
        }
        else if (selectWeapon.currentWeapon == 5)
        {
            isRocket = true;
            currentWeapon = 5;
        }
    }

    private void Start()
    {
        //Esconder todas as armas menos a selecionada
        for (int i = 0; i < guns.Length; i++)
        {
            if (isRifle == true)
            {
                playerShooting = guns[0].GetComponent<PlayerShooting>();

                if (i != 0)
                    guns[i].SetActive(false);
            }
            if (isMinigun == true)
            {
                playerShooting = guns[1].GetComponent<PlayerShooting>();

                if (i != 1)
                    guns[i].SetActive(false);
            }
            if (isSniper == true)
            {
                playerShooting = guns[2].GetComponent<PlayerShooting>();

                if (i != 2)
                    guns[i].SetActive(false);
            }
            if (isShotgun == true)
            {
                playerShooting = guns[3].GetComponent<PlayerShooting>();

                if (i != 3)
                    guns[i].SetActive(false);
            }
            if (isRevolver == true)
            {
                playerShooting = guns[4].GetComponent<PlayerShooting>();

                if (i != 4)
                    guns[i].SetActive(false);
            }
            if (isRocket == true)
            {
                playerShooting = guns[5].GetComponent<PlayerShooting>();

                if (i != 5)
                    guns[i].SetActive(false);
            }
        }

        spriteGO = GameObject.Find("Player Sprite");
        rb = GetComponent<Rigidbody2D>();
        animator = spriteGO.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        StartCoroutine(VoiceLine());
    }

    private void FixedUpdate()
    {
        //Movimento
        if (movementJoystick.isMoving == true)
        {
            rb.velocity = new Vector2(movementJoystick.GetAxis("Horizontal") * speed,
                movementJoystick.GetAxis("Vertical") * speed);

            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }

    private void Update()
    {
        //Atualização da vida
        hpNumber.text = hp.ToString();

        //Morrer
        if (hp <= 0)
            Dead();
    }

    #endregion

    #region Funções

    private IEnumerator VoiceLine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenVoiceLines);

            audioSource.clip = voiceLines[Random.Range(0, voiceLines.Length)];
            audioSource.Play();
        }
    }

    private void Dead()
    {
        levelLoader.wasFromTP = true;
        levelLoader.NextScene = 6;
        levelLoader.LoadNextScene();
        Destroy(this.gameObject);
        playerShooting.StopAllCoroutines();
    }

    #endregion
}
