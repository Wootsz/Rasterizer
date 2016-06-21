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

        public void AddChild(Mesh mesh1, Mesh mesh2)
        {
            if (children[mesh1] == null)
            {
                List<Mesh> child = new List<Mesh>();
                child.Add(mesh2);
            }
            else
                children[mesh1].Add(mesh2);
            AddParent(mesh2);
        }

        public void Render(Shader shader, Matrix4 matrix, Texture texture, Mesh mesh)
        {
            Matrix4 newMatrix = matrix * mesh.modelView;
            mesh.Render(shader, newMatrix, texture);
            List<Mesh> childs = children[mesh];
            if (childs != null)
            {
                foreach (Mesh m in childs)
                    this.Render(shader, newMatrix, texture, m);
            }
        }
    }
}