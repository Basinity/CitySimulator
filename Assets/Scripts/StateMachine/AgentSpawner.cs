using System;
using StateMachine;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private void Awake()
    {
        AIManager.Instance.SpawnPoints.Add(this);
    }
}
