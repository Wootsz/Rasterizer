using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using OpenTK;

namespace template_P3
{
    public class Camera
    {
        Vector3 viewDirection, position;
        //Matrix4 mvp,  mperspective;
        Matrix4 mcamera;
        float velocity;

        public Camera(Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.viewDirection = direction;
            velocity = 1f;
        }

        public void HandleInput()
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();

            if (keyboard[OpenTK.Input.Key.Space])
                position.Y += velocity;
            if (keyboard[OpenTK.Input.Key.ShiftLeft])
                position.Y -= velocity;

            if (keyboard[OpenTK.Input.Key.Right])
            {
                viewDirection = CalcMethods.Normalize(viewDirection - CalcMethods.CrossProduct(viewDirection, Vector3.UnitY) * .05f);
            }
            if (keyboard[OpenTK.Input.Key.Left])
            {
                viewDirection = CalcMethods.Normalize(viewDirection + CalcMethods.CrossProduct(viewDirection, Vector3.UnitY) * .05f);
            }
            if (keyboard[OpenTK.Input.Key.Up])
            {
                viewDirection = CalcMethods.Normalize(viewDirection + CalcMethods.CrossProduct(viewDirection, Vector3.UnitX) * .05f);
            }
            if (keyboard[OpenTK.Input.Key.Down])
            {
                viewDirection = CalcMethods.Normalize(viewDirection - CalcMethods.CrossProduct(viewDirection, Vector3.UnitX) * .05f);
            }
            if (keyboard[OpenTK.Input.Key.D])
            {
                position.X += velocity * (float)(viewDirection.Z);
                position.Z -= velocity * (float)(viewDirection.X);
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
                position.X -= velocity * (float)(viewDirection.Z);
                position.Z += velocity * (float)(viewDirection.X);
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                position.X -= velocity * (float)(viewDirection.X);
                position.Z -= velocity * (float)(viewDirection.Z);
            }
            if (keyboard[OpenTK.Input.Key.W])
            {
                position.X += velocity * (float)(viewDirection.X);
                position.Z += velocity * (float)(viewDirection.Z);
            }
        }

        public Matrix4 Mcamera()
        {
            Vector3 X = CalcMethods.CrossProduct(-viewDirection, Vector3.UnitY), Y = CalcMethods.CrossProduct(viewDirection, Vector3.UnitX);

            mcamera = new Matrix4(  new Vector4(X.X, X.Y, X.Z, -position.X),
                                    new Vector4(Y.X, Y.Y, Y.Z, -position.Y),
                                    new Vector4(-viewDirection.X, -viewDirection.Y, -viewDirection.Z, -position.Z),
                                    new Vector4(0, 0, 0, 1));
            return mcamera;
        }

        //public Matrix4 Mperspective()
        //{
        //    Vector3 l = Vector3.Zero, r = Vector3.Zero, t = Vector3.Zero, b = Vector3.Zero, n = Vector3.Zero, f = Vector3.Zero;

        //    mperspective = new Matrix4( new Vector4(2 * n / (r - l), 0, (l + r) / (l - r), 0),
        //                                new Vector4(0, 2 * n / (t - b), (b + t) / (b - t), 0),
        //                                new Vector4(0, 0, (n + f) / (n - f), 2 * f * n / (f - n)),
        //                                new Vector4(0, 0, 1, 0));
        //    return mperspective;
        //}

        //public Matrix4 Mvp()
        //{
        //    Vector2 N = new Vector2(Game.screen.width, Game.screen.height);
        //    mvp = new Matrix4(  new Vector4(N.X / 2, 0, 0, N.X / 2),
        //                        new Vector4(0, N.Y / 2, 0, N.Y / 2),
        //                        new Vector4(0, 0, 1, 0),
        //                        new Vector4(0, 0, 0, 1));
        //    return mvp;
        //}

    }
}
