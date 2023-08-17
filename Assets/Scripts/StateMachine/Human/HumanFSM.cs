using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class HumanFSM : FiniteStateMachine
    {
        public NavMeshAgent navMeshAgent;
        public Transform destination;
        public Transform Skin;
        public Animator Animator;
        public float walkingSpeed;
        public float runningSpeed;
        private bool triggeredListenBusker;
        private bool triggeredSitBench;

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
            states["Walk"].OnEnter();
            SwitchState(states["Walk"]);
        }

        public void OnCollisionWithTrigger(string trigger)
        {
            switch (trigger)
            {
                case "ListenBusker":
                    if (AIManager.Instance.goToBuskerChance >= Random.Range(0, 100) && !triggeredListenBusker && !WeatherManager.Instance.isRaining)
                    {
                        SwitchState(states["ListenBusker"]);
                    }
                    triggeredListenBusker = true;
                    break;
                case "SitBench":
                    if (AIManager.Instance.goToBenchChance >= Random.Range(0, 100) && !triggeredSitBench && !WeatherManager.Instance.isRaining)
                    {
                        SwitchState(states["SitBench"]);
                    }
                    triggeredSitBench = true;
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
            } while (destination == manager.SpawnPoints[targetNr].transform);
            destination = manager.SpawnPoints[targetNr].transform;
        }
    }
}