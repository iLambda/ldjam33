using System;
using System.Collections.Generic;

public class GenericWeapon
{
    //This class implements the weapon type
    public double CoolDown;
    public float Range;
    public int Damages;
    public int Contagion;

    public GenericWeapon(double coo, float r, int d, int c=0) //TODO type is subject to ulteriors changes
    {
        CoolDown = coo;
        Range = r;
        Damages = d;
        Contagion = c;
    }
}
