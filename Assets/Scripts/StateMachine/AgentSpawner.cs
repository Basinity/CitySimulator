using StateMachine;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private void Start()
    {
        AIManager.Instance.SpawnPoints.Add(this);
    }
}
