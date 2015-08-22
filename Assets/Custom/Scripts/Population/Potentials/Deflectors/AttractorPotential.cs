using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class AttractorPotential : MonoBehaviour, IPotential
{
    // Attraction amplitude (A)
    public float Amplitude = 1;

    // Returns the deflectors potential
    public float GetPotential(float x, float y)
    {
        // Compute distance
        var xC = x - transform.position.x;
        var yC = y - transform.position.z;
        var sqDistance = (xC * xC) + (yC * yC);
        sqDistance = sqDistance != 0 ? sqDistance : 1;

        // Use a newtonian potential law (A/r)
        return this.Amplitude / (sqDistance);
    }
}
