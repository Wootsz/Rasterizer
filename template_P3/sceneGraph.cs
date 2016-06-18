using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Template_P3;

namespace template_P3
{
    public class SceneGraph
    {
        public SceneGraph()
        {

        }

        public void Render(Matrix4 matrix, Mesh mesh)
        {
            Matrix4 newmatrix = matrix * mlocal
            mesh.Render(newmatrix);
            for(int i = 0; i < mesh.children.Count; i++)
                SceneGraph.Render(newmatrix, mesh.children[0]);
        }
    }
}