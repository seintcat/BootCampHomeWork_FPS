using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = Instantiate(player);
        //PhotonNetwork.Instantiate(obj.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ShowRoomInfo()
    {
        if (PhotonNetwork.InRoom)
        {
            string roomName = PhotonNetwork.CurrentRoom.Name;
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;

            string playerName = "Player : ";
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerName += PhotonNetwork.PlayerList[i].NickName + ", ";
            }

            return roomName + "(" + playerCount + " / " + maxPlayer + ")\n" + playerName;
        }

        return "!";
    }

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
}
