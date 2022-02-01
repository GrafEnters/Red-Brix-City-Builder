using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public float landshaftRotationMax; // максимальный угол наклона холмов
    public Transform LandshaftCube;

    bool hasBuildingOnIt;

    public void SetHasBuilding(bool value)
    {
        hasBuildingOnIt = value;
    }

    public bool IsEmpty()
    {
        return !hasBuildingOnIt;
    }

    public void GenerateRandomLandshaft()
    {
        Quaternion rndRot = Quaternion.Euler(Random.Range(-landshaftRotationMax, landshaftRotationMax), 0, Random.Range(-landshaftRotationMax, landshaftRotationMax));
        LandshaftCube.rotation = rndRot;
    }
}
