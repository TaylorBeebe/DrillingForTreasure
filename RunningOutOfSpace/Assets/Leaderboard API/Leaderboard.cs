using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour {

    string domain = "http://localhost:8080"; //TODO

	// Use this for initialization
	void Start () {
        StartCoroutine(Upload());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Test()
    {
        print("Testing");

        string postUrl = domain + "/test/";
        JSONNode sendJson = JSON.Parse("{}");
        
        /*
        sendJson["uuid"] = "u";
        sendJson["topicBlockLength"] = 3;
        sendJson["topicBlocks"] = 4;
        */    

        string jsonData = sendJson.ToString();
        Hashtable headers = CreateHeaders();
        byte[] pData = System.Text.Encoding.UTF8.GetBytes(jsonData.ToCharArray());
        WWW www = new WWW(postUrl, pData, headers);

        yield return www;
        print(www.text);
        JSONNode V = JSON.Parse(www.text);
    }

    Hashtable CreateHeaders()
    {
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        headers.Add("accept-version", "1.0");
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        headers.Add("Access-Control-Allow-Origin", "*");
        return headers;
    }

    IEnumerator Upload()
    {
        Debug.Log("Uploading!");
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("myname=maybelaterx&myscore=7"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post(domain + "/test", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            //string data = bundle.LoadAsset<string>("data");
        }
    }

}


/*
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using SimpleJSON;

public class FetchFullGameTopics : MonoBehaviour
{
    public string nextLevel;

    public static bool versus;
    public static string gameID = "0000"; // only changes for multiplayer games

    public static JSONNode N;
    public static string wwwText; 
    public static WWW www;

    public static JSONNode V;
    public static string wwwVText; 
    public static WWW wwwV; 

    public static bool waitingN;
    public static bool waitingV;

    public static int round; // make all other scripts read the round from here. 

    public static string[] round1; // fake bool "yay" or "nay" or "unanswered"
    public static string[] round2; // "1", "1", "2", "1"...
    public static string[,] round3; // ["a","b","c","d"],["f","e","g","h"]
    public static Sprite[,] round3Colours; // this needs to become obsolete
    public static string[] round4; // fake int "53"

    public static string[] round1Opponent = new string[0]; // fake bool "yay" or "nay" or "unanswered"
    public static string[] round2Opponent; // "1", "1", "2", "1"...
    public static string[,] round3Opponent; // ["a","b","c","d"],["f","e","g","h"]
    public static string[] round4Opponent; // fake int "53"

    public static int myScore = 0, opponentScore = 0;

    // Use this for initialization
    void Start()
    {
        round = 0; // 0 indexed

        waitingV = true;

        versus = GlobalScript.versus;


        if (versus) {
            waitingN = false;
            
            gameID = GlobalScript.gameID;
        }
        else
        {
            waitingN = true;
            StartCoroutine(FetchGameTopics()); // only fetch topics when playing single player, otherwise it is assigned through matchmaking/lobbies
            wwwText = GlobalScript.wwwText;
        }
        
        StartCoroutine(FetchOpinionTopics());
    }

    private void Update()
    {
        if (!waitingN && !waitingV)
        {
            FindObjectOfType<AudioScript>().SetMusic("PlayMusic", true);

            // Load next level
            Application.LoadLevel(nextLevel); //Application.LoadLevel("S_VotingTime"); 
        }
    }

    IEnumerator FetchOpinionTopics()
    {
        print("Retrieving JSON topics for opinion voting");

        string postOpinionsUrl = GlobalScript.domain + "/getVoteTopics";
        JSONNode sendJson = JSON.Parse("{}");
        print(GlobalScript.uuid);
        sendJson["uuid"] = GlobalScript.uuid;
        sendJson["topicBlockLength"] = 3;
        sendJson["topicBlocks"] = 4;
        string jsonData = sendJson.ToString(); //"{\"uuid\": \"" + GlobalScript.uuid + "\", \"topicBlockLength\": 3, \"topicBlocks\": 4}"; //length should be 5
        Hashtable headers = UtilitiesScript.CreateHeaders();
        byte[] pData = System.Text.Encoding.UTF8.GetBytes(jsonData.ToCharArray());
        wwwV = new WWW(postOpinionsUrl, pData, headers);

        yield return wwwV;
        wwwVText = wwwV.text; 
        print(wwwV.text);
        JSONNode V = JSON.Parse(wwwV.text);
        waitingV = false;

    }

    private IEnumerator FetchGameTopics()
    {
        
        print("Retrieving JSON topics for all games");

        string postTopicsUrl = GlobalScript.domain + "/getTopicsAndAnswers";
        string jsonData = "{ \"rounds\": [{\"batches\":5,\"size\":1},{\"batches\":5,\"size\":2},{\"batches\":3,\"size\":4},{\"batches\":3,\"size\":1}]}";
        //string jsonData = "{ \"rounds\": [{\"batches\":10,\"size\":1},{\"batches\":10,\"size\":2},{\"batches\":3,\"size\":4},{\"batches\":5,\"size\":1}]}";
        Hashtable headers = new Hashtable();
        headers.Add("Content-Type", "application/json");
        headers.Add("accept-version", GlobalScript.apiVersion);
        byte[] pData = System.Text.Encoding.UTF8.GetBytes(jsonData.ToCharArray());
        www = new WWW(postTopicsUrl, pData, headers);

        yield return www;
        wwwText = www.text; 
        // example return: {"success":true,"data":[[[{"id":"work","rating":0.01438905768877832,"likes":5645,"dislikes":386667}],[{"id":"cheetos","rating":0.6005595315549772,"likes":553836,"dislikes":368364}],[{"id":"lemons","rating":0.6377706617871303,"likes":58555,"dislikes":33257}],[{"id":"tomatos","rating":0.72223333500025,"likes":86655,"dislikes":33327}],[{"id":"foxes","rating":0.6005384138490669,"likes":55378836,"dislikes":36836474}],[{"id":"elephants","rating":0.6986486486486486,"likes":1034,"dislikes":446}],[{"id":"yachts","rating":0.2879690538248314,"likes":13921,"dislikes":34421}],[{"id":"journals","rating":0.29131989623483007,"likes":106797,"dislikes":259800}],[{"id":"beavers","rating":0.20733944954128442,"likes":113,"dislikes":432}],[{"id":"biscuits","rating":0.7298632218844985,"likes":1921,"dislikes":711}]],[[{"id":"yoghurts","rating":0.7269151478311769,"likes":678057,"dislikes":254730},{"id":"gerbils","rating":0.99366921656555,"likes":11301,"dislikes":72}],[{"id":"panthers","rating":0.6368086147821831,"likes":1301,"dislikes":742},{"id":"maggots","rating":0.14917108069065144,"likes":13011,"dislikes":74211}],[{"id":"cats","rating":0.7541899441340782,"likes":135,"dislikes":44},{"id":"superheroes","rating":0.5942489666603722,"likes":56645,"dislikes":38677}],[{"id":"cheese","rating":0.5339358685215372,"likes":95645,"dislikes":83487},{"id":"radios","rating":0.7260589303729257,"likes":674357,"dislikes":254434}],[{"id":"gravy","rating":0.6360566134405463,"likes":568585,"dislikes":325337},{"id":"uni","rating":0.724858696811347,"likes":6797,"dislikes":2580}],[{"id":"vanilla","rating":0.6375423557513622,"likes":586655,"dislikes":333527},{"id":"noodles","rating":0.7169585987261147,"likes":1801,"dislikes":711}],[{"id":"salt","rating":0.7217311541027351,"likes":8655,"dislikes":3337},{"id":"cigarettes","rating":0.6425592625109745,"likes":5855,"dislikes":3257}],[{"id":"pyjamas","rating":0.9359046608764835,"likes":56465,"dislikes":3867},{"id":"deserts","rating":0.6359325218876215,"likes":56825685,"dislikes":32532357}],[{"id":"pickles","rating":0.5339995533720411,"likes":9565,"dislikes":8347},{"id":"leopards","rating":0.20764424843807425,"likes":1130,"dislikes":4312}],[{"id":"beess","rating":0.4893324156916724,"likes":711,"dislikes":742},{"id":"santa","rating":0.9229403868456543,"likes":4123921,"dislikes":344321}]],[[{"id":"june","rating":0.6414473684210527,"likes":585,"dislikes":327},{"id":"fleas","rating":0.14917108069065144,"likes":13011,"dislikes":74211},{"id":"lollipops","rating":0.7259781237122767,"likes":67407357,"dislikes":25443040},{"id":"chips","rating":0.5329241071428571,"likes":955,"dislikes":837}],[{"id":"ladybirds","rating":0.6360188831215321,"likes":56855,"dislikes":32537},{"id":"cockrels","rating":0.14956109188421077,"likes":13051,"dislikes":74211},{"id":"cows","rating":0.01342549514821215,"likes":101,"dislikes":7422},{"id":"buildings","rating":0.7267935893298914,"likes":6757,"dislikes":2540}],[{"id":"chocolate","rating":0.9478433098591549,"likes":12921,"dislikes":711},{"id":"oceans","rating":0.6785849894934803,"likes":682685,"dislikes":323357},{"id":"flamingos","rating":0.001996007984031936,"likes":2,"dislikes":1000},{"id":"clocks","rating":0.6024590163934426,"likes":5586,"dislikes":3686}]],[[{"id":"rainbows","rating":0.9892053284336243,"likes":12921,"dislikes":141}],[{"id":"pencils","rating":0.7901785714285714,"likes":12921,"dislikes":3431}],[{"id":"mice","rating":0.5934873949579832,"likes":565,"dislikes":387}],[{"id":"houses","rating":0.7303128371089536,"likes":677,"dislikes":250}],[{"id":"money","rating":0.5328576521177283,"likes":95665,"dislikes":83867}]]]}
        print(www.text);
        JSONNode N = JSON.Parse(www.text);
        waitingN = false;
        

        // DELETE THIS 
        //wwwT = "{\"data\": { [ [{}] , [{}] , [{\"id\" : \"cats\", \"rating\" : 0.56}], [{}] ] } } ";
        //wwwT = "{\"data\":      [ 	[{}] ,	[{}] , 	[{\"id\" : \"cats\", \"rating\" : 0.56}], [{}] ]   } ";
        //wwwT = "{\"data\":      [ 	[{}] ,	[{}] , 	[{\"id\" : \"cats\", \"rating\" : 0.56}, {\"id\" : \"dogs\", \"rating\" : 0.31}, {\"id\" : \"foxes\", \"rating\" : 0.15}, {\"id\" : \"piglets\", \"rating\" : 0.86}], [{}] ]   }";
        //JSONNode N = JSON.Parse("{\"data\": { [ [{}] , [{}] , [{\"id\" : \"cats\", \"rating\" : 0.56}], [{}] ] } } ");
        //print(N["data"][2].Count);



    }
}

    */
