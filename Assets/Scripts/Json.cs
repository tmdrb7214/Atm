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
        Debug.Log($"저장 경로{saveData}");
        UserData loadedData = LoadData();
    }

    public void SaveData(UserData userData)
    {
        string json = JsonUtility.ToJson(userData, true);
        File.WriteAllText(saveData, json);
        Debug.Log($"저장 완료{json}");
    }

    public UserData LoadData()
    {
        if (File.Exists(saveData))
        {
            string json = File.ReadAllText(saveData);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log($"불러오기 완료{userData.UserName}");
            Debug.Log($"불러오기 완료{userData.UserCash}");
            Debug.Log($"불러오기 완료{userData.UserBalance}");

            return userData;
        }
        else
        {
            return null;
        }
    }
}
