using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI Usernametext;
    [SerializeField] private TextMeshProUGUI Usercashtext;
    [SerializeField] private TextMeshProUGUI UserbalanceText;

    public UserData userData;
    
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void Refresh()
    {
        Usernametext.text = userData.UserName;
        Usercashtext.text = string.Format("{0:N0}", userData.UserCash);
        UserbalanceText.text = string.Format("{0:N0}", userData.UserBalance);
    }
}
