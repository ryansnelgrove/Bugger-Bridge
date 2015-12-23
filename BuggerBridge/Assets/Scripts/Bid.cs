using UnityEngine;
using UnityEngine.UI;

public class Bid : MonoBehaviour {

    public string defaultText = "-";
    public Image realImage;

    Round roundContainer;
    int bidValue;
    string keyboardText = "";

    bool _editable = false;
    bool _isFail = false;

    [SerializeField]
    Toggle xToggle;

    [SerializeField]
    Text bidText;

    private TouchScreenKeyboard keyboard;

    public bool Editable
    {
        get { return _editable; }
    }

    public int BidValue
    {
        get { return bidValue; }
    }

    public bool IsFail
    {
        get { return _isFail; }
    }

    void Update()
    {
        if (keyboard != null && keyboard.done)
        {
            keyboardText = keyboard.text;
            if (keyboardText != defaultText)
            {
                int temp;
                
                if (int.TryParse(keyboardText, out temp))
                {
                    if (temp >= 0 && temp <= roundContainer.ThisRoundNumber)
                    {
                        bidValue = temp;
                        bidText.text = bidValue.ToString();
                        keyboard = null;
                    }
                }
                else
                {
                    keyboard = null;
                }
            }
            else
            {
                keyboard = null;
            }
        }
    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false, false, false, false, "-");
    }

    void ResetBid()
    {
        _editable = true;
        _isFail = false;
        xToggle.isOn = false;
        realImage.enabled = false;

        bidText.text = defaultText;
    }

    public Bid Initialize(Round round)
    {
        roundContainer = round;

        ResetBid();

        return this;
    }

    public void SetBid(int bidVal)
    {
        bidValue = bidVal;
    }

    public void ResetBidValue()
    {
        bidValue = 0;
    }

    public void ChangeEditable(bool value)
    {
        _editable = value;
    }

    public void ToggleFail()
    {
        _isFail = !_isFail;
        xToggle.isOn = _isFail;
        realImage.enabled = _isFail;

        roundContainer.GM.CalculateTotals();
        roundContainer.GM.UpdateScores();
    }

    
}
