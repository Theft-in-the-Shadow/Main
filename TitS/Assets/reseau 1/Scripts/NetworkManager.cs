using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    public const string TypeName = "TheftInTheShadow";
    public static string GameName = "Nom du jeu";
    public static HostData GameToJoin = null;
    private HostData[] _hostData;
    private Rect _startBtnRect;
    private Rect _joinBtnRect;
    private Rect _cacheRect;
    private Rect _namegame;
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

        _namegame = new Rect(
              Screen.width - 500,
              Screen.height / 2 - 35, 200, 50);

        _cacheRect = new Rect(0, 0, 200, 50);
    }

    void OnGUI()
    {

        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(_startBtnRect, "Lancer le serveur"))
                StartServer();

            if (GUI.Button(_joinBtnRect, "Rafraichir la liste"))
                MasterServer.RequestHostList(TypeName);
            GameName = GUI.TextArea(_namegame, GameName, 200);
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