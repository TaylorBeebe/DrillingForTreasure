using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public bool clear;
    public bool upload;

    public string playerName;
    public string floor;
    public string minerals;

    public GameObject[] scores;

    void Start()
    {
        if (clear)
        {
            //Clear
            StartCoroutine(ClearHighScores());
            StartCoroutine(GetHighscores());
        }

        if (upload)
            StartCoroutine(SubmitHighscore());        
    }

    IEnumerator ClearHighScores()
    {
        WWWForm form = new WWWForm();
        form.AddField("myscore", "3002");
        form.AddField("date", "2018-08-13T00:19:17.633Z");

        UnityWebRequest www = UnityWebRequest.Post("http://206.189.191.161/api/mft/clearscores", form); //http://206.189.191.161/api/mft/clearscores
        www.SetRequestHeader("auth", "8483qnf3&#@f3q9nF$@&!$9n#");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator GetHighscores()
    {
        WWWForm form = new WWWForm();
        form.AddField("myscore", "3002");
        form.AddField("date", "2018-08-13T00:19:17.633Z");

        UnityWebRequest www = UnityWebRequest.Post("http://206.189.191.161/api/mft/getHighscores", form); //http://206.189.191.161/api/mft/getHighscores
        www.SetRequestHeader("auth", "38gq9G3b83f9BF3&^@f9f2!933");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            var highscores = JSON.Parse(www.downloadHandler.text);

            for (int i = 0; i < 10; i++)
            {
                scores[i].transform.Find("name").GetComponent<Text>().text = highscores["data"]["highscores"][i]["name"];
                scores[i].transform.Find("floor").GetComponent<Text>().text = highscores["data"]["highscores"][i]["floor"];
                scores[i].transform.Find("minerals").GetComponent<Text>().text = highscores["data"]["highscores"][i]["minerals"];
            }
        }
    }

    IEnumerator SubmitHighscore()
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("name", playerName);
        form1.AddField("minerals", minerals);
        form1.AddField("floor", floor);


        UnityWebRequest www = UnityWebRequest.Post("http://206.189.191.161/api/mft/submitHighscore", form1); //http://206.189.191.161/api/mft/submitHighscore
        www.SetRequestHeader("auth", "bueibwf&#@b830B*wnUW&!$9nce#");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            var highscores = JSON.Parse(www.downloadHandler.text);

            for (int i = 0; i < 10; i++)
            {
                scores[i].transform.Find("name").GetComponent<Text>().text = highscores["data"]["highscores"][i]["name"];
                scores[i].transform.Find("minerals").GetComponent<Text>().text = highscores["data"]["highscores"][i]["minerals"];
                scores[i].transform.Find("floor").GetComponent<Text>().text = highscores["data"]["highscores"][i]["floor"];
            }

            if (highscores["data"]["myrank"] > 10)
            {
                scores[10].transform.Find("name").GetComponent<Text>().text = playerName;
                scores[10].transform.Find("minerals").GetComponent<Text>().text = minerals;
                scores[10].transform.Find("floor").GetComponent<Text>().text = floor;
                scores[10].transform.Find("Place").GetComponent<Text>().text = highscores["data"]["myrank"];
            }

        }
    }
}
