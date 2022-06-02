using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public string username = "User";
    public int score = 0;
}
[System.Serializable]
public class SaveDataArray
{
    //public SaveData[] saves;
    public List<SaveData> saves = new List<SaveData>();
}
[System.Serializable]
public class LeaderBoard
{
    public string token = "reCmV2mT6M05lOM";
    public SaveData leaderboard = new SaveData();
}

public class SaveManager : MonoBehaviour
{
    public SaveDataArray _SaveData;
    public GameObject LeaderBoardInputGameObject;
    private GameObject LeaderBoardGameObject;
    private string uri = "https://301.sebight.eu/api/leaderboard/VaA4Fvd2Rc";
    public static SaveManager instance;

    private string savePath => $"{Application.dataPath}/PlayerData.json";

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        MakeThisTheOnlySaveManager();
        DontDestroyOnLoad(transform.gameObject);
        _SaveData = new SaveDataArray();
        StartCoroutine(GetRequest(uri));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene == SceneManager.GetSceneByName("MainMenu"))
        {
            LeaderBoardGameObject = Resources.FindObjectsOfTypeAll<LeaderBoardObject>().FirstOrDefault()?.gameObject;
        }
    }

    IEnumerator PostRequest(string url, string json)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
    }

    IEnumerator GetRequest(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();
        _SaveData = JsonUtility.FromJson<SaveDataArray>("{\"saves\":" + webRequest.downloadHandler.text + "}");
        _SaveData.saves.Add(new SaveData());
    }

    public void SaveData()
    {
        LeaderBoard leaderboard = new LeaderBoard();
        leaderboard.leaderboard = _SaveData.saves[^1];
        Debug.Log(JsonUtility.ToJson(leaderboard));
        StartCoroutine(PostRequest(uri, JsonUtility.ToJson(leaderboard)));
    }
    
    public void CreateLeaderboard()
    {
        List<SaveData> sortedSaves = new List<SaveData>();
        sortedSaves.AddRange(_SaveData.saves);
        sortedSaves.Sort((x, y) => y.score.CompareTo(x.score));
        foreach (Transform child in LeaderBoardGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var t in sortedSaves)
        {
            GameObject input = Instantiate(LeaderBoardInputGameObject, LeaderBoardGameObject.transform);
            input.GetComponent<LeaderBoardInput>().SetInput(t.username, t.score);
        }
    }
    
    private void MakeThisTheOnlySaveManager(){
        if(instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else{
            if(instance != this){
                Destroy (gameObject);
            }
        }
    }

    /*private void OnApplicationQuit()
    {
        SaveData();
    }*/
}