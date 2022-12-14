using System;

namespace NetWork.TypeJsonBody
{
    public struct GameObject
    {
        public int Id;
        public int IdType;
        public Vector3D Position;
        public Vector3D Rotation;
        public long UpdateTime;
        public GameObject(int Id, int IdType, Vector3D Position, Vector3D Rotation)
        {
            this.Id = Id;
            this.IdType = IdType;
            this.Position = Position;
            this.Rotation = Rotation;
            UpdateTime = DateTime.Now.Ticks;
        }
    }
}