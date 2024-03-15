using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UpgradeList: MonoBehaviour 
{
    public UpgradeHandler upgradeHandler;
    // Start is called before the first frame update
    void Start()
    {
       upgradeHandler.UpdateList(ListUpgrade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Upgrade> ListUpgrade()
    {
        List<Upgrade> list = new List<Upgrade>();
        UpgradeAggressiveBoost upgrade1Agg = new UpgradeAggressiveBoost(5);
        UpgradeAggressiveBoost upgrade2Agg = new UpgradeAggressiveBoost(5);
        UpgradePassiveBoost upgrade1Pas = new UpgradePassiveBoost(10);
        UpgradePassiveBoost upgrade2Pas = new UpgradePassiveBoost(10);
        
        list.Add(upgrade1Agg);
        list.Add(upgrade2Agg);
        list.Add(upgrade1Pas);
        list.Add(upgrade2Pas);
        
        Debug.Log(list);

        return list;
    }
}
