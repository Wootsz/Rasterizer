using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Template_P3;

namespace Template_P3
{
    public class SceneGraph
    {
        public SceneGraph()
        {

        }

        public static void Render(Shader shader, Matrix4 matrix, Texture texture, Mesh mesh)
        {
            mesh.Render(shader, matrix, texture);
            Matrix4 newMatrix = matrix * mesh.modelView;
            
            for(int i = 0; i < mesh.children.Count; i++)
                SceneGraph.Render(shader, newMatrix, texture, mesh.children[i]);
        }
}
    }