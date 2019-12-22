using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    //public Variables
    public float bulletMoveSpeed=2f;
    public enum BulletCategory{
        NORMAL , LASER
    }
    public BulletCategory bulletCategory;

     //private Variables
    private Vector3 travelDir;
    private bool canTrvel;
    private Rigidbody myRigidBody;
    
    // Start is called before the first frame update
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        canTrvel = false;
    }
    // Update is called once per frame
    void Update()
    {
        TranslateBullet();
    }

    public void Init(BulletCategory category = BulletCategory.NORMAL){
        /*Set bullet characteristic */
        switch(category){
            case BulletCategory.LASER:
            //bulletMoveSpeed = 10f;
            break;
            case BulletCategory.NORMAL:
            //bulletMoveSpeed = 5f;
            break;
        }
        canTrvel = true;
    }

    void TranslateBullet(){
        if(!canTrvel) return;
        myRigidBody.AddForce (transform.forward*bulletMoveSpeed,ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
    }
}
