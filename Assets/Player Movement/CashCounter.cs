using UnityEngine;
using TMPro;

public class CashCounter : MonoBehaviour
{
    public int cashAmount = 0; // The player's current cash
    public TextMeshProUGUI cashText; // Reference to the TextMeshPro component that displays cash

    void Start()
    {
        UpdateCashText();
    }

    public void AddCash(int amount)
    {
        cashAmount += amount;
        UpdateCashText();
    }

    void UpdateCashText()
    {
        cashText.text = "Cash: $" + cashAmount.ToString(); // Update the UI with the current cash
    }
}
