using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    
    public GameObject upgradeUIPrefab;

    public GameObject upgradesParentAggressive;
    
    public GameObject upgradesParentFurtive;
    
    public GameObject upgradesParentPassive;
    
    private List<Upgrade> upgrades;
    
    public int nbAggressive;

    public int nbFurtive;

    public int nbPassive;

    public void InstantiateUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.Type == "Aggressive")
            {
                this.InstantiateUpgradeUI(nbAggressive, upgradesParentAggressive, upgrade);
                
            }
            
            if (upgrade.Type == "Passive")
            {
                this.InstantiateUpgradeUI(nbPassive, upgradesParentPassive, upgrade);
                
            }
            
            if (upgrade.Type == "Furtive")
            {
                this.InstantiateUpgradeUI(nbFurtive, upgradesParentFurtive, upgrade);
                
            }

            
        }
    }

    public void InstantiateUpgradeUI(int position, GameObject scrollView, Upgrade upgrade)
    {
        GameObject go = GameObject.Instantiate(upgradeUIPrefab, scrollView.transform, false);
        go.transform.localPosition = Vector3.down *(position * 600);
        go.GetComponent<UpgradeUI>().InitialiseUpgrade(upgrade);
        position++;
    }

    public void UpdateList(List<Upgrade> upgradesList)
    {
        this.upgrades = upgradesList;
        this.InstantiateUpgrades();
    }
}
