using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;
    public TextMeshProUGUI GasAmountText, MineralsAmountText;
    public GameObject StopConstructingButton;

    private void Awake()
    {
        instance = this;
    }

    //Обновляет кол-во ресурсов в интерфейсе в интерфейсе
    public void UpdateResourcesTexts(Resources resorces, Resources limit )
    {
        GasAmountText.text = resorces.gas.ToString() + "/" + limit.gas.ToString();
        MineralsAmountText.text = resorces.minerals.ToString() + "/" + limit.minerals.ToString();
    }

    //Скрывает/показывает кнопку прекратить строительство
    public void SetStopConstrictingButtonActive(bool isActive)
    {
        StopConstructingButton.SetActive(isActive);
    }
}
