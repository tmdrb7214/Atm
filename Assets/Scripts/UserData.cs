using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData
{
    public string ID;
    public string Password;   
    public string UserName;
    public int UserCash;
    public int UserBalance;
    public UserData(string iD, string password, string userName, int userCash, int userBalance)
    {
        this.ID = iD;
        this.Password = password;
        this.UserName = userName;
        this.UserCash = userCash;
        this.UserBalance = userBalance;
    }
}


