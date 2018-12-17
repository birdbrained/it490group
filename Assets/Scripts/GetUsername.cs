using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUsername : MonoBehaviour
{
    private string playerName = "";
    [SerializeField]
    private Text playerNameText;
    [SerializeField]
    private InputField inputField;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(GetUsernameFromSession());
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playerNameText != null)
        {
            if (playerName == "")
            {
                playerNameText.text = "";
            }
            else
            {
                playerNameText.text = "Welcome " + playerName + "!";
            }
        }
	}

    public void ChangeName()
    {
        if (inputField != null)
        {
            playerName = inputField.text;
            LocalUserData.Instance.UserName = playerName;
        }
    }

    public IEnumerator GetUsernameFromSession()
    {
        string address = "http://10.0.0.34/it490group/GetUsername.php";
        WWW request = new WWW(address);
        yield return request;
        Debug.LogFormat("GetUsernameFromSession: {0}", request.text);
        if (!string.IsNullOrEmpty(request.text))
        {
            playerName = request.text;
            //GameManager.Instance.UserName = playerName;
            LocalUserData.Instance.UserName = playerName;
        }
    }
}
