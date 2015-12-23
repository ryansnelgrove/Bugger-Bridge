using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject roundPrefab;
    public RectTransform roundSpawnObject;
    public RectTransform playerNamesObject;
    public GameObject roundSelector;

    List<Player> players = new List<Player>();
    List<Round> rounds = new List<Round>();

    int[] scores;

    int currentRound = 0;

    const int cardsInDeck = 52;
    const int maxPerHand = 10;

    public void Initialize(List<PlayerInfo> newPlayerInfo)
    {
        ClearRounds();

        NewGame(newPlayerInfo);        
    }

    void CreatePlayers(List<PlayerInfo> playerInfoList)
    {
        for(int i = 0; i < playerInfoList.Count; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.SetParent(playerNamesObject.transform);
            newPlayer.transform.localScale = Vector3.one;

            players.Add(newPlayer.GetComponent<Player>().Initialize(playerInfoList[i].PlayerName));
        }
    }

    public Player GetPlayerByIndex(int index)
    {
        return (index > 0 && index < players.Count) ? players[index] : null;
    }

    public Player GetPlayerByName(string name)
    {
        return players.Find(Player => Player.PlayerName == name);
    }

    void ClearRounds()
    {
        foreach(Transform child in roundSpawnObject)
        {
            if (child != roundSelector.transform)
            {
                Destroy(child);
            }
        }

        foreach(Transform child in playerNamesObject)
        {
            Destroy(child);
        }
    }

    void NewGame(List<PlayerInfo> playerInfoList)
    {
        CreatePlayers(playerInfoList);

        int handSize = GetMaxHandSize(playerInfoList.Count);

        List<int> hands = GetHandList(handSize);

        for(int i = 0; i < hands.Count; i++)
        {
            GameObject newRound = Instantiate(roundPrefab);
            newRound.transform.SetParent(roundSpawnObject);
            newRound.transform.localScale = Vector3.one;
            rounds.Add(newRound.GetComponentInChildren<Round>().Initialize(players.Count, hands[i], this));
        }
        currentRound = 1;
    }

    List<int> GetHandList(int maxHandSize)
    {
        List<int> handList = new List<int>();

        for(int i = 1; i <= maxHandSize; i++)
        {
            handList.Add(i);
        }

        for(int i = maxHandSize- 1; i > 0; i--)
        {
            handList.Add(i);
        }

        return handList;
    }

    int GetMaxHandSize(int numberOfPlayers)
    {
        int cards = Mathf.FloorToInt(cardsInDeck / numberOfPlayers);
        return (cards > maxPerHand) ? maxPerHand : cards;
    }

    public void UpdateScores()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].UpdateScore(scores[i]);
        }
    }

    public void CalculateTotals()
    {
        scores = new int[players.Count];
        
        foreach(Round round in rounds)
        {
            List<Bid> bidList = round.RoundBids;
            for(int i = 0; i < bidList.Count; i++)
            {
                if (bidList[i].IsFail)
                {
                    scores[i] += 1;
                }
            }
        }
    }
}
