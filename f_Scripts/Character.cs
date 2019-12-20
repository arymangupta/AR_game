using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(Animator))]
 public class Character : MonoBehaviour
{
   public float health;
   public Weapon weapon;
    public Animator CharacterAnim;
   public float moveSpeed;
   public float damageAmt;
    public bool isDead;
    public bool canAttack;

    public Image healtUI;
    public GameObject toFollow;

   virtual public void ReceiveDamage(float damage){
       return;
   }
   virtual public void DamageEffect(){
       return;
   }
   
   virtual public void GiveDamage(float damage){
       return;
   }

   virtual public void DestroyCharacter(){
       return;
   }

   virtual public void Init(){
       return;
   }

    virtual public void MoveCharacter(){
       return;
   }
}
