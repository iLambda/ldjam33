using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum FunctionMode
{
    Inverse,
    SqInverse,
    Gaussian
}

public sealed class DeflectorPotential : MonoBehaviour, IPotential
{
    // Attraction amplitude (A)
    public float Amplitude = 1;
    /* Function profile :
     *  A/d       : FunctionMode.Inverse
     *  A/d²      : FunctionMode.SqInverse
     *  Ae^(-x²)  : FunctionMode.Gaussian
     */
    public FunctionMode FunctionProfile;

    // Returns the deflectors potential
    public float GetPotential(float x, float y)
    {
        // Compute distance
        var xC = x - transform.position.x;
        var yC = y - transform.position.z;
        var sqDistance = (xC * xC) + (yC * yC);
        sqDistance = sqDistance != 0 ? sqDistance : 1;

        // Return the potential
        switch (this.FunctionProfile)
        {
            case FunctionMode.Inverse:
                return this.Amplitude / Mathf.Sqrt(sqDistance);
            case FunctionMode.SqInverse:
                return this.Amplitude / sqDistance;
            case FunctionMode.Gaussian:
                return this.Amplitude * Mathf.Exp(-sqDistance);
        }
        
        // Default
        return 0f;
    }
}
