using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    private const string typeName = "AsteroidArena";
    private const string gameName = "RoomName";

    /*private void StartServer()
    {
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
        MasterServer.ipAddress = "127.0.0.1";
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();
        }
    }*/
}
