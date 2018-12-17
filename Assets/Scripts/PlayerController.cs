using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public enum PlayerState
{
    PS_ERROR,
    PS_Selecting,
    PS_Cook,
    PS_Waiting
}

public enum Action
{
    Action_ERROR,
    Action_Cook,
    Action_Attack,
    Action_Eat
}

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject LocalPlayerInstance;

    [SerializeField][Range(1, 2)]
    private int playerNum = 1;
    public int PlayerNum
    {
        get
        {
            return playerNum;
        }
    }
    [SerializeField]
    private string playerName = "Player";
    [SerializeField]
    private int playerHP = 20;
    public int PlayerHP
    {
        get
        {
            return playerHP;
        }
    }
    [SerializeField]
    private Text playerHPText;
    public Text PlayerHPText
    {
        get
        {
            return playerHPText;
        }
    }
    private int startingPlayerHP;
    public int StartingPlayerHP
    {
        get
        {
            return startingPlayerHP;
        }
    }
    [SerializeField]
    private int maxPlayerHP = 30;
    [SerializeField]
    private PlayerState playerState;
    public PlayerState CurrPlayerState
    {
        get
        {
            return playerState;
        }
        set
        {
            playerState = value;
        }
    }
    [SerializeField]
    private Action selectedAction;
    public Action SelectedAction
    {
        get
        {
            return selectedAction;
        }
        set
        {
            selectedAction = value;
        }
    }

    [SerializeField]
    private string rawDeckData = "";
    private Stack<Card> deck = new Stack<Card>();
    private Card[] hand = new Card[5];
    public Card[] Hand
    {
        get
        {
            return hand;
        }
    }
    public Card activeCard;
    private Card cookBaseCard;
    public Card CookBaseCard
    {
        get
        {
            return cookBaseCard;
        }
    }
    private Card cookSpiceCard;
    public Card CookSpiceCard
    {
        get
        {
            return cookSpiceCard;
        }
    }
    [SerializeField]
    private GameObject playerPlatePanel;
    public GameObject PlayerPlatePanel
    {
        get
        {
            return playerPlatePanel;
        }
    }
    private Card[] bench = new Card[3];
    public Card[] Bench
    {
        get
        {
            return bench;
        }
    }
    private Stack<Card> discardPile = new Stack<Card>();
    [SerializeField]
    private GameObject playerDeckPanel;
    public GameObject PlayerDeckPanel
    {
        get
        {
            return playerDeckPanel;
        }
    }
    [SerializeField]
    private GameObject playerBenchPanel;
    public GameObject PlayerBenchPanel
    {
        get
        {
            return playerBenchPanel;
        }
    }
    [SerializeField]
    private Text playerDeckInfo;

    private bool deckFinishedBuilding = false;

    void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        startingPlayerHP = playerHP;

        playerName = LocalUserData.Instance.UserName;
        GameManager.Instance.UserName = playerName;
        SetupPlayerDeck();
        //StartCoroutine(GetUsernameFromSession());
        //DoFusion(null, null);
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (playerName == "b")
        {
            playerNum = 1;
        }
        else
        {
            playerNum = 2;
        }

        if (deckFinishedBuilding)
        {
            for (int i = 0; i < 5; i++)
            {
                PullNextCardFromDeck();
            }
            deckFinishedBuilding = false;
        }

        if (playerDeckInfo/*[playerNum - 1]*/ != null)
        {
            playerDeckInfo/*[playerNum - 1]*/.text = "Cards left x" + deck.Count.ToString();
        }
        /*if (playerHPText != null)
        {
            playerHPText.text = playerHP.ToString();
            if (playerHP <= startingPlayerHP)
            {
                float hp1 = playerHP;
                float hp2 = startingPlayerHP;
                float ratio = hp1 / hp2;
                playerHPText.color = new Color(1.0f, 1.0f * ratio, 1.0f * ratio, 1.0f);
            }
            else
            {
                playerHPText.color = Color.cyan;
            }
        }*/
    }

    public void SetupPlayer(int _playerNum, Text _playerHPText, GameObject _plate, GameObject _hand, GameObject _bench, Text _deckInfoText)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        playerNum = _playerNum;
        playerHPText = _playerHPText;
        playerPlatePanel = _plate;
        playerDeckPanel = _hand;
        playerBenchPanel = _bench;
        playerDeckInfo = _deckInfoText;
    }

    public IEnumerator GetUsernameFromSession()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            yield break;
        }

        string address = "http://10.0.0.34/it490group/GetUsername.php";
        WWW request = new WWW(address);
        yield return request;
        Debug.LogFormat("GetUsernameFromSession: {0}", request.text);
        if (!string.IsNullOrEmpty(request.text))
        {
            playerName = request.text;
            GameManager.Instance.UserName = playerName;
        }
    }

    public GameObject CreateCardFromData(string data)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return null;
        }

        Debug.LogFormat("CreateCardFromData {0}", data);
        string[] cardInfo = data.Split('|');
        if (cardInfo[0] == "")
        {
            return null;
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

        //Card newCard = new Card();
        GameObject newCard = PhotonNetwork.Instantiate(GameManager.Instance.CardObj.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        //newCard.transform.SetParent(playerDeckPanels[playerNum - 1].transform);
        Card _newCard = newCard.GetComponent<Card>();
        //Card _newCard = new Card();
        _newCard.SetupCard(
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
        //_newCard.SetCardOwnership(playerNum);
        if (LocalUserData.Instance.UserName == "b")
        {
            _newCard.SetCardOwnership(1);
        }
        else
        {
            _newCard.SetCardOwnership(2);
        }
        return newCard;
    }

    private IEnumerator GetFullPlayerDeck()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            yield break;
        }

        //string address = "http://" + GameManager.Instance.GetDatabaseIP() + "/it490group/BuildFullUserDeck.php?username=b&id=0";
        string address = "http://10.0.0.34/it490group/BuildFullUserDeck.php?username=" + LocalUserData.Instance.UserName + "&id=0";
        //string address = "https://web.njit.edu/~mwk9/it490group/BuildFullUserDeck.php?username=b&id=0";
        //TODO: change this ^^^ to take username from PHP
        WWW request = new WWW(address);
        yield return request;
        rawDeckData = request.text;
        Debug.Log(rawDeckData);

        string[] cards = rawDeckData.Split(';');
        foreach (string card in cards)
        {
            Debug.Log(card);
            string[] cardInfo = card.Split('|');
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

            //Card newCard = new Card();
            GameObject newCard = PhotonNetwork.Instantiate(GameManager.Instance.CardObj.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            //newCard.transform.SetParent(playerDeckPanels[playerNum - 1].transform);
            Card _newCard = newCard.GetComponent<Card>();
            //Card _newCard = new Card();
            _newCard.SetupCard(
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
            //_newCard.SetCardOwnership(playerNum);
            if (LocalUserData.Instance.UserName == "b")
            {
                _newCard.SetCardOwnership(1);
            }
            else
            {
                _newCard.SetCardOwnership(2);
            }
            deck.Push(_newCard);
            PhotonNetwork.Destroy(newCard);
            /*GameObject newCard = CreateCardFromData(card);
            deck.Push(newCard.GetComponent<Card>());*/
        }

        deckFinishedBuilding = true;
        playerState = PlayerState.PS_Selecting;
    }

    void SetupPlayerDeck()
    {
        /*for (int i = 0; i < 20; i++)
        {
            Card card = new Card();
            card.SetupCard(8, "Flour", "Grainy.", CardType.CT_Base, "Sprites/CardImgs/flour", 2, 2, 2, true);
            deck.Push(card);
            if (i % 2 == 0)
            {
                Card card2 = new Card();
                card2.SetupCard(10, "Maple Syrup", "Canada's greatest export", CardType.CT_Spice, "Sprites/CardImgs/maplesyrup", 0, 0, 4, true);
                deck.Push(card2);
            }
        }*/
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        Debug.Log("SetupPlayerDeck: Player #" + playerNum.ToString());
        StartCoroutine(GetFullPlayerDeck());
    }

    public void PullNextCardFromDeck()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        GameObject newCard = PhotonNetwork.Instantiate(GameManager.Instance.CardObj.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        Card c;
        int i = 0;

        if (deck.Count > 0)
        {
            c = deck.Pop();
        }
        else
        {
            Debug.LogWarning("Warning: no more cards in the Deck for player " + playerNum.ToString());
            return;
        }
        c.SetCardOwnership(playerNum);
        for (i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                Debug.Log("Pull next card from deck: Is c null? " + (c == null).ToString());
                hand[i] = c;
                c.cardStatus = CardStatus.CS_Hand;
                c.Index = i;
                break;
            }
        }
        if (i >= hand.Length)
        {
            //something went wrong, pulled card when hand was full
            deck.Push(c);
            PhotonNetwork.Destroy(newCard);
            return;
        }
        newCard.GetComponent<Card>().SetupCardFromCard(c);
        newCard.GetComponent<Card>().cardStatus = CardStatus.CS_Hand;
        newCard.GetComponent<Card>().Index = i;
        newCard.GetComponent<Card>().SetCardOwnership(playerNum);
        //newCard.transform.SetParent(playerDeckPanel/*[playerNum - 1]*/.transform);
    }

    public void FillHandWithCards()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                PullNextCardFromDeck();
            }
        }
    }

    private IEnumerator _DoFusion(string address)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            yield break;
        }

        Debug.Log(address);
        WWW request = new WWW(address);
        yield return request;
        Debug.Log(request.text);

        //build the cooked card
        string[] cardInfo = request.text.Split('|');
        if (cardInfo[0] == "")
        {
            yield break;
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

        //Card newCard = new Card();
        GameObject newCard = PhotonNetwork.Instantiate(GameManager.Instance.CardObj.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        //newCard.transform.SetParent(playerDeckPanels[playerNum - 1].transform);
        Card _newCard = newCard.GetComponent<Card>();
        //Card _newCard = new Card();
        _newCard.SetupCard(
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
        _newCard.SetCardOwnership(playerNum);
        //remove the spice from your hand
        hand[cookSpiceCard.Index] = null;
        PhotonNetwork.Destroy(cookSpiceCard.gameObject);
        cookSpiceCard = null;
        //replace the base card with the new fusion
        //cookBaseCard.SetupCardFromCard(_newCard);
        int ind = cookBaseCard.Index;
        if (cookBaseCard.cardStatus == CardStatus.CS_Bench)
        {
            PhotonNetwork.Destroy(bench[ind].gameObject);
            bench[ind] = null;
            _newCard.cardStatus = CardStatus.CS_Bench;
            //MoveCardToBench(_newCard);
        }
        else if (cookBaseCard.cardStatus == CardStatus.CS_Active)
        {
            PhotonNetwork.Destroy(activeCard.gameObject);
            activeCard = null;
            _newCard.cardStatus = CardStatus.CS_Active;
            activeCard = _newCard;
            //MoveCardToActiveSlot(_newCard);
            //LayoutGroup group = playerPlatePanel.GetComponent<LayoutGroup>();
            //group.childAlignment = TextAnchor.LowerLeft;
            //group.childAlignment = TextAnchor.LowerCenter;
        }
        cookBaseCard = _newCard;
        _newCard.cardStatus = CardStatus.CS_Active;
        cookBaseCard.UpdateCard();
        //PhotonNetwork.Destroy(newCard.gameObject);
        cookBaseCard = null;
        playerState = PlayerState.PS_Waiting; //change to waiting
        selectedAction = Action.Action_Cook;
    }

    public void SetAttack()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (activeCard == null)
        {
            Debug.LogWarning("Warning: Cannot attack without an active card!");
            return;
        }
        if (activeCard.CardType == CardType.CT_NULL || activeCard.CardType == CardType.CT_Spice)
        {
            Debug.LogWarning("Warning: Incorrect Card Type as active card (how did that happen?!?!)");
            return;
        }
        playerState = PlayerState.PS_Waiting;
        selectedAction = Action.Action_Attack;
    }

    public void SetEat()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (activeCard == null)
        {
            Debug.LogWarning("Warning: Cannot eat without an active card!");
            return;
        }
        if (activeCard.CardType == CardType.CT_NULL || activeCard.CardType == CardType.CT_Spice)
        {
            Debug.LogWarning("Warning: Incorrect Card Type as active card (how did that happen?!?!)");
            return;
        }
        playerState = PlayerState.PS_Waiting;
        selectedAction = Action.Action_Eat;
    }

    public void SetFusion()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        playerState = PlayerState.PS_Waiting;
        selectedAction = Action.Action_Cook;
    }

    public void DoFusion(Card Base, Card Spice)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Base == null || Spice == null)
        {
            Debug.LogWarning("Cannot cook when one of the cards are null! Resetting some flags...");
            playerState = PlayerState.PS_Selecting;
            selectedAction = Action.Action_ERROR;
            return;
        }

        if (Base.CardType != CardType.CT_Base)
        {
            if (Base.CardType != CardType.CT_Monster)
            {
                Debug.LogError("Incorrect type for Base card! Was " + Base.CardType.ToString());
                return;
            }
        }
        if (Spice.CardType != CardType.CT_Spice)
        {
            Debug.LogError("Incorrect type of Spice card! Was" + Spice.CardType.ToString());
            return;
        }

        string baseName = Base.CardName.Replace(' ', '+');
        string spiceName = Spice.CardName.Replace(' ',  '+');
        int valueSum = Base.CardValue + Spice.CardValue;

        string address = "http://10.0.0.34/it490group/MakeTurn.php?turntype=cook&playerID=" + playerNum.ToString() +
            "&base=" + baseName + "&spice=" + spiceName + "&valueSum=" + valueSum.ToString();

        StartCoroutine(_DoFusion(address));
    }

    public void MoveCardToBench(Card card)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (card == null)
        {
            return;
        }
        int index = card.Index;
        for (int i = 0; i < bench.Length; i++)
        {
            if (bench[i] == null)
            {
                bench[i] = card;
                hand[index] = null;
                card.Index = i;
                card.gameObject.transform.SetParent(playerBenchPanel.transform);
                card.cardStatus = CardStatus.CS_Bench;
                return;
            }
        }
        Debug.LogWarning("The player's bench is full! Cannot move right now.");
    }

    public void MoveCardToActiveSlot(Card card)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (card == null)
        {
            return;
        }
        int index = card.Index;
        if (activeCard == null)
        {
            activeCard = card;
            bench[index] = null;
            card.gameObject.transform.SetParent(playerPlatePanel.transform);
            card.cardStatus = CardStatus.CS_Active;
            return;
        }
        Debug.LogWarning("The player has an Active Card already, cannot move right now.");
    }

    public void DestroyActiveCard()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (activeCard == null)
        {
            return;
        }

        PhotonNetwork.Destroy(activeCard.gameObject);
        activeCard = null;
    }

    public bool SetOutlineStatus(bool active, string type)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return false;
        }

        bool canCook = false;

        if (type == "base")
        {
            foreach (Card card in bench)
            {
                if (card != null)
                {
                    if (card.IsFusable)
                    {
                        Outline outline = card.CardOutline;
                        outline.enabled = active;
                        canCook = true;
                    }
                }
            }
            if (activeCard != null)
            {
                if (activeCard.IsFusable)
                {
                    Outline outline = activeCard.CardOutline;
                    outline.enabled = active;
                    canCook = true;
                }
            }
        }
        else if (type == "spice")
        {
            //Debug.Log("It's a spice!");
            foreach (Card card in hand)
            {
                //Debug.Log(card == null);
                if (card != null)
                {
                    if (card.IsFusable/* && card.CardType == CardType.CT_Spice*/)
                    {
                        Outline outline = card.CardOutline;
                        outline.enabled = active;
                        canCook = true;
                    }
                }
            }
        }

        return canCook;
    }

    public void StartCook()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        bool canCook = false;

        canCook = SetOutlineStatus(true, "base");

        if (canCook)
        {
            playerState = PlayerState.PS_Cook;
        }
        else
        {
            Debug.LogWarning("Player cannot Cook right now.");
        }
    }

    public void CookSelectBase(Card card)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (card == null)
        {
            return;
        }

        if (card.IsFusable)
        {
            cookBaseCard = card;
            SetOutlineStatus(false, "base");
            SetOutlineStatus(true, "spice");
        }
    }

    public void CookSelectSpice(Card card)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (card == null)
        {
            return;
        }

        if (card.IsFusable)
        {
            cookSpiceCard = card;
            SetOutlineStatus(false, "spice");
            //DoFusion(cookBaseCard, cookSpiceCard);
            SetFusion();
            //playerState = PlayerState.PS_Waiting;
        }
    }

    public int DeckCount()
    {
        return deck.Count;
    }

    public int HandCount()
    {
        int numCards = 0;

        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] != null)
            {
                numCards++;
            }
        }

        return numCards;
    }

    public int BenchCount()
    {
        int numCards = 0;

        for (int i = 0; i < bench.Length; i++)
        {
            if (hand[i] != null)
            {
                numCards++;
            }
        }

        return numCards;
    }

    public int ActiveCardCount()
    {
        return (activeCard != null ? 1 : 0);
    }

    public bool IsPlayerDead()
    {
        return playerHP <= 0;
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (damageAmount >= 0)
        {
            playerHP -= damageAmount;
        }
    }

    public void PlayerHeal(int healAmount)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (healAmount > 0)
        {
            playerHP += healAmount;
            if (playerHP > maxPlayerHP)
            {
                playerHP = maxPlayerHP;
            }
        }
    }

    public void UpdatePlayerState(PlayerState ps)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        playerState = ps;
    }

    public void UpdatePlayerAciton(Action act)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        selectedAction = act;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerNum);
            stream.SendNext(playerName);
            stream.SendNext(playerHP);
            //stream.SendNext(playerHPText);
            stream.SendNext(startingPlayerHP);
            stream.SendNext(maxPlayerHP);
            stream.SendNext(playerState);
            stream.SendNext(selectedAction);
            stream.SendNext(rawDeckData);
            //stream.SendNext(deck);
            //stream.SendNext(hand);
            //stream.SendNext(activeCard);
            //stream.SendNext(cookBaseCard);
            //stream.SendNext(cookSpiceCard);
            //stream.SendNext(playerPlatePanel);
            //stream.SendNext(bench);
            //stream.SendNext(discardPile);
            //stream.SendNext(playerDeckPanel);
            //stream.SendNext(playerBenchPanel);
            //stream.SendNext(playerDeckInfo);
            stream.SendNext(deckFinishedBuilding);
        }
        else
        {
            playerNum = (int)stream.ReceiveNext();
            playerName = (string)stream.ReceiveNext();
            playerHP = (int)stream.ReceiveNext();
            //playerHPText = (Text)stream.ReceiveNext();
            startingPlayerHP = (int)stream.ReceiveNext();
            maxPlayerHP = (int)stream.ReceiveNext();
            playerState = (PlayerState)stream.ReceiveNext();
            selectedAction = (Action)stream.ReceiveNext();
            rawDeckData = (string)stream.ReceiveNext();
            //deck = (Stack<Card>)stream.ReceiveNext();
            //hand = (Card[])stream.ReceiveNext();
            //activeCard = (Card)stream.ReceiveNext();
            //cookBaseCard = (Card)stream.ReceiveNext();
            //cookSpiceCard = (Card)stream.ReceiveNext();
            //playerPlatePanel = (GameObject)stream.ReceiveNext();
            //bench = (Card[])stream.ReceiveNext();
            //discardPile = (Stack<Card>)stream.ReceiveNext();
            //playerDeckPanel = (GameObject)stream.ReceiveNext();
            //playerBenchPanel = (GameObject)stream.ReceiveNext();
            //playerDeckInfo = (Text)stream.ReceiveNext();
            deckFinishedBuilding = (bool)stream.ReceiveNext();
        }
    }

    /*public GameObject GetPlayerDeckPanel(int index)
    {
        if (index < 1 || index > 2)
        {
            return null;
        }
        return playerDeckPanels[index - 1];
    }

    public GameObject GetPlayerBenchPanel(int index)
    {
        if (index < 1 || index > 2)
        {
            return null;
        }
        return playerBenchPanels[index - 1];
    }*/


}
