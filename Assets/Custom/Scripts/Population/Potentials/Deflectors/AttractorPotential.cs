using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Custom.Scripts.Population.Potentials.Attractor
{
    public sealed class AttractorPotential : MonoBehaviour, IPotential
    {
        // Attraction amplitude (A)
        public float Amplitude = 1;
        // The caracteristic distance of the attractor (R)
        public float CaracteristicDistance = 1;

        // Returns the deflectors potential
        public float GetPotential(float x, float y)
        {
            // Compute distance
            var xC = x - transform.position.x;
            var yC = y - transform.position.y;
            var sqDistance = (xC * xC) + (yC * yC);
            sqDistance = sqDistance != 0 ? sqDistance : 1;

            // Use a newtonian potential low (A/(r/R)²)
            return this.Amplitude / (sqDistance / Mathf.Pow(this.CaracteristicDistance, 2));
        }
    }
}