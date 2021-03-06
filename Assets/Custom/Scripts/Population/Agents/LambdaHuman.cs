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

public class LambdaHuman : GenericAgent
{
    private float elapsedTime = 0;
    public GenericWeapon weapon;
	public GameObject transformationPrefab;
	public GameObject bulletPrefab;

    public void Start()
    {
		base.Start();
        speed = UnityEngine.Random.Range(70, 120 ) / 100.0f;  //TODO set human speed value
        weapon = Weapons.GetWeapon(Weapons.Weapon.Gun);
        state = States.Idle;
        target = null;
        targetTag = "zombie";
        healthPoints = 100;
        humanityRate = UnityEngine.Random.Range(40, 100);
		//Debug.Log("Just started as a " + gameObject.tag);
		StatusUpdater.humanCount++;
	}
	
    public override void Move()
    {
        if (StatusUpdater.zombiesCount + StatusUpdater.contaminatedCount > 75)
        {
            if (StatusUpdater.zombiesCount + StatusUpdater.contaminatedCount < 150)
            {
                speed = UnityEngine.Random.Range(20, 30) / 100.0f;
            }
            else
            {
                speed = UnityEngine.Random.Range(30, 50) / 100.0f;
            }
        }
        base.Move();
    }

	public override GenericWeapon Attack(float distance, Rigidbody target)
    {
        if ((cooldown < 0) && (weapon.Range >= distance))
        {
            //Debug.Log(" I, human tried to attack " + targetTag);
			GameObject go = (GameObject) GameObject.Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
			Rigidbody bullet = go.GetComponent<Rigidbody>();
			Vector3 velocity = target.position - this.transform.position;
			velocity.Normalize();
			bullet.AddForce(velocity * 10, ForceMode.VelocityChange);
			LookAt(target.position);
            cooldown = weapon.CoolDown;
            return weapon;
        }
        return null;        
    }

    public override void LiveOrDie()
    {
        if (humanityRate < 10)
        {
            //this object become of type zombie
            //Debug.Log("OH NOOOO I TURNED INTO A ZOMBIE !!!!");
			GameObject newZombie = (GameObject) GameObject.Instantiate(transformationPrefab, this.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
			newZombie.transform.parent = gameObject.transform.parent;
			Destroy(gameObject);
			StatusUpdater.humanCount--;
			StatusUpdater.contaminatedCount++;
			StatusUpdater.zombiesCount++;
        }
        else if (healthPoints < 0)
        {
            //TODO add this death to the human loss score
            Destroy(gameObject);
			//Debug.Log("OH NO I MET A HUMAN DEATH");
			StatusUpdater.humanCount--;
		}
	}
}


