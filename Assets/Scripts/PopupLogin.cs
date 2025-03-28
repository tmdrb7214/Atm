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

    public static PopupLogin instance;

    private string savePath;


    private void Awake()
    {
        LoginButton.onClick.AddListener(Login);
        SignButton.onClick.AddListener(Sign);
        CancelButton.onClick.AddListener(Cancel);
        SignUpButton.onClick.AddListener(OnSignUpButtonClick);
        CancelError.onClick.AddListener(() => Error.SetActive(false));
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        savePath = Application.persistentDataPath;
        LoadData(savePath);
    }


    public void Login()
    {
        string[] files = Directory.GetFiles(savePath, "*.json");
        if (files.Length > 0)
        {
            bool login = false;

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
                    login = true;
                    break;
                }
                //if (LoginID.text == loadData.ID)
                //{
                //    if (LoginPassword.text == loadData.Password)
                //    {
                //        GameManager.instance.userData = loadData;
                //        gameObject.SetActive(false);
                //        mainUI.SetActive(true);
                //        GameManager.instance.Refresh();
                //    }
                //}
            }
            if (!login)
            {
                Error.SetActive(true);
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
        string[] files = Directory.GetFiles(savePath, "*.json");
        foreach (string file in files) 
        {
            string json = File.ReadAllText(file);
            UserData loadedData = JsonUtility.FromJson<UserData>(json);
            if (loadedData.ID == id)
            {
                Debug.Log("이미 가입된 아이디가 있습니다.");
                Error.SetActive(true);
                return;
            }
        }
        SaveUserData(new UserData(id, password, name , 10000 ,5000));
        Cancel();
        Debug.Log(files);
    }

    public void SaveUserData(UserData userdata)
    {
        savePath = $"{Application.persistentDataPath}/{userdata.UserName}.json";
        string json = JsonUtility.ToJson(userdata, true);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath);
    }
    public UserData LoadData(string userName)
    {
        string[] files = Directory.GetFiles(savePath, "*.json");

        if (files.Length == 0)
        {
            Debug.Log("데이터 없음"); return null;
        }
        string json = File.ReadAllText(files[0]);

        UserData userData = JsonUtility.FromJson<UserData>(json);

        if (userData != null)
        {
            Debug.Log("파일 갯수 " + files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                Debug.Log(files[i]);
            }
            return userData;
        }
        Debug.Log("유저데이터 없음"); return null;
    }

    public UserData SelecUserData(string userName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"{userName}.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            return userData;
        }
        else
        {
            return null;
        }
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
