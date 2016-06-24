using System;
using System.Collections.Generic;
using OpenTK;
using System.Diagnostics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace template_P3
{
    class Light
    {
        public Vector4 position;
        public Vector4 diffuse;
        public Vector4 specularity;
        public Vector3 attenuation;

        public Light(Vector4 position, Vector4 diffuse, Vector4 specularity, Vector3 attenuation)
        {
            this.position = position;
            this.diffuse = diffuse;
            this.specularity = specularity;
            this.attenuation = attenuation;
        }
    }
}
