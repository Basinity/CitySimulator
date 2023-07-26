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
        private readonly List<Transform> spawnPoints = new();
        private int agentsTypeCount;

        private void Start()
        {
            agentsTypeCount = agentPrefab.transform.childCount - 1;
            foreach (Transform spawnPoint in spawnPointsParent.transform)
            {
                spawnPoint.GetComponent<MeshRenderer>().enabled = false;
                spawnPoints.Add(spawnPoint);
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
            var spawnNr = Random.Range(0, spawnPoints.Count - 1);
            var newAgent = Instantiate(agentPrefab, spawnPoints[spawnNr].position, new Quaternion(), agentParent);

            int targetNr;
            do
            {
                targetNr = Random.Range(0, spawnPoints.Count - 1);
            } while (spawnNr == targetNr);
            newAgent.SetDestination(spawnPoints[targetNr]);

            newAgent.transform.GetChild(Random.Range(0, agentsTypeCount - 1)).gameObject.SetActive(true);
        }
    }
}