using System;
using System.Collections.Generic;
using UnityEditor;

public class Weapons: MoveBehavior
{
    public enum Weapon{Fist, Gun, Bite};
	public static Dictionary<Weapon, GenericWeapon> type = new Dictionary<Weapon, GenericWeapon>();

	public void Awake ()
	{
        type.Add(Weapon.Fist, new GenericWeapon(5, 10, 10)); // TODO Gamedesign Values subject to future changes
		type.Add(Weapon.Gun, new GenericWeapon (3, 15, 15)); //TODO Gamedesign Values subject to future changes
        type.Add(Weapon.Bite, new GenericWeapon(5, 10, 5, 50)); //TODO Gamedesign Values subject to future changes
	}

	public static GenericWeapon GetWeapon(Weapon n){
		return type [n];
    }
}