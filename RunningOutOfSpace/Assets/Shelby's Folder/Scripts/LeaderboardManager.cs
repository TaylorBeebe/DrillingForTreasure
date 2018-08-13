using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour {


    public string playerName;
    public int floor;
    public int minerals;

    void Start()
    {
        StartCoroutine(GetHighscores());
        StartCoroutine(SubmitHighscore());
    }

    IEnumerator GetHighscores()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("myname", "mark"));
        formData.Add(new MultipartFormFileSection("myscore", "145"));
        formData.Add(new MultipartFormFileSection("date", "2018-08-13T00:19:17.633Z"));

        UnityWebRequest www = UnityWebRequest.Post("http://206.189.191.161/api/mft/getHighscores", formData);
        www.SetRequestHeader("auth", "38gq9G3b83f9BF3&^@f9f2!933");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);

        }
    }

    IEnumerator SubmitHighscore()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name", "claire"));
        formData.Add(new MultipartFormFileSection("minerals", "1234"));
        formData.Add(new MultipartFormFileSection("floor", "3"));

        UnityWebRequest www = UnityWebRequest.Post("http://206.189.191.161/api/mft/submitHighscore", formData);
        www.SetRequestHeader("auth", "bueibwf&#@b830B*wnUW&!$9nce#");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);

        }
    }
}
