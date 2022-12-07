using UnityEngine;

[SerializeField]
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
    public override string ToString()
    {
        return $"x: {X}, y: {Y}, z: {Z}";
    }
    public Vector3 ToVector3()
    {
        return new Vector3(X, Y, Z);
    }
    public static Vector3D FromVector3(Vector3 vector3)
    {
        return new Vector3D(vector3.x, vector3.y, vector3.z);
    }
}
