using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public string nextScene;
    public GameObject errorText;
    public GameObject screen;

    [Header("Login Details")]
    public TMP_InputField username;
    public TMP_InputField password;
    public Button loginButton;

    void Update()
    {
        if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text))
        {
            loginButton.interactable = true;
        }
        else { loginButton.interactable = false; }
    }

    public void AttemptLogin()
    {
        if (username.text == "123456789" && password.text == "password1")
        {
            LoadNextScene();
            if (errorText.activeSelf)
            {
                errorText.SetActive(false);
            }
        }
        else
        {
            errorText.SetActive(true);
        }
    }

    public void ExitScreen()
    {
        screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
