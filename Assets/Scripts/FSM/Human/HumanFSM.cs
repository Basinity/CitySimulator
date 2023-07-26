using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FSM
{
    public class HumanFSM : FiniteStateMachine
    {
        [SerializeField, Range(0, 100)] private float goToBuskerChance;
        public NavMeshAgent navMeshAgent;
        public Transform destination;
        
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
        
        public void SetDestination(Transform targetDestination)
        {
            destination = targetDestination;
        }

        
        public void OnCollisionEnter(Collision collision)
        {
            OnCilliderWithBuskerTrigger(collision);
        }

        public void OnCilliderWithBuskerTrigger(Collision collision)
        {
            if (!collision.gameObject.CompareTag("BuskerTrigger")) return;
            
            Debug.Log($"I collided with the Busker Trigger! {gameObject.name}");
            if (goToBuskerChance >= Random.Range(0, 100))
            {
                SwitchState(states["ListenBusker"]);
            }
        }
    }
}