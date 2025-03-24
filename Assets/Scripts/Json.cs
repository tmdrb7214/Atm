using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Json : MonoBehaviour
{
    private string saveData;

    private void Start()
    {
        saveData = Path.Combine(Application.persistentDataPath, "SaveData.json");
        Debug.Log($"���� ���{saveData}");
        UserData loadedData = LoadData();
    }

    public void SaveData(UserData userData)
    {
        string json = JsonUtility.ToJson(userData, true);
        File.WriteAllText(saveData, json);
        Debug.Log($"���� �Ϸ�{json}");
    }

    public UserData LoadData()
    {
        if (File.Exists(saveData))
        {
            string json = File.ReadAllText(saveData);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log($"�ҷ����� �Ϸ�{userData.UserName}");
            Debug.Log($"�ҷ����� �Ϸ�{userData.UserCash}");
            Debug.Log($"�ҷ����� �Ϸ�{userData.UserBalance}");

            return userData;
        }
        else
        {
            return null;
        }
    }
}
