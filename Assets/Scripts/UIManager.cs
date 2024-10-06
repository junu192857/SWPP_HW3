using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateMoney(int money, int maxMoney) {
        text.text = $"Money : {money} / {maxMoney}";
    }
}
