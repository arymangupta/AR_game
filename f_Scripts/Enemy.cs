using UnityEngine;
public class Enemy : Character
{
    // Start is called before the first frame update

    public bool isMoving;
    public float enemydistance;

    public float attackRange=1.5f;

    void Start()
    {
        if(toFollow==null){
            toFollow = Camera.main.gameObject;
        }
        if(CharacterAnim==null){
            CharacterAnim = GetComponent<Animator>();
        }
         isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    override public  void Init() {
       
    }
    override public void ReceiveDamage(float damage) {
        health-=damage;
    }
    override public void DamageEffect(){
   
    }

    override public void DestroyCharacter(){
        isDead = true;
        CharacterAnim.SetBool("walk", false);
     
    }
    override public void GiveDamage(float damage){

    }

    override public void MoveCharacter()
    {
        if(isDead) return;

        /*enemy attack and enemy dead logic */
        enemydistance = Vector3.Distance(Spwaner.Hitposition, transform.position);
        if (health > 0)
        {
            if (enemydistance <= attackRange)
            {
                isMoving = false;
                GiveDamage(damageAmt);
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
        CharacterAnim.SetBool("walk", true);
       
        
       

    }
}
