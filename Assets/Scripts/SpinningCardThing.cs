using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCardThing : MonoBehaviour
{
    private SpriteRenderer rend;
    private readonly string[] filepaths =
    {
        "Sprites/CardImgs/dough",
        "Sprites/CardImgs/egg",
        "Sprites/CardImgs/potato",
        "Sprites/CardImgs/corn",
        "Sprites/CardImgs/tofu",
        "Sprites/CardImgs/mysterymeat",
        "Sprites/CardImgs/flour",
        "Sprites/CardImgs/water",
        "Sprites/CardImgs/feed",
        "Sprites/CardImgs/maplesyrup",
        "Sprites/CardImgs/tomatosauce",
        "Sprites/CardImgs/horseradish",
        "Sprites/CardImgs/saltandpepper",
        "Sprites/CardImgs/onions",
        "Sprites/CardImgs/garlic",
        "Sprites/CardImgs/hawtsauce",
        "Sprites/CardImgs/sugar",
        "Sprites/CardImgs/cheese",
        "Sprites/CardImgs/gruel",
        "Sprites/CardImgs/plainoatmeal",
        "Sprites/CardImgs/gdsslop",
        "Sprites/CardImgs/grub",
        "Sprites/CardImgs/biggrub",
        "Sprites/CardImgs/feast",
        "Sprites/CardImgs/cauliflowerbeast",
        "Sprites/CardImgs/omelette",
        "Sprites/CardImgs/soup",
        "Sprites/CardImgs/taco",
        "Sprites/CardImgs/waffles",
        "Sprites/CardImgs/chicken",
        "Sprites/CardImgs/pasta",
        "Sprites/CardImgs/pizza",
        "Sprites/CardImgs/crab",
        "Sprites/CardImgs/onionrings",
        "Sprites/CardImgs/fries",
        "Sprites/CardImgs/scrumptiouscake",
        "Sprites/CardImgs/frenchtoast",
        "Sprites/CardImgs/loadedbakedpotato",
        "Sprites/CardImgs/hotwings",
        "Sprites/CardImgs/popcorn",
        "Sprites/CardImgs/almonds",
        "Sprites/CardImgs/ribs",
        "Sprites/CardImgs/sugarglider",
        "Sprites/CardImgs/chickenhorse",
        "Sprites/CardImgs/bird.obj",
        "Sprites/CardImgs/activatedalmonds",
        "Sprites/CardImgs/triplethreattaco",
        "Sprites/CardImgs/corndogofdisappointment",
        "Sprites/CardImgs/kidzbopcask",
        "Sprites/CardImgs/holymolystromboli",
        "Sprites/CardImgs/tendersandfries",
        "Sprites/CardImgs/foreclosedpancakehouse",
        "Sprites/CardImgs/cheesecake",
        "Sprites/CardImgs/pancake",
        "Sprites/CardImgs/hotdog"
    };

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeImage()
    {
        int i = Random.Range(0, filepaths.Length);
        Sprite imageSprite = Resources.Load<Sprite>(filepaths[i]);
        if (imageSprite != null)
        {
            rend.sprite = imageSprite;
        }
    }
}
