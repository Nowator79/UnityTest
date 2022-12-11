namespace NetWork.TypeJsonBody
{
    public struct ServerInfo
    {
        public int PlayerId;
        public string NameServer;
        public ServerInfo(int playerId, string nameServer)
        {
            PlayerId = playerId;
            NameServer = nameServer;
        }
    }
}
