using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class HumanFSM : FiniteStateMachine
    {
        [SerializeField, Range(0, 100)] private float goToBuskerChance;
        public NavMeshAgent navMeshAgent;
        public Transform destination;
        public Transform Skin;
        private bool triggeredListenBusker;
        
        private void Start()
        {
            var HumanWalkState = new HumanWalkState(this);
            var HumanSitBenchState = new HumanSitBenchState(this);
            var HumanListenBuskerState = new HumanListenBuskerState(this);
            var HumanInBuildingState = new HumanInBuildingState(this);
            
            states.Add("Walk", HumanWalkState);
            states.Add("SitBench", HumanSitBenchState);
            states.Add("ListenBusker", HumanListenBuskerState);
            states.Add("InBuilding", HumanInBuildingState);
            
            SetNewDestination();
            SwitchState(states["Walk"]);
        }

        public void OnCollisionWithTrigger(string trigger)
        {
            switch (trigger)
            {
                case "ListenBusker":
                    if (goToBuskerChance >= Random.Range(0, 100) && !triggeredListenBusker)
                    {
                        SwitchState(states["ListenBusker"]);
                    }
                    triggeredListenBusker = true;
                    break;
                case "SitBench":
                    break;
            }

        }

        public void SetNewDestination()
        {
            var manager = AIManager.Instance;
            int targetNr;
            do
            {
                targetNr = Random.Range(0, manager.SpawnPoints.Count - 1);
            } while (destination == manager.SpawnPoints[targetNr]);
            destination = manager.SpawnPoints[targetNr].transform;
        }
    }
}