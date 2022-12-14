using System;

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
            X = (float)Math.Round(Convert.ToDouble(vector.x), 3);
            Y = (float)Math.Round(Convert.ToDouble(vector.y), 3);
            Z = (float)Math.Round(Convert.ToDouble(vector.z), 3);
        }
        public UnityEngine.Vector3 GetVector3()
        {
            return new UnityEngine.Vector3(X, Y, Z);
        }
        public UnityEngine.Quaternion GetQuaternion()
        {
            return UnityEngine.Quaternion.Euler(GetVector3());
        }
    }
}
