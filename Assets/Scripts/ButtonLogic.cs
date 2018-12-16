using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField]
    private Text totalMoneyText;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (totalMoneyText != null)
        {
            totalMoneyText.text = "Current Money: " + GameManager.Instance.TotalMoney.ToString() + "¢";
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdatePlayerMoney()
    {
        GameManager.Instance.UpdateTotalMoney();
    }
}
