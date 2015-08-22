using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Custom.Scripts.Tools
{
    public class Field3DMesh : MonoBehaviour
    {
        private Func<float, float, float> field = (x, y) => 0;

        // The function that will compute the field. It can
        // use time as a parameter.
        public Func<float, float, float> Field 
        { 
            get { return field; } 
            set { field = value; } 
        }

        // Number of vertex along X axis
        public int ResolutionX { get; set; }
        // Number of vertex along Y axis
        public int ResolutionY { get; set; }
        // The part of the field you want to draw
        public Rect Boundaries { get; set; }
                

        public void Start() 
        {
            // Creating a new mesh
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            
            // X*Y vertices/UV. Since we assume the given field is
            // defined everywhere, and what we draw is isomorph to a plane, 
            // there is (X-1)*(Y-1) squares,  impying there is exactly
            // 2*(X-1)(Y-1) tris (3x more entries in the array).
            meshFilter.mesh.vertices = new Vector3[ResolutionX * ResolutionY];
            var uvs = new Vector2[ResolutionX * ResolutionY];
            var triangles = new int[2 * 3 *(ResolutionX - 1) * (ResolutionY - 1)];

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
                        var stepY = this.Boundaries.height/ this.ResolutionY;
                        var x = stepX * i;
                        var y = stepY * j;

                        // We compute the index
                        var triIndex = i + j * (this.ResolutionX - 1);

                        // We set the triangle
                        triangles[3 * triIndex] = vertexMatchingIndex; // The direct vertex
                        triangles[3 * triIndex + 1] = vertexMatchingIndex + 1; // The next vertex on X axis
                        triangles[3 * triIndex + 2] = vertexMatchingIndex + this.ResolutionX; // The next vertex on Y axis
                    }
                }   

            }

            // We set triangles and UVs
            meshFilter.mesh.triangles = triangles;
            meshFilter.mesh.uv = uvs;

            // Update mesh once
            UpdateMesh();
        }

        public void Update() 
        {
            // If we're in the editor, we don't re-render it every time
            if (Application.isEditor)
                return;

            // Updating it
            UpdateMesh();
		}

        public void UpdateMesh() 
        {
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
                    var stepY = this.Boundaries.height/ this.ResolutionY;
                    var x = stepX * i;
                    var y = stepY * j;

                    // We compute the height of every vertex
                    // The field must be defined
                    var z = this.Field(x, y);

                    // Setting the vertex
                    vertices[index] = new Vector3(x, z, y);
               
			    }
			} 

            // We set back the vertices (preferred way)
            meshFilter.mesh.vertices = vertices;
        }
    }
}
