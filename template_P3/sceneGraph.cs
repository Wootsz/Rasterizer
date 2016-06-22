using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using template_P3;
using System.Collections.Generic;

namespace template_P3
{
    public class SceneGraph
    {
        public Dictionary<Mesh, List<Mesh>> children;

        public SceneGraph()
        {
            children = new Dictionary<Mesh, List<Mesh>>();
        }

        public void AddParent(Mesh mesh)
        {
            children.Add(mesh, null);
        }

        public void AddChild(Mesh parent, Mesh child)
        {
            if (children[parent] == null)
            {
                List<Mesh> children2 = new List<Mesh>();
                children2.Add(child);
            }
            else
                children[parent].Add(child);
            AddParent(child);
        }

        public void Render(Shader shader, Matrix4 matrix, Texture texture, Mesh mesh)
        {
            Matrix4 newMatrix = mesh.modelView * matrix;
            mesh.Render(shader, newMatrix, texture);
            List<Mesh> childz = children[mesh];
            if (childz != null)
            {
                foreach (Mesh m in childz)
                    this.Render(shader, newMatrix, texture, m);
            }
        }
    }
}