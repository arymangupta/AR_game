using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float CoolDownTime;
    public float damageAmt=1;
    public float coolDownTimeInterval = 0.5f;

    public float muzzelFlash_Length=0.2f;

    public GameObject muzzel;

    public bool canFire = true;

    public bool isAuthorised=false; // can a character use the weapon

    public enum WeaponCategory {
        PISTOL , RIFLE  , LASER  , MACHINEGUN
    }

    public WeaponCategory currentWeapon = WeaponCategory.PISTOL;

    public AudioSource weaponSound;

    private Ray bullets;

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
        if(!muzzel){
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
        if(Input.touchCount>1){
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
    public void Shoot()
    {
        if (!canFire)
            return;

        if (weaponSound != null)
        {
            Debug.Log("Played");
            weaponSound.Play();
        }
        muzzel.transform.localScale = Vector3.zero;
        iTween.PunchScale(muzzel, Vector3.one, muzzelFlash_Length);

        bullets = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));

        if (Physics.Raycast(bullets, out bulletPath, 100))
        {
            if (bulletPath.transform.tag != "BombEnenmy_LVL_1")
                return;
            Debug.Log("Hit");

             if (bulletPath.collider.GetType() == typeof(SphereCollider))
            {
                bulletPath.transform.GetComponent<Enemy>().ReceiveDamage(damageAmt);

            }
        }
    }

}
