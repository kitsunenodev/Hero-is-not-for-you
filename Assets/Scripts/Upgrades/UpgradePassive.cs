using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradePassive : Upgrade
{
    public new string Type = "Passive";

    public override bool CheckCost()
    {
        return this.Cost <= GameController.Instance.GetPassiveScore();
    }
}
