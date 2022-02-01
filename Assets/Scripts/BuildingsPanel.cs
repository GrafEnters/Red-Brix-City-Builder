using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsPanel : MonoBehaviour
{
    public BuildingSO[] offers; // ���������, ��������� ��� �������
    public BuildingOffer BuildingOfferPrefab;
    public Transform ContentTransform;

    private void Start()
    {
        GenerateOffers();
    }

    //���������� ����������� � ��������
    void GenerateOffers()
    {
        for (int i = 0; i < offers.Length; i++)
        {
            BuildingOffer offer = Instantiate(BuildingOfferPrefab, ContentTransform);
            int offerIndex = i;
            offer.SetValues(offers[i], delegate { SelectBuilding(offerIndex); });
            offer.gameObject.SetActive(true);
        }
    }

    //�������� ��������� ��� ������������� �� ������ �� ������ �����������
    //���� �� ������� �������� ��� ��� ���������� �������� - ������ �� ������
    public void SelectBuilding(int index)
    {           
        BuildingSO buildingSO = offers[index];
        if (!ResourceManager.instance.IsEnoughResources(buildingSO.price))
        {
            Debug.Log("Not enough resources! Earn some and comeback!");
            return;
        }

        if (!ResourceManager.instance.HasWorkerAvailable())
        {
            Debug.Log("You don't have a worker available!. Wait until previous buildng finished constructing.");
            return;
        }
        GridManager.instance.StartConstructing(buildingSO);
        gameObject.SetActive(false);
    }

    public void ShowHidePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        GridManager.instance.StopConstructing();
    }
}
