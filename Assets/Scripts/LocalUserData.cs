using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUserData : MonoBehaviour
{
    private static LocalUserData instance;
    public static LocalUserData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalUserData>();
            }
            return instance;
        }
    }
    public string UserName;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
