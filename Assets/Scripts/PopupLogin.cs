using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Unity.VisualScripting;

public class PopupLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField LoginID;
    [SerializeField] private TMP_InputField LoginPassword;
    [SerializeField] private TMP_InputField SignId;
    [SerializeField] private TMP_InputField SignName;
    [SerializeField] private TMP_InputField SignPassword;
    [SerializeField] private TMP_InputField SignPasswordConfirm;

    [SerializeField] private Button LoginButton;
    [SerializeField] private Button SignButton;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button SignUpButton;
    [SerializeField] private Button CancelError;

    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject SignUI;
    [SerializeField] private GameObject LoginUI;
    [SerializeField] private GameObject Error;

    [SerializeField] private TextMeshProUGUI Check;


    private string savePath;


    private void Awake()
    {
        LoginButton.onClick.AddListener(Login);
        SignButton.onClick.AddListener(Sign);
        CancelButton.onClick.AddListener(Cancel);
        SignUpButton.onClick.AddListener(OnSignUpButtonClick);
        CancelError.onClick.AddListener(() => Error.SetActive(false));
    }
    private void Start()
    {
        savePath = Application.persistentDataPath;
        LoadData();
    }


    public void Login()
    {
        string[] files = Directory.GetFiles(savePath, "*.json");
        if (files.Length > 0)
        {
            foreach (string file in files)
            {
                Debug.Log(file);
                string json = File.ReadAllText(file);
                UserData loadData = JsonUtility.FromJson<UserData>(json);
                if (LoginID.text == loadData.ID && LoginPassword.text == loadData.Password)
                {
                    GameManager.instance.userData = loadData;
                    gameObject.SetActive(false);
                    mainUI.SetActive(true);
                    GameManager.instance.Refresh();
                    return;
                }
                else if(LoginID.text != loadData.ID || LoginPassword.text != loadData.Password)
                {
                    Debug.Log("faild Login");
                    Error.SetActive(true);
                }
            }
        }
        ClearInputFields();
    }

    public void OnSignUpButtonClick()
    {
        string id = SignId.text;
        string name = SignName.text;
        string password = SignPassword.text;
        string passwordConfirm = SignPasswordConfirm.text;
        CheckSign();
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            Debug.Log("��ĭ�� ä���ּ���");
            Error.SetActive(true);
            return;
        }
        else if (password != passwordConfirm)
        {
            Debug.Log("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            Error.SetActive(true);
            return;
        }
        string[] files = Directory.GetFiles(savePath, "*.json");
        foreach (string file in files) 
        {
            string json = File.ReadAllText(file);
            UserData loadedData = JsonUtility.FromJson<UserData>(json);
            if (loadedData.ID == id)
            {
                Debug.Log("�̹� ���Ե� ���̵� �ֽ��ϴ�.");
                Error.SetActive(true);
                return;
            }
        }
        SaveUserData(id, password, name , 10000 ,5000);
        Cancel();
        Debug.Log(files);
    }

    public void SaveUserData(string id, string password , string name , int cash , int balnace)
    {
        savePath = $"{Application.persistentDataPath}/{name}.json";
        UserData userData = new UserData(id ,password , name, cash , balnace);
        string json = JsonUtility.ToJson(userData,true);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath);
    }
    public UserData LoadData()
    {
        string[] files = Directory.GetFiles(savePath,"*.json");

        if (files.Length == 0) 
        { 
            Debug.Log("������ ����") ; return null;
        }
        string json = File.ReadAllText(files[0]);

        UserData userData = JsonUtility.FromJson<UserData>(json);

        if(userData != null)
        {
            Debug.Log("���� ���� " + files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                Debug.Log(files[i]);
            }
            return userData;
        }
        Debug.Log("���������� ����") ; return null;
    }

    public void Sign()
    {
        SignUI.SetActive(true);
        LoginUI.SetActive(false);
        ClearInputFields();
    }

    public void Cancel()
    {
        SignUI.SetActive(false);
        LoginUI.SetActive(true);
        ClearInputFields();
    }

    public void CheckSign()
    {
        if (string.IsNullOrEmpty(SignId.text))
        {
            Check.text = "���̵� �Է����ּ���";
            return;
        }
        else if (string.IsNullOrEmpty(SignName.text))
        {
            Check.text = "�̸��� �Է����ּ���";
            return;
        }
        else if (string.IsNullOrEmpty(SignPassword.text))
        {
            Check.text = "��й�ȣ�� �Է����ּ���";
            return;
        }
        else if (string.IsNullOrEmpty(SignPasswordConfirm.text))
        {
            Check.text = "��й�ȣ�� Ȯ�� ���ּ���";
            return;
        }
        else
        {
            Check.text = "";
        }
    }
    private void ClearInputFields()
    {
        SignId.text = "";
        SignName.text = "";
        SignPassword.text = "";
        SignPasswordConfirm.text = "";
        LoginID.text = "";
        LoginPassword.text = "";
    }

}
