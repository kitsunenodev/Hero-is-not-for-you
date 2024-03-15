public class UpgradeAggressiveBoost : UpgradeAggressive
{
    public int boost;

    public UpgradeAggressiveBoost(int boost)
    {
        this.boost = boost;
    }

    public override bool AddUpgrade()
    {
        if (CheckCost())
        {
            GameController.Instance.playerStats.strength += boost;
            return true;
        }

        return false;

    }
}
