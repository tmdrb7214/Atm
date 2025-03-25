using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

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
    }

    private void Update()
    {
        CheckSign();
    }
    public void Login()
    {
        string path = $"{Application.persistentDataPath}/{name}.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(path);
            UserData loadData = JsonUtility.FromJson<UserData>(json);
            if (LoginID.text == loadData.ID && LoginPassword.text == loadData.Password)
            {
                GameManager.instance.userData = loadData;
                gameObject.SetActive(false);
                mainUI.SetActive(true);
            }
            else
            {
                Debug.Log("Login Failed");
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

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            Debug.Log("빈칸을 채워주세요");
            Error.SetActive(true);
            return;
        }
        else if (password != passwordConfirm)
        {
            Debug.Log("비밀번호가 일치하지 않습니다.");
            Error.SetActive(true);
            return;
        }

        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            UserData loadedData = JsonUtility.FromJson<UserData>(json);
            if (loadedData.ID == id)
            {
                Debug.Log("이미 가입된 아이디가 있습니다.");
                return;
            }
        }
        ClearInputFields();
        SaveUserData(id, password, name , 0 ,0);
        Cancel();
    }


    private void SaveUserData(string id, string password , string name , int cash , int balnace)
    {
        savePath = $"{Application.persistentDataPath}/{name}.json";

        UserData userData = new UserData(id ,password , name, cash , balnace);
        string json = JsonUtility.ToJson(userData,true);
        File.WriteAllText(savePath, json);
        GameManager.instance.userData = userData;
    }


    public void Sign()
    {
        SignUI.SetActive(true);
        LoginUI.SetActive(false);
    }

    public void Cancel()
    {
        SignUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    public void CheckSign()
    {
        if (string.IsNullOrEmpty(SignId.text))
        {
            Check.text = "아이디를 입력해주세요";
            return;
        }
        else if (string.IsNullOrEmpty(SignName.text))
        {
            Check.text = "이름을 입력해주세요";
            return;
        }
        else if (string.IsNullOrEmpty(SignPassword.text))
        {
            Check.text = "비밀번호를 입력해주세요";
            return;
        }
        else if (string.IsNullOrEmpty(SignPasswordConfirm.text))
        {
            Check.text = "비밀번호를 확인 해주세요";
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
