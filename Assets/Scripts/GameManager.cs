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

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
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
    public PlayerController Player1
    {
        get
        {
            return player1;
        }
    }
    [SerializeField]
    private PlayerController player2;
    public PlayerController Player2
    {
        get
        {
            return player2;
        }
    }
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private int winnerIDNum = 0;
    /*[SerializeField]
    private string dbIP = "0.0.0.0";
    [SerializeField]
    private InputField ipInput;
    [SerializeField]
    private InputField brokerIPInput;*/

    [SerializeField]
    private string userName = "b";
    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
        }
    }
    private string password = "444";
    private string email = "am2272-buyer@njit.edu";

    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private GameObject shopEntryPrefab;
    [SerializeField]
    private GameObject cardObj;
    public GameObject CardObj
    {
        get
        {
            return cardObj;
        }
    }
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private int totalMoney;
    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
    }
    [SerializeField]
    private Text[] totalMoneyTexts;
    private bool playerCreated = false;
    private bool turnExecuting = false;
    [SerializeField]
    private Text player1HealthText;
    [SerializeField]
    private Text player2HealthText;

	// Use this for initialization
	void Start ()
    {
        winnerIDNum = 0;
        if (playerPrefab != null)
        {
            if (playerCreated == false)
            {
                GameObject play = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
                int playerID = 0;
                if (LocalUserData.Instance.UserName == "b")
                {
                    playerID = 1;
                }
                else
                {
                    playerID = 2;
                }
                Debug.Log("Photon View ID: " + playerID.ToString());
                if (playerID == 1)
                {
                    play.GetComponent<PlayerController>().SetupPlayer(
                        1,
                        PlayerUIData.Instance.player1HPText,
                        PlayerUIData.Instance.player1PlatePanel,
                        PlayerUIData.Instance.player1DeckPanel,
                        PlayerUIData.Instance.player1BenchPanel,
                        PlayerUIData.Instance.player1DeckInfo
                        );
                    player1 = play.GetComponent<PlayerController>();
                }
                else
                {
                    play.GetComponent<PlayerController>().SetupPlayer(
                        2,
                        PlayerUIData.Instance.player2HPText,
                        PlayerUIData.Instance.player2PlatePanel,
                        PlayerUIData.Instance.player2DeckPanel,
                        PlayerUIData.Instance.player2BenchPanel,
                        PlayerUIData.Instance.player2DeckInfo
                        );
                    player2 = play.GetComponent<PlayerController>();
                }
                playerCreated = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player1 != null && player2 != null)
        {
            if (player1.CurrPlayerState == PlayerState.PS_Waiting && player2.CurrPlayerState == PlayerState.PS_Waiting)
            {
                if (player1.SelectedAction != Action.Action_ERROR && player2.SelectedAction != Action.Action_ERROR)
                {
                    ExecuteTurn();
                }
            }

            if (player1HealthText != null)
            {
                player1HealthText.text = player1.PlayerHP.ToString();
                if (player1.PlayerHP <= player1.StartingPlayerHP)
                {
                    float hp1 = player1.PlayerHP;
                    float hp2 = player1.StartingPlayerHP;
                    float ratio = hp1 / hp2;
                    player1HealthText.color = new Color(1.0f, 1.0f * ratio, 1.0f * ratio, 1.0f);
                }
                else
                {
                    player1HealthText.color = Color.cyan;
                }
            }
            if (player2HealthText != null)
            {
                player2HealthText.text = player2.PlayerHP.ToString();
                if (player2.PlayerHP <= player2.StartingPlayerHP)
                {
                    float hp1 = player2.PlayerHP;
                    float hp2 = player2.StartingPlayerHP;
                    float ratio = hp1 / hp2;
                    player2HealthText.color = new Color(1.0f, 1.0f * ratio, 1.0f * ratio, 1.0f);
                }
                else
                {
                    player2HealthText.color = Color.cyan;
                }
            }
        }
        else
        {
            PlayerController[] list = FindObjectsOfType<PlayerController>();
            foreach (PlayerController pc in list)
            {
                if (pc.PlayerNum == 1)
                {
                    player1 = pc;
                }
                else if (pc.PlayerNum == 2)
                {
                    player2 = pc;
                }
            }
        }
	}

    private IEnumerator ShowMeTheMoneyyyyyyyy()
    {
        string address = "http://10.0.0.34/it490group/PullUserData.php?username=" + userName + "?type=totalMoney";
        WWW request = new WWW(address);
        yield return request;
        if (!string.IsNullOrEmpty(request.text))
        {
            totalMoney = int.Parse(request.text);
        }
    }

    public void UpdateTotalMoney()
    {
        StartCoroutine(ShowMeTheMoneyyyyyyyy());
    }

    private IEnumerator FetchUserData(string type)
    {
        if (type == "password" || type == "totalMoney" || type == "username" || type == "email")
        {
            string address = "http://10.0.0.34/it490group/PullUserData.php?username=" + userName + "&type=" + type;
            WWW request = new WWW(address);
            yield return request;
            if (type == "password")
            {
                password = request.text;
            }
            else if (type == "totalMoney")
            {
                totalMoney = int.Parse(request.text);
            }
            else if (type == "username")
            {
                userName = request.text;
            }
            else if (type == "email")
            {
                email = request.text;
            }
        }
        else yield return null;
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

    private IEnumerator JustHangOnASecond(float time, Card player1ActiveCard, Card player2ActiveCard)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("updating statuses");
        player1.UpdatePlayerState(PlayerState.PS_Selecting);
        player1.UpdatePlayerAciton(Action.Action_ERROR);
        player2.UpdatePlayerState(PlayerState.PS_Selecting);
        player2.UpdatePlayerAciton(Action.Action_ERROR);

        //then check to see if the active card has died
        //if (player1.activeCard.IsCardDead())
        if (player1ActiveCard.IsCardDead())
        {
            player1.DestroyActiveCard();
        }
        //if (player2.activeCard.IsCardDead())
        if (player2ActiveCard.IsCardDead())
        {
            player2.DestroyActiveCard();
        }
        //and then fill hand with cards
        player1.FillHandWithCards();
        player2.FillHandWithCards();

        //check if a player has won
        //...because they have no more cards left?
        if (IsPlayerOutOfCards(player1))
        {
            winnerIDNum = 2;
        }
        if (IsPlayerOutOfCards(player2))
        {
            if (winnerIDNum == 2)
            {
                //both players are out of cards somehow
                //3 is the id for a draw
                winnerIDNum = 3;
            }
            else
            {
                winnerIDNum = 1;
            }
        }
        //...because a player is dead?
        if (player1.IsPlayerDead())
        {
            winnerIDNum = 2;
        }
        if (player2.IsPlayerDead())
        {
            if (winnerIDNum == 2)
            {
                //both players are out of cards somehow
                //3 is the id for a draw
                winnerIDNum = 3;
            }
            else
            {
                winnerIDNum = 1;
            }
        }

        turnExecuting = false;
    }

    public void ExecuteTurn()
    {
        if (turnExecuting)
        {
            Debug.Log("Turn already executing.");
            return;
        }
        Debug.Log("Executing turn...");
        Card player1ActiveCard = PlayerUIData.Instance.player1PlatePanel.gameObject.GetComponentInChildren<Card>();
        Card player2ActiveCard = PlayerUIData.Instance.player2PlatePanel.gameObject.GetComponentInChildren<Card>();

        if (player1ActiveCard == null || player2ActiveCard == null)
        {
            Debug.LogWarning("Warning! One of the active cards is null...restting appropriate flags.");
            if (player1ActiveCard == null)
            {
                //player1.CurrPlayerState = PlayerState.PS_Selecting;
                //player1.SelectedAction = Action.Action_ERROR;
                player1.UpdatePlayerState(PlayerState.PS_Selecting);
                player1.UpdatePlayerAciton(Action.Action_ERROR);
            }
            if (player2ActiveCard == null)
            {
                //player2.CurrPlayerState = PlayerState.PS_Selecting;
                //player2.SelectedAction = Action.Action_ERROR;
                player2.UpdatePlayerState(PlayerState.PS_Selecting);
                player2.UpdatePlayerAciton(Action.Action_ERROR);
            }
            return;
        }

        //cook first
        if (player1.SelectedAction == Action.Action_Cook)
        {
            player1.DoFusion(player1.CookBaseCard, player1.CookSpiceCard);
        }
        if (player2.SelectedAction == Action.Action_Cook)
        {
            player2.DoFusion(player2.CookBaseCard, player2.CookSpiceCard);
        }

        //attack second
        if (player1.SelectedAction == Action.Action_Attack)
        {
            //attack player 2's active card
            //int attackNum = player1.activeCard.AttackAmount;
            //int defenseNum = player2.activeCard.DefenseAmount;
            int attackNum = player1ActiveCard.AttackAmount;
            int defenseNum = player2ActiveCard.DefenseAmount;
            int damage = attackNum - defenseNum;
            //player2.activeCard.CardTakeDamage(1);
            player2ActiveCard.CardTakeDamage(1);
            //if (player2.activeCard.IsCardDead() && damage > 0)
            if (player2ActiveCard.IsCardDead() && damage > 0)
            {
                player2.PlayerTakeDamage(damage);
            }
        }
        if (player2.SelectedAction == Action.Action_Attack)
        {
            //int attackNum = player2.activeCard.AttackAmount;
            //int defenseNum = player1.activeCard.DefenseAmount;
            int attackNum = player2ActiveCard.AttackAmount;
            int defenseNum = player1ActiveCard.DefenseAmount;
            int damage = attackNum - defenseNum;
            //player1.activeCard.CardTakeDamage(1);
            player1ActiveCard.CardTakeDamage(1);
            //if (player1.activeCard.IsCardDead() && damage > 0)
            if (player1ActiveCard.IsCardDead() && damage > 0)
            {
                player1.PlayerTakeDamage(damage);
            }
        }

        //eat last
        if (player1.SelectedAction == Action.Action_Eat)
        {
            //int healAmount = player1.activeCard.CardValue;
            int healAmount = player1ActiveCard.CardValue;
            player1.PlayerHeal(healAmount);
            player1ActiveCard.CardHP = 0;
        }
        if (player2.SelectedAction == Action.Action_Eat)
        {
            //int healAmount = player2.activeCard.CardValue;
            int healAmount = player2ActiveCard.CardValue;
            player2.PlayerHeal(healAmount);
            player2ActiveCard.CardHP = 0;
        }

        //turn is over, reset flags for each player

        //player1.CurrPlayerState = PlayerState.PS_Selecting;
        //player1.SelectedAction = Action.Action_ERROR;
        //player2.CurrPlayerState = PlayerState.PS_Selecting;
        //player2.SelectedAction = Action.Action_ERROR;

        turnExecuting = true;
        StartCoroutine(JustHangOnASecond(0.5f, player1ActiveCard, player2ActiveCard));

        
    }

    public bool IsPlayerOutOfCards(PlayerController player)
    {
        int numCards = 0;

        if (player == null)
        {
            Debug.LogError("Error: Cannot determine if a null player is out of cards! Returning false...");
            return false;
        }

        numCards += player.DeckCount();
        numCards += player.HandCount();
        numCards += player.BenchCount();
        numCards += player.ActiveCardCount();

        Debug.Log("Player " + player.PlayerNum.ToString() + " has " + numCards.ToString() + " cards left.");

        return numCards == 0;
    }

    public void SetDatabaseIP(string ip)
    {
        //dbIP = ip;
    }

    public string GetDatabaseIP()
    {
        //return dbIP;
        return "";
    }

    public string GetBrokerIP()
    {
        //return brokerIPInput.text;
        return "";
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
                fuseable,
                int.Parse(cardInfo[7])
            );
            shopEntry.SetPrice(int.Parse(cardInfo[10]));
            newEntry.transform.SetParent(shopPanel.transform);
        }
    }

    private void BuildDeckInfo(string[] cardsInfo, string[] currentCardIDsInDeck)
    {
        Dictionary<string, int> myDeck = new Dictionary<string, int>();
        Dictionary<string, int> myNamedDeck = new Dictionary<string, int>();
        string result = "";

        foreach (string cardID in currentCardIDsInDeck)
        {
            if (myDeck.ContainsKey(cardID))
            {
                myDeck[cardID]++;
            }
            else
            {
                myDeck.Add(cardID, 1);
            }
            Debug.Log(cardID + ": " + myDeck[cardID].ToString());
        }

        foreach (string key in myDeck.Keys)
        {
            foreach (string str in cardsInfo)
            {
                Debug.Log(key + " " + str);
                string[] cardInfo = str.Split('|');
                if (cardInfo[0].ToString() == key)
                {
                    Debug.Log("it's a match!");
                    myNamedDeck.Add(cardInfo[1], myDeck[key]);
                    break;
                }
            }
        }

        foreach (string key in myNamedDeck.Keys)
        {
            Debug.Log(key + ": " + myNamedDeck[key].ToString());
            result += key + "x " + myNamedDeck[key].ToString() + "\n";
        }

        if (resultText != null)
        {
            resultText.text = result;
        }
    }

    private IEnumerator _BuildDeckInfo()
    {
        //string address = "http://" + ipInput.text + "/it490group/GetUserDecks.php?username=" + GetUsername() + "&id=0";
        string address = "http://10.0.0.34/it490group/GetUserDecks.php?username=" + GetUsername() + "&id=0";
        WWW request = new WWW(address);
        Debug.Log(address);
        yield return request;
        string[] currentDeckIDs = request.text.Split('|');
        //address = "http://" + ipInput.text + "/it490group/GetCardInfo.php";
        address = "http://10.0.0.34/it490group/GetCardInfo.php";
        //SetDatabaseIP(ipInput.text);
        Debug.Log(address);
        request = new WWW(address);
        yield return request;
        string data = request.text;
        string[] items = data.Split(';');
        BuildDeckInfo(items, currentDeckIDs);
    }

    public void BuildDeckInfoStart()
    {
        StartCoroutine(_BuildDeckInfo());
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(player1);
            //stream.SendNext(player2);
        }
        else
        {
            //player1 = (PlayerController)stream.ReceiveNext();
            //player2 = (PlayerController)stream.ReceiveNext();
        }
    }

    public void PlayerCook(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1.StartCook();
        }
        else
        {
            player2.StartCook();
        }
    }

    public void PlayerAttack(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1.SetAttack();
        }
        else
        {
            player2.SetAttack();
        }
    }

    public void PlayerEat(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1.SetEat();
        }
        else
        {
            player2.SetEat();
        }
    }
}
