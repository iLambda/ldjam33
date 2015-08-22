using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Custom.Scripts.Tools
{
    public class Field3DMesh : MonoBehaviour
    {
        //Variables that store old resolution values
        private float oldResolutionX;
        private float oldResolutionY;

        // The function that will compute the field. It can
        // use time as a parameter.
        public Func<float, float, float> Field = (x, y) => Mathf.Cos(x + Time.time) + Mathf.Cos(y + Time.time);

        // Number of vertex along X axis
        public int ResolutionX;
        // Number of vertex along Y axis
        public int ResolutionY;
        // The part of the field you want to draw
        public Rect Boundaries;
                

        public void Start() 
        {
            // Creating a new mesh
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.name = "Field 3D";
            meshFilter.mesh = new Mesh();
            
            //Save data
            this.oldResolutionX = this.ResolutionX;
            this.oldResolutionY = this.ResolutionY;
            
            // Initialize mesh
            InitMesh();

            // Update mesh once
            UpdateMesh();
        }

        public void Update() 
        {

            // Updating it
            UpdateMesh();
		}

        public void InitMesh()
        {
            // Getting MeshFilter and vertices + tris
            var meshFilter = this.GetComponent<MeshFilter>();

            // X*Y vertices/UV. Since we assume the given field is
            // defined everywhere, and what we draw is isomorph to a plane, 
            // there is (X-1)*(Y-1) squares,  impying there is exactly
            // 2*(X-1)(Y-1) tris (3x more entries in the array).
            var vertices = new Vector3[ResolutionX * ResolutionY];
            var uvs = new Vector2[ResolutionX * ResolutionY];
            var triangles = new int[2 * 3 * (ResolutionX - 1) * (ResolutionY - 1)];

            // Since the triangles and UV of the mesh won't reorder, we set them here.
            for (int i = 0; i < this.ResolutionX; i++)
            {
                for (int j = 0; j < this.ResolutionY; j++)
                {
                    // We compute the index 
                    var vertexMatchingIndex = i + j * this.ResolutionX;

                    // We set the UV
                    uvs[vertexMatchingIndex] = new Vector2(i / this.ResolutionX, j / this.ResolutionY);

                    // We set triangles only if not on outer max edge
                    if (i < this.ResolutionX - 1 && j < this.ResolutionY - 1)
                    {
                        // Computing the step to place every vertex
                        var stepX = this.Boundaries.width / this.ResolutionX;
                        var stepY = this.Boundaries.height / this.ResolutionY;
                        var x = stepX * i;
                        var y = stepY * j;

                        // We compute the index
                        var triIndex = i + j * (this.ResolutionX - 1);

                        /* We map the triangles
                         * v    v+1
                         *  +---+
                         *  | \ |
                         *  +---+
                         * v+rX  v+rX+1
                         */
                        triangles[6 * triIndex] = vertexMatchingIndex + this.ResolutionX + 1; // The direct vertex
                        triangles[6 * triIndex + 1] = vertexMatchingIndex + 1; // The next vertex on X axis
                        triangles[6 * triIndex + 2] = vertexMatchingIndex; // The next vertex on X&Y axis
                        triangles[6 * triIndex + 3] = vertexMatchingIndex + this.ResolutionX; // The direct vertex
                        triangles[6 * triIndex + 4] = vertexMatchingIndex + this.ResolutionX + 1; // The next vertex on X axis
                        triangles[6 * triIndex + 5] = vertexMatchingIndex ; // The next vertex on Y axis
                    }
                }

            }

            // We set triangles and UVs
            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.triangles = triangles;
            meshFilter.mesh.uv = uvs;
        }

        public void UpdateMesh() 
        {
            //If values changed, we rebuild
            if (this.ResolutionX != oldResolutionX || this.ResolutionY != oldResolutionY)
                InitMesh();

            // Getting MeshFilter and vertices + tris
            var meshFilter = this.GetComponent<MeshFilter>();
            var vertices = meshFilter.mesh.vertices;

            // Rendering the mesh
            for (int i = 0; i < this.ResolutionX; i++)
			{
			    for (int j = 0; j < this.ResolutionY; j++)
			    {   
                    // We enumerate every vertex
			        var index = i + j * this.ResolutionX;
                    
                    // Computing the step to place every vertex
                    var stepX = this.Boundaries.width / this.ResolutionX;
                    var stepY = this.Boundaries.height / this.ResolutionY;
                    var x = stepX * i;
                    var y = stepY * j;

                    // We compute the height of every vertex
                    // The field must be defined
                    var z = this.Field(x + this.Boundaries.x, y + this.Boundaries.y);

                    // Setting the vertex
                    vertices[index] = new Vector3(x, z, y);
               
			    }
			} 

            // We set back the vertices (preferred way)
            meshFilter.mesh.vertices = vertices;

            //Recalculate normals & bounds
            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.RecalculateBounds();

            //Save data
            this.oldResolutionX = this.ResolutionX;
            this.oldResolutionY = this.ResolutionY;
        }
    }
}
