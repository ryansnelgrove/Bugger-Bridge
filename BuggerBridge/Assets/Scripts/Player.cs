using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public Text playerNameText;
    public GameObject scorePrefab;
    Text playerScoreText;

    string playerName;

    public Player Initialize(string name)
    {
        GameObject scoresObject = GameObject.Find("Scores");

        GameObject score = Instantiate(scorePrefab);
        score.transform.SetParent(scoresObject.transform);
        score.transform.localScale = Vector3.one;

        playerName = name;
        playerNameText.text = playerName.Substring(0, 1).ToUpper() + playerName.Substring(1,1).ToLower();

        playerScoreText = score.GetComponent<Text>();
        playerScoreText.text = 0.ToString();

        return this;
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public void UpdateScore(int score)
    {
        playerScoreText.text = score.ToString();
    }
}

public class PlayerInfo
{
    public string PlayerName;

    public PlayerInfo(string name)
    {
        PlayerName = name;
    }
    
}
