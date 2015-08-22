using System;
using System.Collections.Generic;

public class GenericWeapon
{
    //This class implements the weapon type
    public float CoolDown;
    public float Range;
    public float Damages;

    public GenericWeapon(float c, float r, float d) //TODO type is subject to ulteriors changes
    {
        CoolDown = c;
        Range = r;
        Damages = d;
    }
}
