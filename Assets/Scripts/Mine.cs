using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public MineSO MineSO;
    public Bubble Bubble;

    Resources leftResources;

    public void Init(MineSO mine, Bubble bubl)
    {
        MineSO = mine;
        Bubble = bubl;
        StartProduction();
    }

    //Запускает таймер на добычу ресурсов
    public void StartProduction()
    {
        Bubble.StartTimer(MineSO.secondsToMine, OnFinishedMining);
    }
    //Вызывается по окончанию таймера добычи
    //Изменяет действие при нажатии на кнопку над собой
    public void OnFinishedMining()
    {
        Bubble.SetValues(MineSO.MiningResourceIcon);
        leftResources = MineSO.mineAmount;
        Bubble.SetClickedAction(delegate { OnCollectResource(leftResources); });
    }

    //Отправляет добытые ресурсы в инвентарь
    //Если ресурсы не влезают в хранилище - лишние ресурсы остаются в шахте
    //Шахта не начнёт добычу, пока в ней остались ресурсы
    public void OnCollectResource(Resources toCollect)
    {
        leftResources = ResourceManager.instance.ChangeResources(toCollect);

        if (leftResources.IsZero())
            StartProduction();
        else
            Debug.Log("Not enough storage capacity! Increase it or spend some!");
    }
}
