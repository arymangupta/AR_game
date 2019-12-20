using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Handle gun firing muzzel effect and use iTween
 Reduce enemy health
 update player health GUI 
 check if player is dead (if dead it tells GameManager) 
 */
public class PlayerAttack : MonoBehaviour
{

    

    public AudioSource PistolSound;
    public GameObject Muzzel;
    public float MuzzelFlash_Length=0.2f;
    public GameObject BulletHitEffect;
    public float BulletHitEffect_Length = 1f;
    
    float CoolDownTime,TimeInterval=0.2f;

    public float PlayerHealth=10f;

    public Image PlayerHealthUI;

    bool CanFire;

    public GameManager gameManager;

    private void Update()
    {
        Debug.Log(PlayerHealth);
        if (PlayerHealth <= 0)
        {
            gameManager.IsGameOver = true;
        }
        PlayerHealthUI.fillAmount = PlayerHealth / 10f;
        if(Time.time>CoolDownTime)
        {
            CanFire = true;
        }
        else
        {
            CanFire = false;
        }
    }


    public void Shoot()
    {
        if (!CanFire)
            return;

        if (PistolSound != null)
        {
            Debug.Log("Played");
            PistolSound.Play();
        }
        Muzzel.transform.localScale = Vector3.zero;
        iTween.PunchScale(Muzzel, Vector3.one, MuzzelFlash_Length);

        Ray Bullets = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));

        RaycastHit BulletPath;

        if (Physics.Raycast(Bullets, out BulletPath, 100))
        {
            if (BulletPath.transform.tag != "BombEnenmy_LVL_1")
                return;
            Debug.Log("Hit");
          //  GameObject BloodInstance = Instantiate(BulletHitEffect, BulletPath.point, Quaternion.LookRotation(-BulletPath.normal));
         //   iTween.PunchScale(BloodInstance, Vector3.zero, BulletHitEffect_Length);
          //  Destroy(BloodInstance, 1f);
            //if (BulletPath.collider.GetType() == typeof(CapsuleCollider))
            //{

            //    BulletPath.transform.GetComponent<BombEnemyLVL1>().ReceviedDamage(1);

            //}

             if (BulletPath.collider.GetType() == typeof(SphereCollider))
            {
                BulletPath.transform.GetComponent<BombEnemyLVL1>().ReceviedDamage(1);

            }
        }
       CoolDownTime = Time.time + TimeInterval;
    }

}
