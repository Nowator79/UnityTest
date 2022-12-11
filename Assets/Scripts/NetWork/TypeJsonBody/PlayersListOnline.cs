using System;
using System.Collections.Generic;
namespace NetWork.TypeJsonBody { 

    [Serializable]
    public struct PlayersListOnline
    {
        public List<Player> PlayerList;
        [Serializable]
        public struct Player{
            public string Name;
        }
    }
}
