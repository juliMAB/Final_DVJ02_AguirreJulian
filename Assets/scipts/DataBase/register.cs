using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public enum ErrorTypeRegister { Connect, FailConnectBD, NameCheckQueryFailed, AlreadyUserExist, InsertUserQueryFailed, End }
    public enum ErrorTypeLogin { Connect, FailConnectBD, UserDontExist, ErrorPassword, End }
    public ErrorTypeRegister errorTypeRegister = ErrorTypeRegister.End;
    public ErrorTypeLogin errorTypeLogin = ErrorTypeLogin.End;
    public TextMeshProUGUI textAdvert;
    public Text userInput;
    public Text passwordInput;
    public Coroutine registerCR;
    public Coroutine LoginCR;

    public UnityEvent IfSusfulyLoginOrRegister;

    public void SendRegistration()
    {
        if (registerCR==null)
        {
            registerCR = StartCoroutine(CallRegister());
        }
        else
        {
            textAdvert.gameObject.SetActive(true);
            textAdvert.text = "se esta realizando la operacion";
        }
        
    }
    IEnumerator CallRegister()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", userInput.text);
        form.AddField("password", passwordInput.text);
        UnityWebRequest www = UnityWebRequest.Post(DataBD.path + "register.php", form);

        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        int aux;

        if (int.TryParse(data, NumberStyles.Number, CultureInfo.InvariantCulture, out aux))
        {
            errorTypeRegister = (ErrorTypeRegister)aux;
        }
        
        switch (errorTypeRegister)
        {
            case ErrorTypeRegister.Connect:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "Registered!";
                Invoke(nameof(Successful),0.5f);
                DataLogger.Get().username = userInput.text;
                Debug.Log("Usuario Cargado.");
                break;

            case ErrorTypeRegister.AlreadyUserExist:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "El usuario ya existe.";
                Debug.Log("El usuario ya existe.");
                break;

            case ErrorTypeRegister.FailConnectBD:
            case ErrorTypeRegister.NameCheckQueryFailed:
            case ErrorTypeRegister.InsertUserQueryFailed:
                Debug.LogWarning("Internal ERROR." + errorTypeRegister);
                break;

            default:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "--Error de Registro--";
                Debug.Log("Error type desconocido (Ver register.php).");
                break;
        }
        registerCR = null;
    }
    public void CallLogin()
    {
        if (LoginCR==null)
            LoginCR =StartCoroutine(LoginCoroutine());
        else
        {
            textAdvert.gameObject.SetActive(true);
            textAdvert.text = "se esta realizando la operacion";
        }
    }

    IEnumerator LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", userInput.text);
        DataBD.Get().UserName = userInput.text;
        DataLogger.Get().username = userInput.text;
        form.AddField("password", passwordInput.text);
       

        UnityWebRequest web = UnityWebRequest.Post(DataBD.path + "login.php", form);

        yield return web.SendWebRequest();

        if (web.result == UnityWebRequest.Result.Success)
        {
            string data = web.downloadHandler.text;
            int aux;

            if (int.TryParse(data, NumberStyles.Number, CultureInfo.InvariantCulture, out aux))
            {
                errorTypeLogin = (ErrorTypeLogin)aux;
            }
            textAdvert.gameObject.SetActive(true);
            switch (errorTypeLogin)
            {
                case ErrorTypeLogin.Connect:
                    Debug.Log("Loggeado");
                    Invoke(nameof(Successful), 0.5f);
                    DataLogger.Get().username = userInput.text;
                    textAdvert.text = "Logged!";
                    break;
                case ErrorTypeLogin.FailConnectBD:
                    Debug.LogWarning("Internal ERROR." + errorTypeLogin);
                    break;
                case ErrorTypeLogin.UserDontExist:
                    Debug.Log("User Don't Exist.");
                    textAdvert.gameObject.SetActive(true);
                    textAdvert.text = "User Don't Exist!";
                    break;
                case ErrorTypeLogin.ErrorPassword:
                    Debug.Log("Error Password.");
                    textAdvert.gameObject.SetActive(true);
                    textAdvert.text = "Error Password!";
                    break;
                default:
                    Debug.Log("Error type desconocido (Ver login.php). Error:" + errorTypeLogin);
                    break;
            }
        }
        LoginCR = null;
    }

    public void Successful()
    {
        IfSusfulyLoginOrRegister?.Invoke();
        textAdvert.text = "";
    }
}