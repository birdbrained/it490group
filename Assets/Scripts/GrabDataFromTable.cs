using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabDataFromTable : MonoBehaviour
{
    private string data = "";
    private string[] items;
    [SerializeField]
    private string IP;
    [SerializeField]
    private InputField ipInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetDataFromCardsTable()
    {
        StartCoroutine(_GetDataFromCardsTable());
    }

    private IEnumerator _GetDataFromCardsTable()
    {
        string address = "http://" + ipInput.text + "/it490group/GetCardInfo.php";
        GameManager.Instance.SetDatabaseIP(ipInput.text);
        Debug.Log(address);
        WWW request = new WWW(address);
        yield return request;
        data = request.text;
        items = data.Split(';');
        GameManager.Instance.BuildCardInfo(items);
    }
}
