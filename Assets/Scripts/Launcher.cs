using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    private string gameVersion = "1";
    private bool isConnecting;

    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;

    const string playerNamePrefKey = "PlayerName";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Use this for initialization
    void Start ()
    {
        //Connect();
        if (progressLabel != null)
        {
            progressLabel.SetActive(false);
        }
        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Connect()
    {
        isConnecting = true;

        if (progressLabel != null)
        {
            progressLabel.SetActive(true);
        }
        if (controlPanel != null)
        {
            controlPanel.SetActive(false);
        }

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster(): called by PUN");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected(): called by PUN with reason {0}", cause);

        if (progressLabel != null)
        {
            progressLabel.SetActive(false);
        }
        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed(): called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom(): called by PUN, this client is now in a room.");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Loading 'Room1'");
            PhotonNetwork.LoadLevel("Room1");
        }
    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Player name is null or empty!");
            return;
        }
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(playerNamePrefKey, name);
    }
}
