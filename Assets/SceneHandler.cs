using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private SimulationSystemGroup _simulationSystemGroup;
    
    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SystemsUtils.SetSystemsEnabled(World.DefaultGameObjectInjectionWorld
                .GetOrCreateSystemManaged<SimulationSystemGroup>(), false);
        }
    }

    public void LoadSceneCallback(string name)
    {
        SceneManager.LoadScene(name);
    }
}
