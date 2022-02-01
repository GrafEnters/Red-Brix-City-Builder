using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine", menuName = "ScriptableObjects/Mine", order = 2)]
public class MineSO : BuildingSO
{
    public Resources mineAmount;
    public int secondsToMine;    //Время добычи в секундах
    public Sprite MiningResourceIcon;
}
