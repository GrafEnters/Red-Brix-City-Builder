using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingSO buildingSO;
    public Bubble Bubble;
    public Sprite OnConstructedSprite;

    // ������ ���� �������� � �������� ������ �������������
    public void Init(BuildingSO building)
    {
        buildingSO = building;
        Instantiate(buildingSO.ModelPrefab, transform);
        Bubble.gameObject.SetActive(true);
        Bubble.StartTimer(building.constructionTime, OnConstructed);
    }

    // ���������� �� ��������� ������� �������������
    // ��������� ������� �� ������� �� ������ ��� �����
    void OnConstructed()
    {
        Bubble.SetValues(OnConstructedSprite);
        Bubble.SetClickedAction(OnFinished);
    }

    // ���������� �� ������� �� ������ ��������� ���������
    // ��������� ������ ��������� � ���� � �������������� ���
    void OnFinished()
    {
        ResourceManager.instance.ChangeWorker(true);

        if (buildingSO is MineSO)
        {
            MineSO mine = buildingSO as MineSO;
            Mine mineComponent = gameObject.AddComponent<Mine>();
            mineComponent.Init(mine, Bubble);
            Debug.Log("Mine constructing Finished");
        }
        else if (buildingSO is StorageSO)
        {
            StorageSO storage = buildingSO as StorageSO;
            Bubble.gameObject.SetActive(false);
            ResourceManager.instance.ChangeStorageCapacity(storage.storageAmount);
            Debug.Log("Storage constructing Finished");
        }
        else
            Debug.Log("Else constructing Finished");
    }
}
