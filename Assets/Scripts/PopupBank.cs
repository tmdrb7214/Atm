using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{

    [SerializeField] private Button[] button;
    [SerializeField] private GameObject[] UI;
    [SerializeField] private TMP_InputField DepositinputField;
    [SerializeField] private TMP_InputField WithdrawinputField;

    public enum UIType
    {
        USerInfo,
        Deposit,
        Withdraw,
        Popup
    }
    private void Awake()
    {
        for (int i = 0; i < button.Length; i++)
        {
            int buttonIndex = i;
            button[buttonIndex].onClick.AddListener(() => OnClick(buttonIndex));
        }
    }
    private void OnClick(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
                UI[0].SetActive(false);
                UI[1].SetActive(true);
                break;
            case 1:
                UI[0].SetActive(false);
                UI[2].SetActive(true);
                break;
            case 2:
                UI[1].SetActive(false);
                UI[0].SetActive(true);
                break;
            case 3:
                UI[2].SetActive(false);
                UI[0].SetActive(true);
                break;
            case 4:
                UI[3].SetActive(false);
                break;
            case 5:
                OnDeposit(0);
                break;
            case 6:
                OnDeposit(0);
                break;
            case 7:
                OnDeposit(0);
                break;
            case 8:
                OnDeposit(0);
                break;
            case 9:
                OnWithdraw(0);
                break;
            case 10:
                OnWithdraw(0);
                break;
            case 11:
                OnWithdraw(0);
                break;
            case 12:
                OnWithdraw(0);
                break;
            case 13:
                UI[4].SetActive(true);
                UI[5].SetActive(false);
                break;
        }
    }
    public void OnDeposit(int amount)
    {
        UserData userData = GameManager.instance.userData;
        //Json json = new Json();
        if (DepositinputField.text != "")
        {
            amount = int.Parse(DepositinputField.text);
        }
        if (userData.UserCash - amount < 0)
        {
            UI[3].SetActive(true);
            return;
        }
        userData.UserCash -= amount;
        userData.UserBalance += amount;
        DepositinputField.text = "";
        GameManager.instance.Refresh();
        //json.SaveData(userData);
    }

    public void OnWithdraw(int amount)
    {
        UserData userData = GameManager.instance.userData;
        //Json json = new Json();
        if (WithdrawinputField.text != "")
        {
            amount = int.Parse(WithdrawinputField.text);
        }
        if (userData.UserBalance - amount < 0)
        {
            UI[3].SetActive(true);
            return;
        }
        userData.UserBalance -= amount;
        userData.UserCash += amount;
        WithdrawinputField.text = "";
        GameManager.instance.Refresh();
        //json.SaveData(userData);
    }
}



