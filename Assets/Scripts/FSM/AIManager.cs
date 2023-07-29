using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace FSM
{
    public class AIManager : Singleton<AIManager>
    {
        [SerializeField] private int numberOfAgents;
        [SerializeField] private Transform spawnPointsParent;
        [SerializeField] private Transform agentParent;
        [SerializeField] private HumanFSM agentPrefab;
        public Transform Busker;
        public readonly List<Transform> SpawnPoints = new();
        private int agentsTypeCount;

        private void Start()
        {
            agentsTypeCount = agentPrefab.transform.childCount - 1;
            foreach (Transform spawnPoint in spawnPointsParent.transform)
            {
                spawnPoint.GetComponent<MeshRenderer>().enabled = false;
                SpawnPoints.Add(spawnPoint);
            }

            for (var i = 0; i < numberOfAgents; i++)
            {
                SpawnAgent();
            }
        }

        private void Update()
        {
            if (agentParent.childCount < numberOfAgents)
            {
                SpawnAgent();
            }
        }

        private void SpawnAgent()
        {
            var spawnNr = Random.Range(0, SpawnPoints.Count - 1);
            var newAgent = Instantiate(agentPrefab, SpawnPoints[spawnNr].position, new Quaternion(), agentParent);
            newAgent.destination = SpawnPoints[spawnNr];
            
            (newAgent.Skin = newAgent.transform.GetChild(Random.Range(0, agentsTypeCount - 1))).gameObject.SetActive(true);
        }
    }
}