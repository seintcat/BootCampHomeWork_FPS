using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static bool soloMode;

    // Start is called before the first frame update
    void Start()
    {
        soloMode = false;
        //Connect();
    }

    // Update is called once per frame
    void Update()
    {
        //if(PhotonNetwork.IsConnected)
        //{
        //    print("true");
        //}
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");
        JoinLobby();
    }

    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");

        SceneManager.LoadScene("LoginScene");
    }

    public void MoveNext()
    {
        SceneManager.LoadScene("LoginScene");
        soloMode = true;
    }
}
