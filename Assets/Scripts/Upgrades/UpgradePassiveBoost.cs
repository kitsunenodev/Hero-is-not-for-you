using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePassiveBoost : UpgradePassive
{
    public UpgradePassiveBoost(int boost)
    {
        this.boost = boost;
    }

    public int boost;
    public override bool AddUpgrade()
    {
        if (CheckCost())
        {
            GameController.Instance.playerStats.maxHealth += boost;
            GameController.Instance.playerStats.currentHealth += boost;
            return true;
        }

        return false;
    }
}
