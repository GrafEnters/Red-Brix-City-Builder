using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingSO buildingSO;
    public Bubble Bubble;
    public Sprite OnConstructedSprite;

    // Создаёт свою модельку и начинает таймер строительства
    public void Init(BuildingSO building)
    {
        buildingSO = building;
        Instantiate(buildingSO.ModelPrefab, transform);
        Bubble.gameObject.SetActive(true);
        Bubble.StartTimer(building.constructionTime, OnConstructed);
    }

    // Вызывается по окончании таймера строительства
    // Добавляет событий по нажатию на кнопку над собой
    void OnConstructed()
    {
        Bubble.SetValues(OnConstructedSprite);
        Bubble.SetClickedAction(OnFinished);
    }

    // Вызывается по нажатию на иконку завершить постройку
    // Добавляет нужный компонент к себе и инициализирует его
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
