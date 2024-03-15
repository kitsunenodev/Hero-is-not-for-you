using System;
using UnityEngine;


[Serializable]
public abstract class Upgrade 
{
    public String Type;

    public Sprite TypeImage;

    public int Cost;
    
    public String Description;

    public abstract bool CheckCost();

    public abstract bool AddUpgrade();

}
