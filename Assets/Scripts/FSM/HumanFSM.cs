using System;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class HumanFSM : FiniteStateMachine
    {
        public NavMeshAgent navMeshAgent;
        public Transform destination;

        public void Initialize(Transform targetDestination)
        {
            destination = targetDestination;
        }
        
        private void Start()
        {
            var HumanWalkState = new HumanWalkState(this);
            var HumanSitBenchState = new HumanSitBenchState(this);
            var HumanListenBuskerState = new HumanListenBuskerState(this);
            
            states.Add("Walk", HumanWalkState);
            states.Add("SitBench", HumanSitBenchState);
            states.Add("ListenBusker", HumanListenBuskerState);

            currentState = HumanWalkState;
            currentState.OnEnter();
        }
    }
}