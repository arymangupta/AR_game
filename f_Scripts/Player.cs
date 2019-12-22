using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour
{
    // Public Variables
    public float health;
    public Animator CharacterAnim;
    public float moveSpeed;
    public bool isDead;
    public bool canAttack;


    // Private Variables
    private Weapon weapon;
   

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void Init() {
        weapon = GetComponent<Weapon>();
        isDead = false;
        weapon.isAuthorised = true;
    }
     public void ReceiveDamage(float damage) {
        DamageEffect();
        health-=damage;
        if(health<=0){
           DestroyCharacter();
        }
    }
     public void DamageEffect(){
   
    }

     public void DestroyCharacter(){
            isDead = true;
    }

    public void FireWeapon(){
        weapon.Shoot();
    }

}
