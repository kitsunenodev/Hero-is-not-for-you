using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeFurtive : Upgrade
{
    public new String Type = "Furtive";

    public override bool CheckCost()
    {
        return this.Cost <= GameController.Instance.GetFurtiveScore();
    }
}
