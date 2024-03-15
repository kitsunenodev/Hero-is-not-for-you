using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Wave
{
    public List<GameObject> monsterPrefabs;
    
    public List<int> monster;
    
    public GameObject spawn;

    public int startingTime = 10;

    public int waitingTime = 50;
    
    public float timerWait; 
    
    public int currentMonster;

    
    
    //Function used to update the timer counting time between the instantiation of two different monster
    public void UpdateTimer()
    {
        if (!WaveEnd())
        {
            if (timerWait <= 0)
            {
                timerWait = waitingTime;
                InstantiateObject();
                currentMonster++;
                
            }
            else
            {
                timerWait -= 0.01f;
            }
        }
    }

    //Function to indicates whether all the monster of the waves have been Instantiated
    public bool WaveEnd()
    {
        return currentMonster >= monster.Count;
    }

    //Function to start the wave by setting the 
    public void InitialiseWave()
    {
        timerWait = startingTime;
    }

    
    //Function to Instantiate a monster
    void InstantiateObject()
    {
        GameObject go = Object.Instantiate(monsterPrefabs[monster[currentMonster]], spawn.transform, true);
        go.transform.position = spawn.transform.position;
        go.transform.parent = null;
    }

    
    //Function that indicates whether all the monster of the wave have been killed or not
    public bool WaveCleared()
    {
        return GameController.Instance.demonKilled == monster.Count;
    }
}
