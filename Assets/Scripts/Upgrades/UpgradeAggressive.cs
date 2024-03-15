using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeAggressive : Upgrade
{
    public new String Type = "aggressive";

    public override bool CheckCost()
    {
        return Cost <= GameController.Instance.GetAggressiveScore();
    }
}
