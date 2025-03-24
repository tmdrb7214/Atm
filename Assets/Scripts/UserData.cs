using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData
{
    public string UserName;
    public int UserCash;
    public int UserBalance;
    public UserData(string userName, int userCash, int userBalance)
    {
        UserName = userName;
        UserCash = userCash;
        UserBalance = userBalance;
    }
}


