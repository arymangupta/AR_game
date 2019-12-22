using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

     //Public variables
    public float damageAmt=1;
    public float coolDownTimeInterval = 2f;

    public float muzzelFlash_Length=0.2f;

    public GameObject muzzelFlash;

    public bool canFire = true;

    public bool isAuthorised=false; // can a character use the weapon

    public GameObject bullet;

    public GameObject muzzelPoint;

    public enum WeaponCategory {
        PISTOL , RIFLE  , LASER  , MACHINEGUN
    }

    public WeaponCategory currentWeapon = WeaponCategory.PISTOL;

    public AudioSource weaponSound;

    //Private variables
    private Ray bullets;

    private float CoolDownTime;

    private   RaycastHit bulletPath;
    void Start()
    {
        /* 
        switch (currentWeapon){
            case WeaponCategory.PISTOL:
                coolDownTimeInterval = 0.5f;
            break;
        }
        */
        if(transform.tag.Equals("Player")){
            isAuthorised = true;
        }
        if(!muzzelFlash){
            Debug.LogError("Muzzel is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Do not let anyone fire the weapon unless they are authorised*/
        if(!isAuthorised){
             return;
        }

        /*Cooldown logic */
        if(Time.time < CoolDownTime) {
            canFire = false;
        }
        else canFire = true;

        TouchInput();
    }

    public void TouchInput() {
        if(Input.touchCount>0){
                 Touch touch = Input.GetTouch(0);
            switch(touch.phase){
                case  TouchPhase.Began:
                if(canFire) {
                     CoolDownTime = Time.time + coolDownTimeInterval;
                     Shoot();
                }
                break;
            }
        }
    }

    public void Shoot(){
        ShootRaysact();
    }
    public void ShootRaysact()
    {
        if (!canFire)
            return;

        if (weaponSound != null)
        {
            Debug.Log("Played");
            weaponSound.Play();
        }
        muzzelFlash.transform.localScale = Vector3.zero;
        iTween.PunchScale(muzzelFlash, Vector3.one, muzzelFlash_Length);

        bullets = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));

        if (Physics.Raycast(bullets, out bulletPath, 100))
        {
            if (bulletPath.transform.tag != "Enemy")
                return;
                Debug.Log("Hit");
             if(bulletPath.collider.GetType() == typeof(SphereCollider))
            {
                bulletPath.transform.GetComponent<Enemy>().ReceiveDamage(damageAmt);

            }
        }
    }

    public void ShootBullet(){
        GameObject temp = Instantiate(bullet , muzzelPoint.transform.position , Quaternion.identity);
        temp.GetComponent<Bullet>().Init();
    }

}
