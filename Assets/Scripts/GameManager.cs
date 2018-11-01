using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public enum TurnType
{
    TT_ERROR,
    TT_Attack,
    TT_Cook,
    TT_Eat
};

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    private PlayerController player1;
    [SerializeField]
    private PlayerController player2;
    [SerializeField]
    private string dbIP = "0.0.0.0";
    [SerializeField]
    private InputField ipInput;

    [SerializeField]
    private string userName = "b";
    private string password = "password";

    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private GameObject shopEntryPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Photon Network: trying to load a level, but we are not the master client");
        }
        Debug.LogFormat("PhotonNetwork: Loading Level: {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void ExecuteTurn()
    {

    }

    public void SetDatabaseIP(string ip)
    {
        dbIP = ip;
    }

    public string GetDatabaseIP()
    {
        return dbIP;
    }

    public string GetUsername()
    {
        return userName;
    }

    public void BuildCardInfo(string[] cardsInfo)
    {
        foreach (string str in cardsInfo)
        {
            string[] cardInfo = str.Split('|');
            if (cardInfo[0] == "")
            {
                break;
            }
            int fuseable_raw = int.Parse(cardInfo[6]);
            bool fuseable = false;
            if (fuseable_raw > 0)
            {
                fuseable = true;
            }
            string type = "CT_" + cardInfo[2];
            if (type == "CT_Super Food")
            {
                type = "CT_Superfood";
            }
            GameObject newEntry = Instantiate(shopEntryPrefab);
            ShopEntry shopEntry = newEntry.GetComponent<ShopEntry>();
            shopEntry.SetupMyCard
            (
                int.Parse(cardInfo[0]),
                cardInfo[1],
                cardInfo[8],
                (CardType)System.Enum.Parse(typeof(CardType), type),
                cardInfo[9],
                int.Parse(cardInfo[3]),
                int.Parse(cardInfo[4]),
                int.Parse(cardInfo[5]),
                fuseable
            );
            shopEntry.SetPrice(int.Parse(cardInfo[10]));
            newEntry.transform.SetParent(shopPanel.transform);
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }
}
