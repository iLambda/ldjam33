//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

public class LambdaHuman : GenericAgent
{
    public int humanityRate = 100;
    private float elapsedTime = 0;

    public void Start()
    {
        speed = 0.5f; //TODO set zombie speed value
        weapon = Weapons.GetWeapon(Weapons.Weapon.Gun);
        state = States.Idle;
        target = null;
        targetTag = "zombie";
        nextpos = transform.position;
        healthPoints = 100;
        gameObject.tag = "human";
    }

    public void Move()
    {
        //TODO write random move function + obstacle avoidance

    }

    public void Attack()
    {
        //Debug.Log("Tried to attack " + targetTag );
        //TODO write Attack() function
        if (elapsedTime > weapon.CoolDown)
        {
            elapsedTime = 0;
        }
        
    }

    public void Die()
    {
        if (humanityRate < 10)
        {
            //this object become of type zombie
            gameObject.AddComponent<Zombie>();
            Destroy(this);
        }
        else
        {
            //TODO add this death to the human loss score
            Destroy(gameObject);
        }
    }
}

