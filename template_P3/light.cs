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
        Vector4 position;
        Vector4 diffuse;
        Vector4 specular;
        float constantAttenuation, linearAttenuation, quadraticAttenuation;
        Vector3 attenuation;

        public Light(Vector4 position, Vector4 diffuse, Vector4 specularity, Vector3 attenuation)
        {
            this.position = position;
            this.diffuse = diffuse;
            specular = specularity;
            this.attenuation = attenuation;
            constantAttenuation = attenuation.X;
            linearAttenuation = attenuation.Y;
            quadraticAttenuation = attenuation.Z;
        }
    }
}
