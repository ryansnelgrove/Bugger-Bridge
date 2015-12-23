using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {

    public GameObject inputPrefab;
    public Slider numberSlider;
    public Text numberDisplay;
    public Canvas gameCanvas;

    public List<InputField> inputFields;

    int numberOfPlayers = 0;

    List<InputField> playerNameFields = new List<InputField>();
    List<PlayerInfo> newPlayers = new List<PlayerInfo>();

    public void UpdatePlayerNumber()
    {
        numberOfPlayers = (int)numberSlider.value;
        numberDisplay.text = ((int)numberSlider.value).ToString();

        UpdateFields();
    }

    void UpdateFields()
    {
        for(int i = 0; i < inputFields.Count; i++)
        {
            if (i < numberOfPlayers)
            {
                inputFields[i].gameObject.SetActive(true);
            }
            else
            {
                inputFields[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        newPlayers = CreatePlayerInfoList();

        gameCanvas.gameObject.SetActive(true);
        GameManager gm = gameCanvas.GetComponentInChildren<GameManager>();
        gm.Initialize(newPlayers);

        gameObject.SetActive(false);

        

    }

    List<PlayerInfo> CreatePlayerInfoList()
    {
        List<PlayerInfo> players = new List<PlayerInfo>();

        for(int i = 0;i < numberOfPlayers; i++)
        {
            players.Add(new PlayerInfo(inputFields[i].text));
        }

        return players;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
