﻿using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{

    // public variables
    public float health = 1f;
    public float attackRange = 1.5f;
    public float moveSpeed = 1;
    public float dyingAnimTime = 2f;

    public float damage = 1f;
    public bool isDead = true;

    public bool ememyDebug = false;

    public int pixelOffset = 50;

    // Private variables
    private float enemydistance;
    private GameObject toFollow;
    private Animator characterAnim;
    private Renderer myRenderer;
    private ObjectPool myobjPool;
    private GameObject player;

    private Image myUI;



    void Start()
    {
        if (ememyDebug)
        {
            InitEmeny();
        }
    }
    public void InitEmeny(ObjectPool objectPool = null)
    {
        myUI = gameObject.GetComponentInChildren<Image>();
        myobjPool = objectPool;

        if (toFollow == null)
        {
            toFollow = Camera.main.gameObject;
        }
        if (characterAnim == null)
        {
            characterAnim = GetComponent<Animator>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        DisplayDirection();
    }
    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyCharacter();
        }
    }
    public void DamageEffect()
    {

    }

    public void DestroyCharacter()
    {
        isDead = true;
        characterAnim.SetBool("walk", false);
        characterAnim.SetTrigger("attack");
        if (myobjPool)
            myobjPool.DestroyGameObject(gameObject, dyingAnimTime);
        else
            Debug.LogError("Object pool is null");

    }
    public void GiveDamage(float damage)
    {

    }

    public void MoveCharacter()
    {
        if (isDead) return;

        /*enemy attack and enemy dead logic */
        enemydistance = Vector3.Distance(Spwaner.hitposition, transform.position);
        if (health > 0)
        {
            if (enemydistance <= attackRange)
            {

                player.GetComponent<Player>().ReceiveDamage(damage);
                DestroyCharacter();
            }
        }
        else
        {
            DestroyCharacter();
        }

        /* direction to look*/
        Vector3 direction = toFollow.transform.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);

        /*enemy movemen */
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        characterAnim.SetBool("walk", true);

    }

    public void SetMyObjectPool(ObjectPool objPool)
    {
        myobjPool = objPool;
    }

    void DisplayDirection()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!isVisible)
        {
            myUI.gameObject.SetActive(true);
            Vector2 screenCor = Camera.main.ViewportToScreenPoint(screenPoint);
            print("Before " + screenCor);
            screenCor.x = Mathf.Clamp(screenCor.x, pixelOffset, Screen.width - pixelOffset);
            if (screenPoint.z < 0)
            {
                screenCor.y = pixelOffset;
            }
            else
            {

                screenCor.y = Mathf.Clamp(screenCor.y, pixelOffset, Screen.height - pixelOffset);
            }
            print(screenCor);
            myUI.rectTransform.position = screenCor;

            //check if the enemy is right or letf to the player.
            if (screenPoint.x < 0)
            {

            }
            else if (screenPoint.x > 1)
            {

            }

        }
        else
        {
            myUI.gameObject.SetActive(false);
        }
    }
}
