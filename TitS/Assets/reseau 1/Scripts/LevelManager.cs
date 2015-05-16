using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public SmoothCameraWithBumper playerCam;
    public GameObject playerPrefab;
    public GameObject player2Prefab;
    public GameObject script_objet;
    public GameObject inst;
    public GameObject ennemy;
    public SpawnPoint[] spawnPoints;
    public int index;
    void Start()
    {
        index = 0;
        if (Network.isServer)
            SpawnPlayer();
        else
            Network.Connect(NetworkManager.GameToJoin);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Un nouveau joueur s'est connecté !");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {

        if (NetworkManager.GameToJoin != null)
            index = NetworkManager.GameToJoin.connectedPlayers;


        Debug.Log(index);
        
        var l = Network.Instantiate(inst, new Vector3(-16.70504f, 3.862434f, 1.250793f), new Quaternion(0f, 0f, 0f, 0f), 0) as GameObject;
        
        // Attention ici on utilise Network.Instanciate et pas Object.Instanciate.
        
        if (index == 0)
        {
            var player = Network.Instantiate(playerPrefab, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0) as GameObject;
            var j = Network.Instantiate(script_objet, new Vector3(-16.70504f, 3.862434f, 1.250793f), new Quaternion(0f, 0f, 0f, 0f), 0) as GameObject;
            var q = Network.Instantiate(ennemy, new Vector3(-10.28f, -5.192f, 1.62f), new Quaternion(0f, 0f, 0f, 0f), 0) as GameObject;
            var f = Network.Instantiate(ennemy, new Vector3(40.42f, -3.11f, -5.67f), new Quaternion(0f, 0f, 0f, 0f), 0) as GameObject;
            playerCam.target = player.transform;
        }
        if(index == 1)
        {
            var player2 = Network.Instantiate(player2Prefab, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0) as GameObject;
            playerCam.target = player2.transform;
        }
        
    }
}