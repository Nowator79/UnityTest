using System;
using System.Collections.Generic;
namespace NetWork.TypeJsonBody
{
    [Serializable]
    public struct World
    {
        public List<GameObject> objects;
        public World(List<GameObject> objects)
        {
            this.objects = objects;
        }
        public struct GameObject
        {
            public int Id;
            public Vector3D Position;
            public Vector3D Rotation;
            public GameObject(int Id, Vector3D Position, Vector3D Rotation)
            {
                this.Id = Id;
                this.Position = Position;
                this.Rotation = Rotation;
            }
        }
    }
}
