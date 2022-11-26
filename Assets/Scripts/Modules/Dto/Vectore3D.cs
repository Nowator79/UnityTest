using UnityEngine;

[SerializeField]
public class Vector3D
{
    public float X;
    public float Y;
    public float Z;
    public Vector3D()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }
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
}
