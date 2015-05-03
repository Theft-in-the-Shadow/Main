using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    public const string TypeName = "TheftInTheShadow";
    public static string GameName = "GameName";
    public static HostData GameToJoin = null;
    private HostData[] _hostData;
    private Rect _startBtnRect;
    private Rect _joinBtnRect;
    private Rect _cacheRect;
    public string levelToLoad;
    public Rect _bgRect = new Rect(0,0,Screen.width,Screen.height);
    public Texture background;

    void Start()
    {
        // Rectangles pour les boutons
        _startBtnRect = new Rect(
              Screen.width - 250,
              Screen.height / 2 - 35, 200, 50);


        _joinBtnRect = new Rect(
              Screen.width - 250,
              Screen.height / 2 + 35, 200, 50);

        _cacheRect = new Rect(0, 0, 200, 50);
    }

    void OnGUI()
    {
        GUI.DrawTexture(_bgRect, background);

        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(_startBtnRect, "Start Server"))
                StartServer();

            if (GUI.Button(_joinBtnRect, "Refresh List"))
                MasterServer.RequestHostList(TypeName);

            if (_hostData != null)
            {
                for (int i = 0, l = _hostData.Length; i < l; i++)
                {
                    _cacheRect.x = 15;
                    _cacheRect.y = Screen.height / 2 + (55 * i);

                    if (GUI.Button(_cacheRect, _hostData[i].gameName))
                        JoinServer(_hostData[i]);
                }
            }
        }
    }
    private void StartServer()
    {
        if (!Network.isClient && !Network.isServer)
        {
            Network.InitializeServer(4, 2500, !Network.HavePublicAddress());
            MasterServer.RegisterHost(TypeName, GameName);
        }
    }

    void OnServerInitialized()
    {
        Application.LoadLevel(levelToLoad);
    }
    private void JoinServer(HostData gameToJoint)
    {
        GameToJoin = gameToJoint;
        Application.LoadLevel(levelToLoad);
    }

    void OnMasterServerEvent(MasterServerEvent sEvent)
    {
        if (sEvent == MasterServerEvent.HostListReceived)
            _hostData = MasterServer.PollHostList();
    }
}