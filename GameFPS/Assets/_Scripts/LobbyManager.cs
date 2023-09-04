using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField roomNameInput;
    [SerializeField]
    private int maxPlayerNum = 5;
    [SerializeField]
    private TMP_InputField id;
    [SerializeField]
    private RoomManager roomManager;

    private string roomName;
    private string _id;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateRoom()
    {
        if (roomNameInput.text != "")
        {
            roomName = roomNameInput.text;
            _id = id.text;
            print("CreateRoom" + PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerNum }, null));
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        print(roomManager.ShowRoomInfo());
        //PhotonNetwork.Instantiate(_id, Vector3.zero, Quaternion.identity);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("OnJoinRandomFailed");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        print("OnLeftRoom");
    }
}
