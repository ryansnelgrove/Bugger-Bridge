using UnityEngine;
using System.Collections;


public class Player
{
    public int playerNum;
    public int[] bidArray;
    public bool[] bidXarray;
    int handNumber;

    string nameStr;

    ScoreManager gameManager;

    public string NameStr
    {
        get { return nameStr; }
        set { nameStr = value; }
    }

    public string NameShort
    {
        get { return nameStr.Substring(0, 2).ToUpper(); }
    }

    public Player(ScoreManager mng, string playerName, int numberOfHands)
    {
        gameManager = mng;
        nameStr = playerName;

        handNumber = numberOfHands;

        ResetPlayer();
    }

    public void setBid(int handNumber, int bid)
    {
        //gameManager.bids[handNumber - 1, playerNum] = bid;
        bidArray[handNumber - 1] = bid;
    }

    public int getScore()
    {
        int score = 0;
        for (int i = 0; i < bidXarray.Length; i++)
        {
            if (bidXarray[i] == true)
                score++;
        }
        return score;
    }

    public void ResetPlayer()
    {
        bidArray = new int[handNumber];
        bidXarray = new bool[handNumber];
    }
}
