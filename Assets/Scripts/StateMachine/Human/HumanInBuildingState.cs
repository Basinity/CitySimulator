using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class HumanInBuildingState : State
    {
        private readonly HumanFSM humanFSM;
        private float timeInBuilding;
        
        public HumanInBuildingState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }

        public override void OnEnter()
        {
            humanFSM.Skin.gameObject.SetActive(false);
            humanFSM.navMeshAgent.enabled = false;
            timeInBuilding = Random.Range(30, 60);
        }

        public override void OnUpdate()
        {
            timeInBuilding -= Time.deltaTime;

            if (timeInBuilding <= 0)
            {
                FSM.SwitchState(humanFSM.states["Walk"]);
            }
        }

        public override void OnExit(Action onExit)
        {
            humanFSM.Skin.gameObject.SetActive(true);
            humanFSM.navMeshAgent.enabled = true;
            humanFSM.SetNewDestination();
            onExit();
        }
    }
}
