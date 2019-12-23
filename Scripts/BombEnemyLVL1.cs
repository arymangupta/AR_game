using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BombEnemyLVL1 : MonoBehaviour
{
    Camera ArCamera;
    public float speed = 1;
    float BombHealth=1;
    public Animator BombEnemyAnim,Point;
    bool isMoving,gameover;

    public static Action IsAttacking;
    public static Action IsNotAttacking;

    public static Action IsActive;
    public static Action IsEliminated;

    PlayerAttack playerAttack;
     GameManager gameManager;
    float Enemydistance;
    bool Fixe = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Point.gameObject.SetActive(false);
        ArCamera = Camera.main;
        playerAttack = ArCamera.GetComponent<PlayerAttack>();
        if (IsActive != null)
        {
            IsActive.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Speed" + speed);
        Vector3 direction = ArCamera.transform.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);

        if (isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            BombEnemyAnim.SetBool("walk", true);
        }
        else
        {
            BombEnemyAnim.SetBool("walk", false);
        }
        Vector3 distance = transform.position - Spwaner.hitposition;//POSITION OF ENEMY -POSITION OF PLAYER
        Enemydistance = Vector3.Distance(Spwaner.hitposition, transform.position);
        if (BombHealth > 0)
        {
            if (Enemydistance <= 1.5)
            {
                gameover = true;
                StartCoroutine("GameOVer");
                if (IsAttacking != null)
                {
                    IsAttacking.Invoke();
                }
                isMoving = false;

                Destroyer();

            }
            else
            {
                isMoving = true;
                if (IsNotAttacking != null)
                {
                    IsNotAttacking.Invoke();
                }
            }
        }
        else
        {
            Destroyer();
        }


    }

    public void ReceviedDamage(int DamageRecevied)
    {
        Debug.Log("Damage Recevied :" + DamageRecevied);
         BombHealth -= DamageRecevied;
        if (IsEliminated != null)
        {
            IsEliminated.Invoke();
        }

    }

    public void Destroyer()
    {
        bool Fixe = false;
    
        if (!Fixe)
        {
           Fixe = true;
            if (Enemydistance <= 1.5 )
            {
                playerAttack.PlayerHealth =playerAttack.PlayerHealth- 1;
              

            }
            else
            {
                if (playerAttack.PlayerHealth < 10)
                {
                    playerAttack.PlayerHealth = playerAttack.PlayerHealth + 0.5F;
                }
                Point.gameObject.SetActive(true);
                Point.SetTrigger("point");

            }
            var Colliders = GetComponents<Collider>();
            foreach (var col in Colliders)
            {
                col.enabled = false;
            }

            BombEnemyAnim.SetBool("attack", true);
        
            Destroy(gameObject, 2f);
            isMoving = false;
            speed = 0;
        } }

 IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        gameManager.IsGameOver = true;
    }
}

