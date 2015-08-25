using System;
using System.Collections.Generic;

public class Weapons: MoveBehavior
{
    public enum Weapon{Fist, Gun, Bite};
	public static Dictionary<Weapon, GenericWeapon> type = new Dictionary<Weapon, GenericWeapon>();

	public void Awake ()
	{   
		type.Clear();
		// cooldown (s), range, damage, contagion
        type.Add(Weapon.Fist, new GenericWeapon(1, 1, 50)); // TODO Gamedesign Values subject to future changes
		type.Add(Weapon.Gun, new GenericWeapon (0.5, 15, 25)); //TODO Gamedesign Values subject to future changes
        type.Add(Weapon.Bite, new GenericWeapon(1, 1, 30, 40)); //TODO Gamedesign Values subject to future changes
	}

	public static GenericWeapon GetWeapon(Weapon n){
		return type [n];
    }
}