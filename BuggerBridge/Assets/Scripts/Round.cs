using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Round : MonoBehaviour {

    public GameObject bidPrefab;
    public Text roundText;

    List<Bid> roundBids = new List<Bid>();

    int thisRoundNumber = 0;

    GameManager gm;

    public int ThisRoundNumber
    {
        get { return thisRoundNumber; }
    }

    public GameManager GM
    {
        get { return gm; }
    }

    public List<Bid> RoundBids
    {
        get { return roundBids; }
    }

    public Round Initialize(int numberOfPlayers, int roundNumber, GameManager gameManager)
    {
        gm = gameManager;
        thisRoundNumber = roundNumber;
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject newBid = Instantiate(bidPrefab);
            newBid.transform.SetParent(transform);
            newBid.transform.localScale = Vector3.one;
            roundBids.Add(newBid.GetComponent<Bid>().Initialize(this));
        }

        roundText.text = roundNumber.ToString();
        return this;
    }
}
