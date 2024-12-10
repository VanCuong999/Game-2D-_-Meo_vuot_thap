using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class firebaselogin : MonoBehaviour
{
    [Header("dang ky")]
    public InputField ipdangkyEmail;
    public InputField ipdangkyPassword;
    public InputField ipConfirmPassword;
    public Button buttondangky;

    private FirebaseAuth auth;

    [Header("dang nhap")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;
    public Button buttonLogin;

    [Header("Swich form")]
    public Button buttonmoveTologin;
    public Button buttonmoveTodangky;

    public GameObject loginForm;
    public GameObject dangkyForm;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // Kiểm tra các tham chiếu UI không bị null
        if (buttondangky != null) buttondangky.onClick.AddListener(dangky);
        if (buttonLogin != null) buttonLogin.onClick.AddListener(Login);
        if (buttonmoveTologin != null) buttonmoveTologin.onClick.AddListener(SwichForm);
        if (buttonmoveTodangky != null) buttonmoveTodangky.onClick.AddListener(SwichForm);
    }

    public void dangky()
    {
        string Email = ipdangkyEmail.text;
        string Password = ipdangkyPassword.text;
        string ConfirmPassword = ipConfirmPassword.text;

        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
        {
            Debug.Log("Email, mật khẩu hoặc xác nhận mật khẩu không được để trống!");
            return;
        }

        if (Password.Length < 6)
        {
            Debug.Log("Mật khẩu phải có ít nhất 6 ký tự!");
            return;
        }

        if (Password != ConfirmPassword)
        {
            Debug.Log("Mật khẩu và xác nhận mật khẩu không khớp!");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWithOnMainThread(Task =>
        {
            if (Task.IsCanceled)
            {
                Debug.Log("Đăng ký bị hủy!");
                return;
            }
            if (Task.IsFaulted)
            {
                Debug.Log("Đăng ký thất bại!");
                return;
            }
            if (Task.IsCompleted)
            {
                Debug.Log("Đăng ký thành công!");
            }
        });
    }

    public void Login()
    {
        string Email = ipLoginEmail.text;
        string Password = ipLoginPassword.text;

        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            Debug.Log("Email hoặc mật khẩu không được để trống!");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(Email, Password).ContinueWithOnMainThread(Task =>
        {
            if (Task.IsCanceled)
            {
                Debug.Log("Đăng nhập bị hủy!");
                return;
            }
            if (Task.IsFaulted)
            {
                Debug.Log("Đăng nhập thất bại!");
                return;
            }
            if (Task.IsCompleted)
            {
                Debug.Log("Đăng nhập thành công!");
                SceneManager.LoadScene("menu"); // Đảm bảo tên scene "menu" đúng trong Build Settings
            }
        });
    }

    public void SwichForm()
    {
        if (loginForm != null && dangkyForm != null)
        {
            loginForm.SetActive(!loginForm.activeSelf);
            dangkyForm.SetActive(!dangkyForm.activeSelf);
        }
    }
}
