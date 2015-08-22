using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    // The start point
    public Transform StartPoint;
    // The end point 
    public Transform EndPoint;
    // The population
    public PopulationManager PopulationManager;

    public void Start()
    {
        transform.position = StartPoint.position;
    }

    public void Update()
    {
        // Getting body
        var body = GetComponent<Rigidbody>();

        // Computing derivatives
        var x = transform.position.x;
        var z = transform.position.z;
        var dXZ = 0.5f;
        var dUX = PopulationManager.GetPotential(x + dXZ, z) - PopulationManager.GetPotential(x - dXZ, z);
        var dUZ = PopulationManager.GetPotential(x, z + dXZ) - PopulationManager.GetPotential(x, z - dXZ);

        var lp = this.transform.position;
        lp.y = PopulationManager.GetPotential(x, z);
        this.transform.position = lp;

        var force = new Vector3(-dUX / (2 * dXZ), 0, -dUZ / (2 * dXZ));

        // Applying the force -grad U(p)
        body.AddForce(force, ForceMode.Force);
        
    }
}

