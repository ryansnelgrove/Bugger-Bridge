using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int[,] bids;
    List<Player> players;
	public static int playerNumber = 1;
	public int maxCards;
	public int[] handArray;
	int turnNumber = 0;

    [SerializeField]
    Toggle xPrefab;

    [SerializeField]
    InputField inputPrefab;

    [SerializeField]
    RectTransform bidObject;

    [SerializeField]
    RectTransform nameObject;

    [SerializeField]
    RectTransform handObject;

    [SerializeField]
    GameObject startUpObject;

    public int TurnNumber
    {
        get { return turnNumber + 1; }
    }

    public string[] PlayerNames
    {
        get { return null; }
    }

    const int NUMBER_OF_CARDS_IN_DECK = 52;
    const int MAX_CARDS_PER_PLAYER = 10;
    

	public void Initialize(int numberOfPlayers, string[] playerNames) {
        playerNumber = numberOfPlayers;

        maxCards = Mathf.Clamp(Mathf.FloorToInt (NUMBER_OF_CARDS_IN_DECK / numberOfPlayers), 1, MAX_CARDS_PER_PLAYER);

        bidObject = (RectTransform)GameObject.FindGameObjectWithTag("BidObject").transform;

        bids = new int[maxCards * 2 - 1, numberOfPlayers];
		handArray = new int[bids.GetLength (0)];

        string handString = null;

		for (int a = 1; a <= handArray.Length; a++) {
			if (a > maxCards)
			{
				handArray[a-1] = handArray.Length - a +1;
			}
			else
			{
				handArray[a-1] = a;
			}
            handString += handArray[a - 1].ToString() + "\n";
		}

        handObject.GetComponent<Text>().text = handString;

        players = new List<Player>();
		for (int b = 0; b < playerNumber; b++) {
            string playerName = playerNames[b];
            Player newPlayer = new Player(this, playerName, maxCards);
            players.Add(newPlayer);
            GameObject.Find("Score" + (b + 1).ToString()).GetComponent<Text>().text = "0";
		}

        string nameString = "\t\t";
        for(int c = 0; c < numberOfPlayers; c++)
        {
            nameString += players[c].NameShort + "\t\t";
        }
        nameObject.GetComponent<Text>().text = nameString;

        PlaceBidSystem();
	}

    void PlaceBidSystem()
    {
        for (int j = 0; j < bids.GetLength(0); j++)
        {
            for (int i = 0; i < playerNumber; i++)
            {
                Toggle newToggle = (Toggle)Instantiate(xPrefab, Vector3.zero, Quaternion.identity);
                newToggle.transform.SetParent(bidObject.FindChild("Player" + (i + 1).ToString()).transform, false);
                newToggle.transform.localPosition = new Vector3(10, -11 - j * 23, 0);
                newToggle.name = "P" + (i+1).ToString() + "." + (j + 1).ToString() + " X";
                newToggle.onValueChanged.AddListener((value) => { CalculateScore(); });

                InputField newField = (InputField)Instantiate(inputPrefab, Vector3.zero, Quaternion.identity);
                newField.transform.SetParent(bidObject.FindChild("Player" + (i + 1).ToString()).transform, false);
                newField.transform.localPosition = new Vector3(25, -11 - j * 23, 0);
                newField.name = "P" + (i + 1).ToString() + "." + (j + 1).ToString() + " #";
                //newField.onEndEdit.AddListener((value) => CheckRowValues(newField, value));
            }
        }
    }

    public void ResetGame()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>();
        foreach(Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }

        InputField[] fields = FindObjectsOfType<InputField>();
        foreach(InputField field in fields)
        {
            field.text = "";
        }

        foreach(Player player in players)
        {
            player.ResetPlayer();
        }

        for(int i = 0; i < playerNumber; i++)
        {
            GameObject.Find("Score" + (i + 1).ToString()).GetComponent<Text>().text = "0";
        }
    }

    public void QuitGame()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Refresh");
        foreach(GameObject go in objects)
        {
            DestroyImmediate(go);
        }

        gameObject.SetActive(false);
        startUpObject.SetActive(true);
    }

    public void CalculateScore()
    {
        int[] scores = new int[playerNumber];
        for(int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
        

        Toggle[] toggles = FindObjectsOfType<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                int playerNum = System.Convert.ToInt16(toggle.name.Substring(1, 1));

                scores[playerNum - 1] += 1;
            }
        }

        for (int i = 0; i < scores.Length; i++)
        {
            GameObject.Find("Score" + (i+1).ToString()).GetComponent<Text>().text = scores[i].ToString();
        }
    }

    void CheckRowValues(InputField field, string value)
    {
        int rowNumber = System.Convert.ToInt16(field.name.Substring(field.name.Length - 3, 1));
        int colNumber = System.Convert.ToInt16(field.name.Substring(1, 1));
        print(colNumber);
        if (colNumber == playerNumber){
            
            print("Nuh uh");
            int totalBids = 0;

            InputField[] fields = FindObjectsOfType<InputField>();
            foreach(InputField aField in fields)
            {
                if (System.Convert.ToInt16(aField.name.Substring(3,1)) == rowNumber)
                {
                    print(aField.text);
                    totalBids += System.Convert.ToInt16(aField.text);
                }
            }

            if (totalBids == rowNumber)
            {
                field.GetComponentInChildren<Text>().text = "";
            }
        }
    }


}
