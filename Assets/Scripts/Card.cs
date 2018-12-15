using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType
{
    CT_NULL,
    CT_Base,
    CT_Spice,
    CT_Monster,
    CT_Superfood
};

public enum CardStatus
{
    CS_Deck,
    CS_Hand,
    CS_Bench,
    CS_Active
}

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int ID = 0;
    [SerializeField]
    private string cardName = "undefined";
    public string CardName
    {
        get
        {
            return cardName;
        }
    }
    [SerializeField]
    private string cardDescription = "undefined";
    [SerializeField]
    private CardType cardType = CardType.CT_NULL;
    public CardType CardType
    {
        get
        {
            return cardType;
        }
    }
    [SerializeField]
    private string imageFilepath = "Sprites/none";
    [SerializeField]
    private Sprite imageSprite;
    [SerializeField]
    private int attackAmount = 0;
    public int AttackAmount
    {
        get
        {
            return attackAmount;
        }
    }
    [SerializeField]
    private int defenseAmount = 0;
    public int DefenseAmount
    {
        get
        {
            return defenseAmount;
        }
    }
    [SerializeField]
    private int value = 0;
    public int CardValue
    {
        get
        {
            return value;
        }
    }
    [SerializeField]
    private bool isFusable = false;
    public bool IsFusable
    {
        get
        {
            return isFusable;
        }
    }
    [SerializeField]
    private int cardHP;

    //ui stuff
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text typeText;
    [SerializeField]
    private Image cardImage;
    [SerializeField]
    private SpriteRenderer cardSpriteRenderer;
    [SerializeField]
    private Text attackText;
    [SerializeField]
    private Text defenseText;
    [SerializeField]
    private Text valueText;
    [SerializeField]
    private Outline cardOutline;
    public Outline CardOutline
    {
        get
        {
            return cardOutline;
        }
    }

    public CardStatus cardStatus;
    public int ownedByPlayer = 1;
    private PlayerController player;
    public int Index = 0;

	// Use this for initialization
	void Start ()
    {
        UpdateCard();
	}

    public void UpdateCard()
    {
        if (nameText != null)
        {
            nameText.text = cardName;
        }
        if (typeText != null)
        {
            typeText.text = cardType.ToString().Substring(3);
        }
        if (cardImage != null)
        {
            imageSprite = Resources.Load<Sprite>(imageFilepath);
            if (imageSprite != null)
            {
                cardImage.sprite = imageSprite;
            }
        }
        if (cardSpriteRenderer != null)
        {
            imageSprite = Resources.Load<Sprite>(imageFilepath);
            if (imageSprite != null)
            {
                cardSpriteRenderer.sprite = imageSprite;
            }
        }
        if (attackText != null)
        {
            attackText.text = attackAmount.ToString();
        }
        if (defenseText != null)
        {
            defenseText.text = defenseAmount.ToString();
        }
        if (valueText != null)
        {
            valueText.text = value.ToString();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateImage()
    {
        if (cardImage != null)
        {
            imageSprite = Resources.Load<Sprite>(imageFilepath);
            if (imageSprite != null)
            {
                cardImage.sprite = imageSprite;
            }
        }
        if (cardSpriteRenderer != null)
        {
            imageSprite = Resources.Load<Sprite>(imageFilepath);
            if (imageSprite != null)
            {
                cardSpriteRenderer.sprite = imageSprite;
            }
        }
    }

    public void SetupCard(int id, string name, string description, CardType type, string imgFilepath, int attack, int defense, int val, bool fusable, int hp)
    {
        ID = id;
        cardName = name;
        cardDescription = description;
        cardType = type;
        imageFilepath = imgFilepath;
        attackAmount = attack;
        defenseAmount = defense;
        value = val;
        isFusable = fusable;
        cardHP = hp;
        cardStatus = CardStatus.CS_Deck;
    }

    public void SetupCardFromCard(Card card)
    {
        ID = card.ID;
        cardName = card.cardName;
        cardDescription = card.cardDescription;
        cardType = card.cardType;
        imageFilepath = card.imageFilepath;
        attackAmount = card.attackAmount;
        defenseAmount = card.defenseAmount;
        value = card.value;
        isFusable = card.isFusable;
        cardHP = card.cardHP;
        cardStatus = CardStatus.CS_Deck;
    }

    public void SetCardOwnership(int playerNum)
    {
        if (playerNum < 1 || playerNum > 2)
        {
            return;
        }
        ownedByPlayer = playerNum;
        if (ownedByPlayer == 1)
        {
            player = GameManager.Instance.Player1;
            //gameObject.transform.SetParent(GameManager.Instance.Player1.GetPlayerBenchPanel(ownedByPlayer).transform);
        }
        else
        {
            player = GameManager.Instance.Player2;
            //gameObject.transform.SetParent(GameManager.Instance.Player2.GetPlayerBenchPanel(ownedByPlayer).transform);
        }
    }

    public int GetCardID()
    {
        return ID;
    }

    public void CardTakeDamage(int damageAmount)
    {
        cardHP -= damageAmount;
        if (cardHP <= 0)
        {
            //destroy the card
        }
    }

    public bool IsCardDead()
    {
        return cardHP <= 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked on " + gameObject.name);

        /*PlayerController player;
        if (ownedByPlayer == 1)
        {
            player = GameManager.Instance.Player1;
            //gameObject.transform.SetParent(GameManager.Instance.Player1.GetPlayerBenchPanel(ownedByPlayer).transform);
        }
        else
        {
            player = GameManager.Instance.Player2;
            //gameObject.transform.SetParent(GameManager.Instance.Player2.GetPlayerBenchPanel(ownedByPlayer).transform);
        }*/

        switch (cardStatus)
        {
            case CardStatus.CS_Hand:
                //move card to player's bench, if it is a base
                if (cardType == CardType.CT_Base)
                {
                    switch (player.CurrPlayerState)
                    {
                        case PlayerState.PS_Selecting:
                            player.MoveCardToBench(this);
                            break;
                        case PlayerState.PS_Cook:
                            player.CookSelectBase(this);
                            break;
                        default:
                            break;
                    }
                }
                else if (cardType == CardType.CT_Spice)
                {
                    if (player.CurrPlayerState == PlayerState.PS_Cook)
                    {
                        player.CookSelectSpice(this);
                    }
                }
                break;
            case CardStatus.CS_Bench:
                Debug.Log("Card on bench!");
                if (cardType == CardType.CT_Base || cardType == CardType.CT_Monster || cardType == CardType.CT_Superfood)
                {
                    switch (player.CurrPlayerState)
                    {
                        case PlayerState.PS_Selecting:
                            player.MoveCardToActiveSlot(this);
                            break;
                        case PlayerState.PS_Cook:
                            player.CookSelectBase(this);
                            break;
                        default:
                            break;
                    }   
                }
                break;
            case CardStatus.CS_Active:
                if (player.CurrPlayerState == PlayerState.PS_Cook)
                {
                    player.CookSelectBase(this);
                }
                break;
            default:
                break;
        }
    }
}
