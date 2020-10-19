using System.Numerics;

namespace CSGO.Features
{
    public static class Vector
    {
        public static void Normalize(this Vector3 vector)
        {
            while (vector.Y < -180)
            {
                vector.Y += 360;
            }
            while (vector.Y > 180)
            {
                vector.Y -= 360;
            }
            if (vector.X > 89)
            {
                vector.X = 89;
            }
            if (vector.X < -89)
            {
                vector.X = -89;
            }
        }
    }
}
