using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public Scene nextScene;
    public GameObject errorText;

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

    void LoadNextScene()
    {

    }
}
