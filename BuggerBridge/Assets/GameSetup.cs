using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameSetup : MonoBehaviour {
    [SerializeField]
    Slider numberSlider;

    [SerializeField]
    Text numberDisplay;

    [SerializeField]
    int numberOfPlayers;

    [SerializeField]
    Canvas gameCanvas;

    [SerializeField]
    RectTransform nameFields;

    [SerializeField]
    InputField inputPrefab;

    string[] names;

    InputField[] extraNames = new InputField[0];


    void Update() {
        numberOfPlayers = (int)numberSlider.value;
        numberDisplay.text = numberOfPlayers.ToString();
    }

    public void StartGame()
    {
        FillNameArray();

        foreach (string name in names)
        {
            if (name == "")
            {
                return;
            }
        }

        ScoreManager manager = gameCanvas.gameObject.GetComponent<ScoreManager>();

        gameObject.SetActive(false);
        manager.gameObject.SetActive(true);

        manager.Initialize(numberOfPlayers, names);
    }

    public void AdjustNames()
    {
        numberOfPlayers = (int)numberSlider.value;

        int numberNow = GameObject.FindGameObjectsWithTag("Name").Length;
        int numberNeeded = numberOfPlayers - numberNow;

        if (numberNeeded > 0)
        {
            InputField[] tempStorage = extraNames;
            extraNames = new InputField[numberOfPlayers - 3];

            System.Array.Copy(tempStorage, 0, extraNames, 0, tempStorage.Length);

            names = new string[numberOfPlayers];

            //extraNames = new InputField[numberNeeded];
            for (int i = 0; i < extraNames.Length; i++)
            {
                if (extraNames[i] == null)
                {
                    InputField newField = (InputField)Instantiate(inputPrefab, Vector3.zero, Quaternion.identity);
                    newField.transform.SetParent(nameFields, false);
                    newField.transform.localPosition = new Vector3(0, -100 - i * 25, 0);
                    extraNames[i] = newField;
                }
            }
        }
        else if (numberNeeded < 0)
        {
            InputField[] tempStorage = new InputField[numberOfPlayers - 3];
            System.Array.Copy(extraNames, 0, tempStorage, 0, tempStorage.Length);

            int length = extraNames.Length;
            for (int a = numberNeeded; a < 0; a++)
            {
                Destroy(extraNames[length + a].gameObject);
            }

            extraNames = tempStorage;
        }

        FillNameArray();
    }

    void FillNameArray()
    {
        GameObject[] nameInputFields = GameObject.FindGameObjectsWithTag("Name");
        names = new string[nameInputFields.Length];

        for (int i = names.Length - 1; i >= 0; i--)
        {
            names[i] = nameInputFields[i].GetComponent<InputField>().text;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
