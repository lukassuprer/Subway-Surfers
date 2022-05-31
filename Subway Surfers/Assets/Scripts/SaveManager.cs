using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class SaveData
{
    public string username = "Lukas";
    public int score;
}
[System.Serializable]
public class SaveDataArray
{
    public SaveData[] saves;
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
    public GameObject LeaderBoardGameObject;
    private string uri = "https://301.sebight.eu/api/leaderboard/VaA4Fvd2Rc";

    private string savePath => $"{Application.dataPath}/PlayerData.json";

    private void Awake()
    {
        _SaveData = new SaveDataArray();
        StartCoroutine(GetRequest(uri));
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
        Array.Sort(_SaveData.saves, (x, y) => y.score.CompareTo(x.score));
        foreach (var t in _SaveData.saves)
        {
            GameObject input = Instantiate(LeaderBoardInputGameObject, LeaderBoardGameObject.transform);
            input.GetComponent<LeaderBoardInput>().SetInput(t.username, t.score);
        }
    }

    /*private void OnApplicationQuit()
    {
        SaveData();
    }*/
}