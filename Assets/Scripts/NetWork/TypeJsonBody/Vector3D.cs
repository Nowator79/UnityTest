using System.Numerics;

namespace NetWork.TypeJsonBody
{
    public struct Vector3D
    {
        public float X;
        public float Y;
        public float Z;
        public Vector3D(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y; 
            this.Z = Z;
        }
        public Vector3D(UnityEngine.Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }
}
