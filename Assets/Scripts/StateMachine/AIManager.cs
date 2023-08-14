using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class AIManager : Singleton<AIManager>
    {
        [SerializeField] private int numberOfAgents;
        [SerializeField] private Transform agentParent;
        [SerializeField] private HumanFSM agentPrefab;
        public Transform Busker;
        public readonly List<AgentSpawner> SpawnPoints = new();
        private int agentsTypeCount;

        public IEnumerator Initialize()
        {
            agentsTypeCount = agentPrefab.transform.childCount - 1;

            for (var i = 0; i < numberOfAgents; i++)
            {
                SpawnAgent();
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void SpawnAgent()
        {
            var spawnNr = Random.Range(0, SpawnPoints.Count - 1);
            var newAgent = Instantiate(agentPrefab, SpawnPoints[spawnNr].transform.position, new Quaternion(), agentParent);
            newAgent.destination = SpawnPoints[spawnNr].transform;
            
            (newAgent.Skin = newAgent.transform.GetChild(Random.Range(0, agentsTypeCount - 1))).gameObject.SetActive(true);
        }
    }
}