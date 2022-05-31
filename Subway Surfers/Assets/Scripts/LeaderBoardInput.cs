using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class LeaderBoardInput : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI score;
    
    public void SetInput(string username, int score)
    {
        this.username.text = username;
        this.score.text = score.ToString();
    }
}
