using Assets.Custom.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PopulationPotentialRenderer : MonoBehaviour
{
    // The population manager
    public PopulationManager PopulationManager;

    public void Start()
    {
        // Set field
        var fieldMesh = GetComponent<Field3DMesh>();
        fieldMesh.Field = (x, y) => PopulationManager.GetPotential(x, y);
    }
}
