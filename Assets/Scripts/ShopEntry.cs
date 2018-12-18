using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Card))]
public class ShopEntry : MonoBehaviour
{
    private Card myCard;
    [SerializeField]
    private int price = 0;
    [SerializeField]
    private Text priceText;

	// Use this for initialization
	void Awake ()
    {
        myCard = GetComponent<Card>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (priceText != null)
        {
            priceText.text = price.ToString() + "¢";
        }
	}

    public Card GetCard()
    {
        return myCard;
    }

    public void SetupMyCard(int id, string name, string description, CardType type, string imgFilepath, int attack, int defense, int val, bool fusable, int hp)
    {
        Debug.LogFormat("ID ({0}), Name ({1}), Description ({2}), Type ({3}), imgFilepath ({4}), Attack ({5}), Defense ({6}), Value ({7}), Fusable ({8})",
            id,
            name,
            description,
            type.ToString(),
            imgFilepath,
            attack.ToString(),
            defense.ToString(),
            val.ToString(),
            fusable.ToString());
        if (myCard != null)
        {
            myCard.SetupCard(id, name, description, type, imgFilepath, attack, defense, val, fusable, hp);
            myCard.UpdateImage();
        }
        else
            Debug.Log("myCard was null");
    }

    public void SetPrice(int Price)
    {
        price = Price;
    }

    public int GetPrice()
    {
        return price;
    }

    public void MakeTransaction()
    {
        StartCoroutine(_MakeTransaction());
    }

    private IEnumerator _MakeTransaction()
    {
        Debug.LogFormat("id {0} username {1} price {2}", myCard.GetCardID().ToString(), GameManager.Instance.GetUsername(), price.ToString());
        string address = "http://10.0.0.34/it490group/MakeTransaction.php?id=" + myCard.GetCardID().ToString()
            + "&username=" + GameManager.Instance.GetUsername() + "&price=" + price.ToString();
        WWW request = new WWW(address);
        yield return request;
        Debug.Log(request.text);
        GameManager.Instance.UpdateTotalMoney();
    }
}
