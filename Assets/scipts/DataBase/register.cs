using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class register : MonoBehaviour
{
    public enum ErrorType { Connect, FailConnectBD, NameCheckQueryFailed, AlreadyUserExist, InsertUserQueryFailed, End }
    public TextMeshProUGUI textAdvert;
    public TextMeshProUGUI userInput;
    public TextMeshProUGUI passwordInput;
    public Coroutine registerCR;
    public ErrorType errorType = ErrorType.End;
    public UnityEvent IfSusfulyLogin;

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
            errorType = (ErrorType)aux;
        }

        switch (errorType)
        {
            case ErrorType.Connect:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "Registered!";
                Invoke(nameof(Successful),0.5f);
                
                Debug.Log("Usuario Cargado.");
                break;

            case ErrorType.AlreadyUserExist:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "El usuario ya existe.";
                Debug.Log("El usuario ya existe.");
                break;

            case ErrorType.FailConnectBD:
            case ErrorType.NameCheckQueryFailed:
            case ErrorType.InsertUserQueryFailed:
                Debug.LogWarning("Internal ERROR." + errorType);
                break;

            default:
                textAdvert.gameObject.SetActive(true);
                textAdvert.text = "--Error de Registro--";
                Debug.Log("Error type desconocido (Ver register.php).");
                break;
        }
        registerCR = null;
    }

    public void Successful()
    {
        IfSusfulyLogin?.Invoke();
    }

    //void GoToMenu()
    //{
    //    uiManager.ButtonPressed(0);
    //}
}