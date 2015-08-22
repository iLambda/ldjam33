using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IPotential
{
    // Returns the deflectors potential
    float GetPotential(float x, float y);
}
