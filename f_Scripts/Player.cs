
public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public  void Init() {
        weapon = GetComponent<Weapon>();
        isDead = false;
    }
    override public void ReceiveDamage(float damage) {
        DamageEffect();
        health-=damage;
        if(health<=0){
           DestroyCharacter();
        }
    }
    override public void DamageEffect(){
   
    }

    override public void DestroyCharacter(){
            isDead = true;
    }

    public void FireWeapon(){
        weapon.Shoot();
    }

}
