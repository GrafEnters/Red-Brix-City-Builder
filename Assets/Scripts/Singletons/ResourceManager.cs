using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public Resources StartingResources, StartingLimit;
    public int StartingWorkers;

    int workersAmount;
    Resources availableResources, limit;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        availableResources = StartingResources;
        limit = StartingLimit;
        workersAmount = StartingWorkers;

        UI.instance.UpdateResourcesTexts(availableResources, limit);
    }

    public bool HasWorkerAvailable()
    {
        return workersAmount > 0;
    }

    public void ChangeWorker(bool isAdd)
    {
        workersAmount += isAdd ? 1 : -1;
    }

    public void ChangeStorageCapacity(Resources delta)
    {
        limit += delta;
        UI.instance.UpdateResourcesTexts(availableResources, limit);
    }

    //ƒобавл€ет или отнимает ресурсы от текущих
    //¬озвращает ресурсы сверх допустимого хранилища
    public Resources ChangeResources(Resources delta)
    {
        availableResources += delta;
        Resources leftResources = availableResources.ExceedLimit(limit);
        availableResources -= leftResources;

        UI.instance.UpdateResourcesTexts(availableResources, limit);
        return leftResources;
    }

    public bool IsEnoughResources(Resources price)
    {
        return availableResources >= price;
    }

    public bool IsEnoughCapacity(Resources addedResources)
    {
        return limit >= (availableResources + addedResources);
    }
}


[System.Serializable]
public class Resources
{
    public int gas, minerals;
    public Resources() { }
    public Resources(int _gas, int _minerals)
    {
        gas = _gas;
        minerals = _minerals;
    }

    public void Copy(Resources b)
    {
        gas = b.gas;
        minerals = b.minerals;
    }

    //¬озвращает ресурсы сверх лимита
    public Resources ExceedLimit(Resources limit)
    {
        Resources overLimit = new Resources(0, 0);
        if (gas > limit.gas)
            overLimit.gas = gas - limit.gas;
        if (minerals > limit.minerals)
            overLimit.minerals = minerals - limit.minerals;
        return overLimit;
    }

    public static bool operator <=(Resources a, Resources b)
    => a.gas <= b.gas && a.minerals <= b.minerals;

    public static bool operator >=(Resources a, Resources b)
   => a.gas >= b.gas && a.minerals >= b.minerals;

    public static Resources operator -(Resources a) => new Resources(-a.gas, -a.minerals);

    public static Resources operator +(Resources a, Resources b)
       => new Resources(a.gas + b.gas, a.minerals + b.minerals);

    public static Resources operator -(Resources a, Resources b)
      => new Resources(a.gas - b.gas, a.minerals - b.minerals);

    public static Resources operator *(Resources a, int b)
    => new Resources(a.gas *= b, a.minerals *= b);

    public bool IsZero()
    {
        return gas == 0 && minerals == 0;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
