using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building", menuName ="ScriptableObjects/Building", order = 1)]
public class BuildingSO : ScriptableObject
{
    public string buildingName;
    public Resources price;

    public Sprite icon;
    public GameObject ModelPrefab;
    public int constructionTime; //Время постройки в секундах
}
