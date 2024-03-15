using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject spawn;
    
    public List<Wave> waves;

    public int currentWave;
    
    public bool waveLaunched;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseWavesSpawn();
        LaunchWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.isRunning)
        {
            NextWave();
            UpdateWaveTimer();
            UpdateWaveStatus();
        }
    }

    //Function to set the spawn point of all the monsters of the waves to the spawn point of the spawner
    void InitialiseWavesSpawn()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            waves[i].spawn = spawn;
        }
    }

    void UpdateWaveTimer()
    {
        if (waveLaunched)
        {
            waves[currentWave].UpdateTimer();
        }
    }

    //Function to start the current wave
    void LaunchWave()
    {
        waves[currentWave].InitialiseWave();
        waveLaunched = true;
    }

    
    //Function to update the status of the wave (Ended, cleared , on going)
    void UpdateWaveStatus()
    {
        if (waves[currentWave].WaveEnd())
        {   
            waveLaunched = false;
        }
        if (waves[currentWave].WaveCleared())
        {
            //Debug.Log(waves[currentWave].WaveCleared());
            currentWave++;
        }
        if (currentWave == waves.Count)
        {
            //WinGame();
        }
    }

    //Function to launch the next wave
    void NextWave()
    {
        if (currentWave == waves.Count)
        {
            return;
        }
        if (waves[currentWave].WaveCleared())
        {
            LaunchWave();
            GameController.Instance.ResetCountKill();
        }
    }

    void WinGame()
    {
        Debug.Log("Game wined");
    }
}
