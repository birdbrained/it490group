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

public class PlayerController : MonoBehaviourPun
{
    [SerializeField][Range(1, 2)]
    private int playerNum = 1;
    [SerializeField]
    private string playerName = "Player";
    [SerializeField]
    private int playerHP = 20;
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
    private Card cookSpiceCard;
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

    // Use this for initialization
    void Start ()
    {
        SetupPlayerDeck();
        //DoFusion(null, null);
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
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
    }

    public GameObject CreateCardFromData(string data)
    {
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
        GameObject newCard = Instantiate(GameManager.Instance.CardObj);
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
            fuseable
            );
        _newCard.SetCardOwnership(playerNum);
        return newCard;
    }

    private IEnumerator GetFullPlayerDeck()
    {
        //string address = "http://" + GameManager.Instance.GetDatabaseIP() + "/it490group/BuildFullUserDeck.php?username=b&id=0";
        string address = "https://web.njit.edu/~mwk9/it490group/BuildFullUserDeck.php?username=b&id=0";
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
            GameObject newCard = Instantiate(GameManager.Instance.CardObj);
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
                fuseable
                );
            _newCard.SetCardOwnership(playerNum);
            deck.Push(_newCard);
            Destroy(newCard);
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
        StartCoroutine(GetFullPlayerDeck());
    }

    public void PullNextCardFromDeck()
    {
        GameObject newCard = Instantiate(GameManager.Instance.CardObj);
        Card c = deck.Pop();
        int i = 0;
        for (i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                Debug.Log("Is c null? " + (c == null).ToString());
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
            Destroy(newCard);
            return;
        }
        newCard.GetComponent<Card>().SetupCardFromCard(c);
        newCard.GetComponent<Card>().cardStatus = CardStatus.CS_Hand;
        newCard.GetComponent<Card>().Index = i;
        newCard.transform.SetParent(playerDeckPanel/*[playerNum - 1]*/.transform);
    }

    private IEnumerator _DoFusion(string address)
    {
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
        GameObject newCard = Instantiate(GameManager.Instance.CardObj);
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
            fuseable
            );
        _newCard.SetCardOwnership(playerNum);
        //remove the spice from your hand
        hand[cookSpiceCard.Index] = null;
        Destroy(cookSpiceCard.gameObject);
        cookSpiceCard = null;
        //replace the base card with the new fusion
        //cookBaseCard.SetupCardFromCard(_newCard);
        int ind = cookBaseCard.Index;
        if (cookBaseCard.cardStatus == CardStatus.CS_Bench)
        {
            Destroy(bench[ind].gameObject);
            bench[ind] = null;
            MoveCardToBench(_newCard);
        }
        else if (cookBaseCard.cardStatus == CardStatus.CS_Active)
        {
            Destroy(activeCard.gameObject);
            activeCard = null;
            //activeCard = _newCard;
            MoveCardToActiveSlot(_newCard);
            LayoutGroup group = playerPlatePanel.GetComponent<LayoutGroup>();
            group.childAlignment = TextAnchor.LowerLeft;
            group.childAlignment = TextAnchor.LowerCenter;
        }
        cookBaseCard = _newCard;
        cookBaseCard.UpdateCard();
        //Destroy(newCard.gameObject);
        cookBaseCard = null;
        playerState = PlayerState.PS_Selecting; //change to waiting
    }

    public void DoFusion(Card Base, Card Spice)
    {
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

    public bool SetOutlineStatus(bool active, string type)
    {
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
        if (card == null)
        {
            return;
        }

        if (card.IsFusable)
        {
            cookSpiceCard = card;
            SetOutlineStatus(false, "spice");
            DoFusion(cookBaseCard, cookSpiceCard);
            playerState = PlayerState.PS_Waiting;
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
