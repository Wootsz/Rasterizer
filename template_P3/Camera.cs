using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace template_P3
{
    class Camera
    {
        public Matrix4 cameramatrix;
        float velocity;

        public Camera()
        {
            cameramatrix = Matrix4.CreateTranslation(0, -4, -15);
            velocity = 0.5f;
        }

        public void HandleInput()
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            //changing the viewangle
            if (keyboard[OpenTK.Input.Key.Right])
                cameramatrix *= Matrix4.CreateRotationY((float)(3 * Math.PI / 180));
            if (keyboard[OpenTK.Input.Key.Left])
                cameramatrix *= Matrix4.CreateRotationY(-(float)(3 * Math.PI / 180));
            if (keyboard[OpenTK.Input.Key.Up])
                cameramatrix *= Matrix4.CreateRotationX(-(float)(3 * Math.PI / 180));
            if (keyboard[OpenTK.Input.Key.Down])
                cameramatrix *= Matrix4.CreateRotationX((float)(3 * Math.PI / 180));

            // moving the camera left, right, foreward and backwards
            if (keyboard[OpenTK.Input.Key.D])
                cameramatrix *= Matrix4.CreateTranslation(-velocity, 0, 0);
            if (keyboard[OpenTK.Input.Key.A])
                cameramatrix *= Matrix4.CreateTranslation(velocity, 0, 0);
            if (keyboard[OpenTK.Input.Key.S])
                cameramatrix *= Matrix4.CreateTranslation(0, 0, -velocity);
            if (keyboard[OpenTK.Input.Key.W])
                cameramatrix *= Matrix4.CreateTranslation(0, 0, velocity);

            // moving up and down
            if (keyboard[OpenTK.Input.Key.Space])
                cameramatrix *= Matrix4.CreateTranslation(0, -velocity, 0);
            if (keyboard[OpenTK.Input.Key.ShiftLeft])
                cameramatrix *= Matrix4.CreateTranslation(0, velocity, 0);

            // reset the camera position
            if (keyboard[OpenTK.Input.Key.R])
                cameramatrix = Matrix4.CreateTranslation(0, -4, -15);
        }
    }
}
