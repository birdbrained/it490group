using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabDataTest : MonoBehaviour
{
    [SerializeField]
    private Text dataText;
    private string data = "";
    private string[] items;
    [SerializeField]
    private string IP;
    [SerializeField]
    private InputField ipInput;

	// Use this for initialization
	void Start ()
    {
        //Coroutines must be started with StartCoroutine(), they cannot
        //be called like normal functions b/c the execute concurrently
        //with the main program's execution
        //StartCoroutine(CallServer());
	}

    void Update()
    {
        if (dataText != null)
        {
            dataText.text = data;
        }
    }

    public void DoLogin()
    {
        StartCoroutine(CallServer());
    }

    /**
     * @brief Runs the PHP file at a certain URL, then grabs the data from it
     */
    IEnumerator CallServer()
    {
        //creates a new WWW object, which basically runs the given
        //URL and gets the data of whatever is echo'd by the PHP page.
        //So maybe make a new php file that just spits out data?
        //string addr = "http://" + IP + "/it490group/grabTestData.php";
        string addr = "http://" + ipInput.text + "/it490group/grabTestData.php";
        WWW request = new WWW(addr); 

        //A web request takes some time to execute, so we wait for the
        //request to finish downloading before we get stuff from it.
        //That's also why this function is in an IEnumerator, which allows
        //us to yield (wait) for something to happen before we continue
        //with the code
        yield return request;

        //returns the contents of the webpage as a string
        data = request.text;
        items = data.Split(';');
        Debug.Log(data);
    }
}
