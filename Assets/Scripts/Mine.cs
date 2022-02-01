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

    //��������� ������ �� ������ ��������
    public void StartProduction()
    {
        Bubble.StartTimer(MineSO.secondsToMine, OnFinishedMining);
    }
    //���������� �� ��������� ������� ������
    //�������� �������� ��� ������� �� ������ ��� �����
    public void OnFinishedMining()
    {
        Bubble.SetValues(MineSO.MiningResourceIcon);
        leftResources = MineSO.mineAmount;
        Bubble.SetClickedAction(delegate { OnCollectResource(leftResources); });
    }

    //���������� ������� ������� � ���������
    //���� ������� �� ������� � ��������� - ������ ������� �������� � �����
    //����� �� ����� ������, ���� � ��� �������� �������
    public void OnCollectResource(Resources toCollect)
    {
        leftResources = ResourceManager.instance.ChangeResources(toCollect);

        if (leftResources.IsZero())
            StartProduction();
        else
            Debug.Log("Not enough storage capacity! Increase it or spend some!");
    }
}
