using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    CT_NULL,
    CT_Base,
    CT_Spice,
    CT_Monster,
    CT_Superfood
};

public class Card : MonoBehaviour
{
    [SerializeField]
    private int ID = 0;
    [SerializeField]
    private string cardName = "undefined";
    [SerializeField]
    private string cardDescription = "undefined";
    [SerializeField]
    private CardType cardType = CardType.CT_NULL;
    [SerializeField]
    private string imageFilepath = "Sprites/none";
    [SerializeField]
    private Sprite imageSprite;
    [SerializeField]
    private int attackAmount = 0;
    [SerializeField]
    private int defenseAmount = 0;
    [SerializeField]
    private int value = 0;
    [SerializeField]
    private bool isFusable = false;

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

	// Use this for initialization
	void Start ()
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
	void Update () {
		
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

    public void SetupCard(int id, string name, string description, CardType type, string imgFilepath, int attack, int defense, int val, bool fusable)
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
    }

    public int GetCardID()
    {
        return ID;
    }
}
