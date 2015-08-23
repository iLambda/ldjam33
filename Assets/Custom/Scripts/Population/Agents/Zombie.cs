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
using UnityEngine;

public class Zombie : GenericAgent
{
    //attributes
    public int agressivityRate = 50; // over 100
    public GenericWeapon fistWeapon;
    public GenericWeapon biteWeapon;
	public void Start(){
        fistWeapon = Weapons.GetWeapon(Weapons.Weapon.Fist);
        biteWeapon = Weapons.GetWeapon(Weapons.Weapon.Bite);
		speed = UnityEngine.Random.Range(5,10)/100.0f; //TODO set zombie speed value
        state = States.Idle;
        target = null;
        targetTag = "human";
        healthPoints = 250;
        humanityRate = 0;
		Debug.Log("Just started as a " + gameObject.tag);
		StatusUpdater.zombiesCount++;
	}

    public override void Move()
    {
        base.Move();
    }

    public override GenericWeapon Attack(float distance)
    {
        GenericWeapon weaponUsed;
        int roll_dice = UnityEngine.Random.Range(1, 101);
        //If aggressive, higher probability to use fist attack to kill your opponent
        if (roll_dice < agressivityRate)
        {
            //fist
            weaponUsed = fistWeapon;
        }
        else //If quiet, higher probability to use bite attack to contaminate your opponent
        {
            //bite
            weaponUsed = biteWeapon;
        }

        if ((cooldown < 0) && (weaponUsed.Range >= distance))
        {
            Debug.Log(" I, zombie tried to attack " + targetTag + "at distance " + distance);
            cooldown = weaponUsed.CoolDown;
            return weaponUsed;
        }
        return null;
    }

    public override void LiveOrDie()
    {
        if (healthPoints < 0)
        {
            // TODO add this zombie death to the zombie counter
            Destroy(gameObject);
			Debug.Log("OH NO I MET ZOMBIE'S DEATH :'( ");
			StatusUpdater.zombiesCount--;
        }
    }

}


