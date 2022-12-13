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
      
    }
}
