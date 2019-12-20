using UnityEngine;
public class Enemy : Character
{
    // Start is called before the first frame update

    public float enemydistance;

    public float attackRange=1.5f;
    
    public float dyingAnimTime= 2f;

    public float damage=1f;

    void Start()
    {
        if(toFollow==null){
            toFollow = Camera.main.gameObject;
        }
        if(CharacterAnim==null){
            CharacterAnim = GetComponent<Animator>();
        }
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
        if(health<=0){
            DestroyCharacter();
        }
    }
    override public void DamageEffect(){
   
    }

    override public void DestroyCharacter(){
        isDead = true;
        CharacterAnim.SetBool("walk", false);
        CharacterAnim.SetBool("attack", true);
        Destroy(gameObject, dyingAnimTime);
     
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
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Player>().GiveDamage(damage);
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
