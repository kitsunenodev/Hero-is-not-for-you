using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAggressiveNew : UpgradeAggressive
{
    private readonly Move _move;

    public UpgradeAggressiveNew(Move move)
    {
        _move = move;
    }

    public override bool AddUpgrade()
    {
        if (CheckCost())
        {
            GameController.Instance.playerStats.moveList.Add(_move);
            return true;
        }

        return false;

    }
}
