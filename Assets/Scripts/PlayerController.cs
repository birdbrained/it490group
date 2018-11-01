using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(1, 2)]
    private int playerNum = 1;
    [SerializeField]
    private string playerName = "Player";
    [SerializeField]
    private int playerHP = 20;
    [SerializeField]
    private int maxPlayerHP = 30;

    private Stack<Card> deck = new Stack<Card>();
    private Card[] hand = new Card[5];
    public Card activeCard;
    private Card[] bench = new Card[3];
    private Stack<Card> discardPile = new Stack<Card>();

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    void SetupPlayerDeck()
    {
        for (int i = 0; i < 20; i++)
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
        }
    }
}
