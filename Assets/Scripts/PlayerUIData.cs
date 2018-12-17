using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIData : MonoBehaviour
{
    private static PlayerUIData instance;
    public static PlayerUIData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerUIData>();
            }
            return instance;
        }
    }

    public Text player1HPText;
    public GameObject player1PlatePanel;
    public GameObject player1DeckPanel;
    public GameObject player1BenchPanel;
    public Text player1DeckInfo;

    public Text player2HPText;
    public GameObject player2PlatePanel;
    public GameObject player2DeckPanel;
    public GameObject player2BenchPanel;
    public Text player2DeckInfo;
}
