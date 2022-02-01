using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BuildingOffer : MonoBehaviour
{
    public Button Button;
    public Image iconImage;
    public TextMeshProUGUI Header;
    public TextMeshProUGUI GasPrice, MineralPrice;

    public void SetValues(BuildingSO buildingSO, UnityAction onCLick)
    {
        iconImage.sprite = buildingSO.icon;
        Header.text = buildingSO.name;
        GasPrice.text = buildingSO.price.gas.ToString();
        MineralPrice.text = buildingSO.price.minerals.ToString();
        Button.onClick.AddListener(onCLick);
    }
}
